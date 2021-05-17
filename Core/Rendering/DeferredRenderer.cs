using HypoSharp.Core;
using HypoSharp.Core.Primitives;
using HypoSharp.Core.Rendering;
using System;
using System.Numerics;

namespace HypoSharp.Core.Rendering
{
    /// <summary>
    /// The renderer
    /// </summary>
    public static class DeferredRenderer
    {
        /// <summary>
        /// Renders the scene (Deffered lighting)
        /// </summary>
        public static void Render()
        {
            //Call the render method each IRenderable object
            foreach (var renderableObject in World.RenderObjects) renderableObject.Render();
        }

        /// <summary>
        /// Unload this deferred renderer
        /// </summary>
        public static void Dispose() 
        {
        }
    }
}
