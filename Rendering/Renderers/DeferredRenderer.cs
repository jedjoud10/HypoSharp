using HypoSharp.Core;
using System;
using System.Drawing;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace HypoSharp.Rendering
{
    /// <summary>
    /// The deferred renderer that renders the whole scene using a specific camera
    /// </summary>
    public class DeferredRenderer : BaseRenderer
    {
        /// <summary>
        /// Initialize the renderer
        /// </summary>
        public override void Initialize()
        {
            // Setup the deferred renderer
            GL.ClearColor(Color.FromArgb(255, 90, 168, 242));
            GL.Enable(EnableCap.DepthTest);

            GL.Enable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.Front);
            GL.FrontFace(FrontFaceDirection.Cw);
            base.Initialize();
        }

        /// <summary>
        /// Renders the scene (Deferred lighting)
        /// </summary>
        public override void Render(Camera camera)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            // Call the render method each IRenderable object
            foreach (var renderableObject in World.RenderObjects) renderableObject.Render(camera);
            World.Context.SwapBuffers();
        }

        /// <summary>
        /// Unload this deferred renderer
        /// </summary>
        public override void Dispose() 
        {

        }
    }
}
