using HypoSharp.Core;
using HypoSharp.Core.Primitives;
using HypoSharp.Core.Rendering;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System;
using System.Numerics;

namespace HypoSharp.Core.Rendering
{
    /// <summary>
    /// The deferred renderer that renders the whole scene using a specific camera
    /// </summary>
    public class DeferredRenderer
    {
        Vector3[] vertices = 
        {
            new Vector3(-0.5f, -0.5f, 0),
            new Vector3(0.5f, -0.5f, 0),
            new Vector3( 0, 0.5f, 0)
        };
        Model model;
        ModelRenderer renderer;

        /// <summary>
        /// Initialize the renderer
        /// </summary>
        public void Initialize()
        {
            Console.WriteLine("Renderer: Renderer started initialization...");            
            GL.ClearColor(1, 1, 1, 1);

            model = new Model()
            {
                Vertices = vertices
            };
            renderer = new ModelRenderer()
            {
                Model = model
            };
        }

        /// <summary>
        /// Renders the scene (Deffered lighting)
        /// </summary>
        public void Render(Camera camera)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            renderer.RenderModel();
            //Call the render method each IRenderable object
            //foreach (var renderableObject in World.RenderObjects) renderableObject.Render();
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
