﻿using HypoSharp.Input;
using HypoSharp.Rendering;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System;
using System.Collections.Generic;
using OpenTK.Windowing.Common;

namespace HypoSharp.Core
{
    /// <summary>
    /// The current HypoSharp world
    /// </summary>
    public static class World
    {
        // The camera that is currently rendering the scene
        public static Camera Camera { get; set; } 
        // All game logic related objects, aka entities
        public static List<IEntity> EntityObjects { get; private set; }
        // All renderable objects
        public static List<IRenderable> RenderObjects { get; private set; }
        // All tickable objects
        public static List<ITickable> TickableObjects { get; private set; }
        // All the objects we need to add next frame, could be IGameLogic or IRenderable objects
        private static List<object> ObjectsToAdd { get; set; }
        // All the objects we need to remove next frame, could be IGameLogic or IRenderable objects
        private static List<object> ObjectsToRemove { get; set; }
        // Window context
        public static Window Context { get; private set; }
        // The current deferred renderer
        public static DeferredRenderer DeferredRenderer { get; private set; }
        // If the program was closed
        public static bool Closed { get; private set; }
        // Going fullscreen
        private static bool fullscreen;
        public static bool Fullscreen { 
            get { return fullscreen; } 
            set 
            { 
                fullscreen = value;
                if (fullscreen)
                {
                    Context.WindowState = WindowState.Fullscreen;
                    Context.WindowBorder = WindowBorder.Hidden;
                    Context.VSync = VSyncMode.On;
                }
                else
                {
                    Context.WindowState = WindowState.Normal;
                    Context.WindowBorder = WindowBorder.Resizable;
                    Context.VSync = VSyncMode.On;
                }
            } 
        }
        //Callbacks
        public static event Action OnInitializeWorld;
        public static event Action OnDestroyWorld;

        /// <summary>
        /// Initialize the world for the first time
        /// </summary>
        public static void InitializeWorld()
        {
            EntityObjects = new List<IEntity>(); RenderObjects = new List<IRenderable>(); TickableObjects = new List<ITickable>();
            ObjectsToAdd = new List<object>(); ObjectsToRemove = new List<object>();

            // Everything essential
            DeferredRenderer = new DeferredRenderer();
            DeferredRenderer.Initialize();


            OnInitializeWorld?.Invoke();
            foreach (var currentObject in EntityObjects) currentObject.InitializeAbstract(currentObject);
            Console.WriteLine("World: World finished initialization.");
        }

        /// <summary>
        /// Right before we call the .Run method
        /// </summary>
        public static void PreInitializeWorld(Window _context)
        {
            Context = _context;

            // Setup the pre window stuff
            Context.CursorGrabbed = true;
            Context.VSync = VSyncMode.On;
        }

        /// <summary>
        /// Adds an object to the world
        /// </summary>
        /// <param name="obj">The new object</param>
        public static void AddObject(object obj) { ObjectsToAdd.Add(obj); }

        /// <summary>
        /// Removes an object from the world
        /// </summary>
        /// <param name="obj">The object to remove</param>
        public static void RemoveObject(object obj) { ObjectsToRemove.Add(obj); }

        /// <summary>
        /// This method is ran when we want to render stuff
        /// </summary>
        public static void RenderWorld(double delta)
        {
            // Call the loop method each IGameLogic object
            Time.DeltaTime = (float)delta;
            Time.TimeSinceGameStart += Time.DeltaTime;
            Time.TimeSinceLastTick += Time.DeltaTime;

            if (Closed) return;            

            // Update the IEntity objects
            foreach (var entityObject in EntityObjects) entityObject.Loop();

            // Update the ITickable objects
            if (Time.TimeSinceLastTick > (1f / Time.TICK_RATE))
            {
                Time.TimeSinceLastTick = 0f;
                foreach (var tickableObject in TickableObjects) tickableObject.Tick();
            }

            // Add the queued objects
            foreach (var addObj in ObjectsToAdd)
            {
                if (addObj is IEntity)
                {
                    ((IEntity)addObj).InitializeAbstract(addObj);
                    EntityObjects.Add(addObj as IEntity);
                }
                if (addObj is IRenderable) RenderObjects.Add((IRenderable)addObj);
                if (addObj is ITickable) TickableObjects.Add((ITickable)addObj);

                Console.WriteLine($"World: Added object {addObj}");
            }
            ObjectsToAdd.Clear();

            // Remove the queued objects
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
                Console.WriteLine($"World: Removed object {removeObj}");
            }
            ObjectsToRemove.Clear();

            // Render everything
            DeferredRenderer.Render(Camera);

            // Tick
            Time.Ticks++;
        }

        /// <summary>
        /// When the user closes the program
        /// </summary>
        public static void DestroyWorld_AKA_NUKE()
        {
            Closed = true;
            OnDestroyWorld?.Invoke();
            DeferredRenderer.Dispose();
            foreach (var logicObject in EntityObjects) logicObject.Dispose();
        }
    }
}
