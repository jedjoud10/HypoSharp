using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;
using System.Numerics;

namespace HypoSharp.Core
{
    /// <summary>
    /// The current HypoSharp world
    /// </summary>
    public static class World
    {
        //Main world vars
        public static Camera3D camera;
        private static List<EngineObject> objects;
        private static float deltaTime;
        public static event Action OnInitializeWorld;

        /// <summary>
        /// Initialize the world for the first time
        /// </summary>
        public static void InitializeWorld() 
        {
            objects = new List<EngineObject>();
            camera = new Camera3D(new Vector3(0, 30, -100), Vector3.Zero, Vector3.UnitY, 60, CameraType.CAMERA_PERSPECTIVE);            
            Raylib.SetCameraMode(camera, CameraMode.CAMERA_CUSTOM);
            OnInitializeWorld?.Invoke();
        }

        /// <summary>
        /// Adds an object to the world
        /// </summary>
        /// <param name="obj">The new object</param>
        public static void AddObject(EngineObject obj) 
        {
            objects.Add(obj);
        }

        /// <summary>
        /// Removes an object from the world
        /// </summary>
        /// <param name="obj">The object to remove</param>
        public static void RemoveObject(EngineObject obj) 
        {
            obj.Dispose();
            objects.Remove(obj);
        }

        /// <summary>
        /// This method is ran every frame
        /// </summary>
        public static void FrameWorld() 
        {
            //Update camera
            Raylib.DisableCursor();
            Raylib.UpdateCamera(ref camera);

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.WHITE);
            Raylib.BeginMode3D(camera);
            deltaTime = Raylib.GetFrameTime();

            //Call the frame function on each object
            foreach (var currentObject in objects) currentObject.Frame(deltaTime);

            Raylib.DrawGrid(50, 10);
            Raylib.EndMode3D();

            //Draw fps
            Raylib.DrawText($"FPS: {Raylib.GetFPS()}", 12, 12, 20, Color.BLACK);
            Raylib.DrawText($"DELTA: {deltaTime}", 12, 32, 20, Color.BLACK);

            Raylib.EndDrawing();
        }

        /// <summary>
        /// When the user closes the program
        /// </summary>
        public static void DestroyWorld() 
        {
            foreach (var currentObject in objects) currentObject.Dispose();
        }
    }
}
