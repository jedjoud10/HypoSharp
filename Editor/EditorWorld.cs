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
        // All the objects in the world
        public static List<object> Objects { get; private set; }
        // All the IEditor objects in the world
        public static List<IEditorEntity> EditorObjects { get; private set; }        
        // Window context
        public static Window Context { get; private set; }
        // The current editor renderer
        public static BaseRenderer EditorRenderer { get; private set; }
        // If the program was closed
        public static bool Closed { get; private set; }
        
        //Callbacks
        public static event Action OnEditorInitializeWorld;
        public static event Action OnEditorDestroyWorld;

        /// <summary>
        /// Initialize the world for the first time
        /// </summary>
        public static void InitializeWorld()
        {
            EditorObjects = new List<IEditorEntity>();

            // Everything essential
            EditorRenderer = new DeferredRenderer();
            EditorRenderer.Initialize();


            OnEditorInitializeWorld?.Invoke();
            foreach (var currentObject in EditorObjects) currentObject.EditorInitialize();
            Console.WriteLine("Editor: Editor finished initialization");
        }

        /// <summary>
        /// This method is ran when we want to render stuff
        /// </summary>
        public static void RenderWorld(double delta)
        {
            // Render everything
            EditorRenderer.Render(EditorCamera);
        }

        /// <summary>
        /// When the user closes the editor
        /// </summary>
        public static void DestroyEditor()
        {
            Closed = true;
            OnEditorDestroyWorld?.Invoke();
            EditorRenderer.Dispose();
            foreach (var editorObject in EditorObjects) editorObject.EditorDispose();
        }
    }
}
