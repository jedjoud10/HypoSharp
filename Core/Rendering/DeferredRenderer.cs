using HypoSharp.Core;
using HypoSharp.Core.Primitives;
using HypoSharp.Core.Rendering;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System;
using System.Numerics;
using HypoSharp.Core.Input;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace HypoSharp.Core.Rendering
{
    /// <summary>
    /// The deferred renderer that renders the whole scene using a specific camera
    /// </summary>
    public class DeferredRenderer
    {
        /// <summary>
        /// Initialize the renderer
        /// </summary>
        public void Initialize()
        {
            Console.WriteLine("Renderer: Renderer started initialization...");    
            
            // Setup the deferred renderer
            GL.ClearColor(Color.FromArgb(255, 90, 168, 242));
            GL.Enable(EnableCap.DepthTest);

            GL.Enable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.Front);
            GL.FrontFace(FrontFaceDirection.Cw);

            Console.WriteLine("Renderer: Renderer finished initialization");
        }

        /// <summary>
        /// Renders the scene (Deferred lighting)
        /// </summary>
        public void Render(Camera camera)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            // Call the render method each IRenderable object
            foreach (var renderableObject in World.RenderObjects) renderableObject.Render(camera);
            World.Context.SwapBuffers();
        }

        /// <summary>
        /// Unload this deferred renderer
        /// </summary>
        public void Dispose() 
        {

        }
    }
}
