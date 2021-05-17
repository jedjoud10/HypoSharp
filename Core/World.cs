using HypoSharp.Core.Input;
using HypoSharp.Core.Rendering;
using OpenTK.Graphics.OpenGL;
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
        //Main world vars
        public static Camera Camera { get; set; }
        public static List<IGameLogic> LogicObjects { get; set; }
        public static List<IRenderable> RenderObjects { get; set; }
        private static List<object> ObjectsToAdd { get; set; }
        private static List<object> ObjectsToRemove { get; set; }
        public static event Action OnInitializeWorld;
        public static event Action OnDestroyWorld;
        private static HypoSharpWindow Context;

        /// <summary>
        /// Initialize the world for the first time
        /// </summary>
        public static void InitializeWorld(HypoSharpWindow _context)
        {
            Context = _context;
            LogicObjects = new List<IGameLogic>(); RenderObjects = new List<IRenderable>();
            ObjectsToAdd = new List<object>(); ObjectsToRemove = new List<object>();
            //Rendering stuff
            GL.ClearColor(Color.Black);

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
        }

        /// <summary>
        /// This method is ran when we want to render stuff
        /// </summary>
        public static void RenderWorld()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            Context.SwapBuffers();
            //Render everything
            Camera.Draw();
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
