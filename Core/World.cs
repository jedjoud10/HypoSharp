using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;
using System.Numerics;
using HypoSharp.Rendering;

namespace HypoSharp.Core
{
    /// <summary>
    /// The current HypoSharp world
    /// </summary>
    public static class World
    {
        //Main world vars
        public static Camera Camera { get; set; }
        private static List<EngineObject> objects;
        private static float deltaTime;
        public static event Action OnInitializeWorld;

        /// <summary>
        /// Initialize the world for the first time
        /// </summary>
        public static void InitializeWorld() 
        {
            objects = new List<EngineObject>();
            OnInitializeWorld?.Invoke();
            foreach (var currentObject in objects) currentObject.Initialize();
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

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.WHITE);

            deltaTime = Raylib.GetFrameTime();

            //Call the frame function on each object
            foreach (var currentObject in objects) currentObject.Loop(deltaTime);

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
