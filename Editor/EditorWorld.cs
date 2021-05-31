using HypoSharp.Core;
using HypoSharp.Rendering;
using HypoSharp.Primitives;
using HypoSharp.Input;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System;
using System.Collections.Generic;
using OpenTK.Windowing.Common;

namespace HypoSharp.Editor
{
    /// <summary>
    /// The editor world, containing all the objects and everything related to the game engine
    /// </summary>
    public static class EditorWorld
    {
        // The camera that is currently rendering the scene
        public static Camera EditorCamera { get; set; }
        // All renderable objects
        public static List<IRenderable> RenderObjects { get; private set; }
        // All the editor logic objects
        public static List<IEditorEntity> EditorObjects { get; private set; }
        // All the objects in the world
        public static List<object> Objects { get; private set; }
        // All the objects we need to add the next editor frame
        private static List<object> ObjectsToAdd { get; set; }
        // All the objects we need to remove for the next editor frame
        private static List<object> ObjectsToRemove { get; set; }
        // The current editor renderer
        public static BaseRenderer EditorRenderer { get; private set; }
        // If the program was closed
        public static bool Closed { get; private set; }
        
        //Callbacks
        public static event Action OnInitializeEditor;
        public static event Action OnDestroyEditor;

        /// <summary>
        /// Initialize the world for the first time
        /// </summary>
        public static void InitializeEditor()
        {
            Objects = new List<object>(); RenderObjects = new List<IRenderable>(); EditorObjects = new List<IEditorEntity>();
            ObjectsToAdd = new List<object>(); ObjectsToRemove = new List<object>();

            // Everything essential
            EditorRenderer = new DeferredRenderer();
            EditorRenderer.Initialize();

            // Alert other classes that the editor finished initializing
            OnInitializeEditor?.Invoke();
            // Add the editor objects
            AddQueuedObjects();
            Console.WriteLine("Editor: Editor finished initialization");
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
        public static void RenderEditor(double delta)
        {
            Time.EditorDeltaTime = (float)delta;

            // Add / Remove pending objects
            AddQueuedObjects();
            RemoveQueuedObjects();

            // Update the IEditorEntity objects
            foreach (var editorObject in EditorObjects) editorObject.EditorLoop();

            // Render everything
            EditorRenderer.Render(EditorCamera, RenderObjects);
        }

        /// <summary>
        /// Add the objects from the queued list to the actual world and sort them
        /// </summary>
        private static void AddQueuedObjects()
        {
            // Add the queued objects
            foreach (var addObj in ObjectsToAdd)
            {
                // Add the object as an IRenderable
                if (addObj is IRenderable)
                {
                    ((IRenderable)addObj).RenderInitialize();
                    RenderObjects.Add((IRenderable)addObj);
                }
                // Add the object as an IEditorEntity
                if (addObj is IEditorEntity)
                {
                    ((IEditorEntity)addObj).EditorInitialize();
                    EditorObjects.Add((IEditorEntity)addObj);
                }

                Console.WriteLine($"Editor: Added object {addObj}");
            }
            ObjectsToAdd.Clear();
        }

        /// <summary>
        /// Remove the queued objects from the world
        /// </summary>
        private static void RemoveQueuedObjects()
        {
            // Remove the queued objects
            foreach (var removeObj in ObjectsToRemove)
            {
                // Remove the object if it's an IRenderable
                if (removeObj is IRenderable)
                {
                    IRenderable renderableEntity = (IRenderable)removeObj;
                    renderableEntity.RenderDispose();
                    RenderObjects.Remove(renderableEntity);
                }
                // Remove the object if it's an IEditorEntity
                if (removeObj is IEditorEntity)
                {
                    IEditorEntity editorEntity = (IEditorEntity)removeObj;
                    editorEntity.EditorDispose();
                    EditorObjects.Remove(editorEntity);
                }
                Console.WriteLine($"Editor: Removed object {removeObj}");
            }
            ObjectsToRemove.Clear();
        }

        /// <summary>
        /// When the user closes the editor
        /// </summary>
        public static void DestroyEditor()
        {
            Closed = true;
            OnDestroyEditor?.Invoke();
            EditorRenderer.Dispose();
            foreach (var obj in Objects) 
            {
                // Dipose of the editor entities
                if (obj is IEditorEntity) ((IEditorEntity)obj).EditorDispose();
                // Dipose of the renderable entities
                if (obj is IRenderable) ((IRenderable)obj).RenderDispose();
            }
        }
    }
}
