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
        public static DeferredRenderer Renderer { get; set; }
        public static List<IGameLogic> LogicObjects { get; set; }
        public static List<IRenderable> RenderObjects { get; set; }
        private static List<object> ObjectsToAdd { get; set; }
        private static List<object> ObjectsToRemove { get; set; }
        private static float _deltaTime;
        public static event Action OnInitializeWorld;
        public static event Action OnDestroyWorld;

        /// <summary>
        /// Initialize the world for the first time
        /// </summary>
        public static void InitializeWorld()
        {
            Renderer = new DeferredRenderer();
            LogicObjects = new List<IGameLogic>(); RenderObjects = new List<IRenderable>();
            ObjectsToAdd = new List<object>(); ObjectsToRemove = new List<object>();
            OnInitializeWorld?.Invoke();
            foreach (var currentObject in LogicObjects) currentObject.Initialize();
        }

        /// <summary>
        /// Adds an object to the world
        /// </summary>
        /// <param name="obj">The new object</param>
        public static void AddObject(object obj)
        {
            ObjectsToAdd.Add(obj);
        }

        /// <summary>
        /// Removes an object from the world
        /// </summary>
        /// <param name="obj">The object to remove</param>
        public static void RemoveObject(object obj)
        {
            ObjectsToRemove.Add(obj);
        }

        /// <summary>
        /// This method is ran every frame
        /// </summary>
        public static void FrameWorld()
        {
            Raylib.DisableCursor();

            //Call the loop method each IGameLogic object
            _deltaTime = Raylib.GetFrameTime();
            Time.DeltaTime = _deltaTime;
            Time.TimeSinceGameStart += _deltaTime;


            foreach (var logicObject in LogicObjects) logicObject.Loop();

            //Add / Remove objects
            foreach (var addObj in ObjectsToAdd)
            {
                if (addObj is IGameLogic)
                {
                    ((IGameLogic)addObj).Initialize();
                    LogicObjects.Add(addObj as IGameLogic);
                }
                if (addObj is IRenderable) RenderObjects.Add(addObj as IRenderable);
            }
            ObjectsToAdd.Clear();

            foreach (var removeObj in ObjectsToRemove)
            {
                if (removeObj is IGameLogic)
                {
                    IGameLogic castObj = ((IGameLogic)removeObj);
                    castObj.Dispose();
                    LogicObjects.Remove(castObj);
                }
                if (removeObj is IRenderable)
                {
                    RenderObjects.Remove((IRenderable)removeObj);
                }
            }
            ObjectsToRemove.Clear();


            Camera.Draw();
        }

        /// <summary>
        /// When the user closes the program
        /// </summary>
        public static void DestroyWorld()
        {
            OnDestroyWorld?.Invoke();
            foreach (var logicObject in LogicObjects) logicObject.Dispose();
        }
    }
}
