using HypoSharp.Rendering;
using Raylib_cs;
using System;
using System.Collections.Generic;

namespace HypoSharp.Core
{
    /// <summary>
    /// The current HypoSharp world
    /// </summary>
    public static class World
    {
        //Main world vars
        public static Camera Camera { get; set; }
        private static List<IGameLogic> _logicObjects;
        private static List<IRenderable> _renderObjects;
        private static float _deltaTime;
        public static event Action OnInitializeWorld;

        /// <summary>
        /// Initialize the world for the first time
        /// </summary>
        public static void InitializeWorld()
        {
            _logicObjects = new List<IGameLogic>(); _renderObjects = new List<IRenderable>();
            OnInitializeWorld?.Invoke();
            foreach (var currentObject in _logicObjects) currentObject.Initialize();
        }

        /// <summary>
        /// Adds an object to the world
        /// </summary>
        /// <param name="obj">The new object</param>
        public static void AddObject(object obj)
        {
            if (obj is IGameLogic)
            {
                ((IGameLogic)obj).Initialize();
                _logicObjects.Add(obj as IGameLogic);
            }
            if (obj is IRenderable) _renderObjects.Add(obj as IRenderable);
        }

        /// <summary>
        /// Removes an object from the world
        /// </summary>
        /// <param name="obj">The object to remove</param>
        public static void RemoveObject(object obj)
        {
            if (obj is IGameLogic)
            {
                IGameLogic castObj = ((IGameLogic)obj);
                castObj.Dispose();
            }
            if (obj is IRenderable)
            {
                _renderObjects.Remove((IRenderable)obj);
            }
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

            _deltaTime = Raylib.GetFrameTime();

            //Call the loop method each IGameLogic object
            foreach (var logicObject in _logicObjects) logicObject.Loop(_deltaTime);

            //Call the render method each IRenderable object
            foreach (var renderableObject in _renderObjects) renderableObject.Render();

            Raylib.DrawGrid(50, 10);
            Raylib.EndMode3D();

            //Draw fps
            Raylib.DrawText($"FPS: {Raylib.GetFPS()}", 12, 12, 20, Color.BLACK);
            Raylib.DrawText($"DELTA: {_deltaTime}", 12, 32, 20, Color.BLACK);
            Raylib.DrawText($"CAM POS: {Camera.Position.ToString("0.0")}", 12, 52, 20, Color.BLACK);

            Raylib.EndDrawing();
        }

        /// <summary>
        /// When the user closes the program
        /// </summary>
        public static void DestroyWorld()
        {
            foreach (var logicObject in _logicObjects) logicObject.Dispose();
        }
    }
}
