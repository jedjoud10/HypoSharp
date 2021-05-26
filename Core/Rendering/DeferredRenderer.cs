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
        // Test model
        Model model;
        ModelRenderer renderer;

        /// <summary>
        /// Initialize the renderer
        /// </summary>
        public void Initialize()
        {
            Console.WriteLine("Renderer: Renderer started initialization...");    
            
            // Setup the deferred renderer
            GL.ClearColor(Color.Black);
            GL.Enable(EnableCap.DepthTest);
            // Create a test model
            model = new Model()
            {
                Vertices = new Vector3[] {
                    new Vector3(0, 0, 0),
                    new Vector3(1, 0, 0),
                    new Vector3(1, 0, 1),
                    new Vector3(0, 0, 1),
                },
                Indices = new uint[] 
                {
                    0, 1, 2,
                    2, 3, 0,
                }                
            };
            renderer = new ModelRenderer() { Model = model };

            Console.WriteLine("Renderer: Renderer finished initialization");
        }

        /// <summary>
        /// Renders the scene (Deferred lighting)
        /// </summary>
        public void Render(Camera camera)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            renderer.RenderModel(camera);
            // Call the render method each IRenderable object
            //foreach (var renderableObject in World.RenderObjects) renderableObject.Render();
            World.Context.SwapBuffers();
        }

        /// <summary>
        /// Unload this deferred renderer
        /// </summary>
        public void Dispose() 
        {
            renderer.DisposeModel();
        }
    }
}
