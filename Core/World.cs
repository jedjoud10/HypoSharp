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
        //All game logic related objects, aka entities
        public static List<IEntity> EntityObjects { get; private set; }
        //All renderable objects
        public static List<IRenderable> RenderObjects { get; private set; }
        //All tickable objects
        public static List<ITickable> TickableObjects { get; private set; }
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
            EntityObjects = new List<IEntity>(); RenderObjects = new List<IRenderable>(); TickableObjects = new List<ITickable>();
            ObjectsToAdd = new List<object>(); ObjectsToRemove = new List<object>();

            //Everything essential
            DeferredRenderer = new DeferredRenderer();
            DeferredRenderer.Initialize();

            OnInitializeWorld?.Invoke();
            foreach (var currentObject in EntityObjects) currentObject.Initialize();

            Console.WriteLine("World.cs: World finished initialization");
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
            Time.TimeSinceGameStart += Time.DeltaTime;
            Time.TimeSinceLastTick += Time.DeltaTime;

            //Update the IEntity objects
            foreach (var entityObject in EntityObjects) entityObject.Loop();

            //Update the ITickable objects
            if (Time.TimeSinceLastTick > (1f / Time.TICK_RATE))
            {
                Time.TimeSinceLastTick = 0f;
                Console.WriteLine("World.cs: Tick");
                foreach (var tickableObject in TickableObjects) tickableObject.Tick();
            }
            //Add the queued objects
            foreach (var addObj in ObjectsToAdd)
            {
                if (addObj is IEntity)
                {
                    ((IEntity)addObj).Initialize();
                    EntityObjects.Add(addObj as IEntity);
                }
                if (addObj is IRenderable) RenderObjects.Add((IRenderable)addObj);
                if (addObj is ITickable) TickableObjects.Add((ITickable)addObj);

                Console.WriteLine($"World.cs: Add object {addObj}");
            }
            ObjectsToAdd.Clear();

            //Remove the queued objects
            foreach (var removeObj in ObjectsToRemove)
            {
                if (removeObj is IEntity)
                {
                    IEntity castObj = ((IEntity)removeObj);
                    castObj.Dispose();
                    EntityObjects.Remove(castObj);
                }
                if (removeObj is IRenderable) RenderObjects.Remove((IRenderable)removeObj);
                if (removeObj is ITickable) TickableObjects.Remove((ITickable)removeObj);
                Console.WriteLine($"World.cs: Remove object {removeObj}");
            }
            ObjectsToRemove.Clear();

            Time.Ticks++;
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
        public static void DestroyWorld_AKA_NUKE()
        {
            OnDestroyWorld?.Invoke();
            DeferredRenderer.Dispose();
            foreach (var logicObject in EntityObjects) logicObject.Dispose();
        }
    }
}
