using HypoSharp.Core;
using System;
using System.Drawing;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace HypoSharp.Rendering
{
    /// <summary>
    /// An abstract renderer, used to create multiple types of renderer
    /// </summary>
    public abstract class BaseRenderer
    {
        /// <summary>
        /// Initialize the renderer
        /// </summary>
        public virtual void Initialize()
        {              
            Console.WriteLine("Renderer: Renderer finished initialization.");
        }

        /// <summary>
        /// Renders the scene, abstract though so the renderer class needs to implement this
        /// </summary>
        public abstract void Render(Camera camera);

        /// <summary>
        /// Unload the renderer
        /// </summary>
        public abstract void Dispose();
    }
}
