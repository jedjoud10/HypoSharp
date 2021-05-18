using HypoSharp.Core.Input;
using HypoSharp.Core.Rendering;
using OpenTK.Graphics.OpenGL4;
using System.Drawing;
using System;
using System.Collections.Generic;

namespace HypoSharp.Core
{
    /// <summary>
    /// The current HypoSharp world
    /// </summary>
    public static class World
    {
        //The camera that is currently rendering the scene
        public static Camera Camera { get; set; } 
        //All game logic related objects
        public static List<IGameLogic> LogicObjects { get; set; }
        //All renderable objects
        public static List<IRenderable> RenderObjects { get; set; }
        //All the objects we need to add next frame, could be IGameLogic or IRenderable objects
        private static List<object> ObjectsToAdd { get; set; }
        //All the objects we need to remove next frame, could be IGameLogic or IRenderable objects
        private static List<object> ObjectsToRemove { get; set; }
        //Window context
        public static HypoSharpWindow Context { get; private set; }
        //The current deferred renderer
        public static DeferredRenderer DeferredRenderer { get; private set; }

        //Callbacks
        public static event Action OnInitializeWorld;
        public static event Action OnDestroyWorld;

        /// <summary>
        /// Initialize the world for the first time
        /// </summary>
        public static void InitializeWorld(HypoSharpWindow _context)
        {
            Context = _context;
            LogicObjects = new List<IGameLogic>(); RenderObjects = new List<IRenderable>();
            ObjectsToAdd = new List<object>(); ObjectsToRemove = new List<object>();
            DeferredRenderer = new DeferredRenderer();            

            OnInitializeWorld?.Invoke();
            foreach (var currentObject in LogicObjects) currentObject.Initialize();

            Console.WriteLine("WORLD FINISHED INITIALIZATION");
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
        public static void UpdateWorld(double delta)
        {
            //Call the loop method each IGameLogic object
            Time.DeltaTime = (float)delta;
            Time.TimeSinceGameStart += (float)delta;

            //Update the IGameLogic objects
            foreach (var logicObject in LogicObjects) logicObject.Loop();

            //Add the queued objects
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

            //Remove the queued objects
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
        }

        /// <summary>
        /// This method is ran when we want to render stuff
        /// </summary>
        public static void RenderWorld()
        {
            //Render everything
            DeferredRenderer.Render(Camera);
        }

        /// <summary>
        /// When the user closes the program
        /// </summary>
        public static void DestroyWorld()
        {
            OnDestroyWorld?.Invoke();
            DeferredRenderer.Dispose();
            foreach (var logicObject in LogicObjects) logicObject.Dispose();
        }
    }
}
