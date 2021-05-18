using HypoSharp.Core;
using HypoSharp.Core.Primitives;
using HypoSharp.Core.Rendering;
using OpenTK.Graphics.OpenGL4;
using System.Drawing;
using System;
using System.Numerics;

namespace HypoSharp.Core.Rendering
{
    /// <summary>
    /// The renderer
    /// </summary>
    public class DeferredRenderer
    {
        Vector3[] vertices = 
        {
            new Vector3(-0.5f, -0.5f, 0),
            new Vector3(0.5f, -0.5f, 0),
            new Vector3(0, 0.5f, 0),
        };
        int VertexBufferObject;
        /// <summary>
        /// Initialize the deferred renderer
        /// </summary>
        public DeferredRenderer() 
        {
            //Rendering stuff
            GL.ClearColor(Color.Black);
        }

        /// <summary>
        /// Renders the scene (Deffered lighting)
        /// </summary>
        public void Render(Camera camera)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            World.Context.SwapBuffers();
            //Call the render method each IRenderable object
            //foreach (var renderableObject in World.RenderObjects) renderableObject.Render();
        }

        /// <summary>
        /// Unload this deferred renderer
        /// </summary>
        public void Dispose() 
        {
        }
    }
}
