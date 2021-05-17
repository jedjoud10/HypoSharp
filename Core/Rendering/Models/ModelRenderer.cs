using System.Numerics;

namespace HypoSharp.Core.Rendering
{
    /// <summary>
    /// A ModelRenderer that renders a single model
    /// </summary>
    public class ModelRenderer
    {
        //Main Model vars
        public Vector3 Position { get; set; }
        public Vector3 Scale { get; set; }
        public Quaternion Rotation { get; set; }

        /// <summary>
        /// When we load a specific model
        /// </summary>
        private unsafe void OnLoadModel() 
        {

        }

        /// <summary>
        /// Renders a specific model with a position, rotation and scale (Tint is optional)
        /// </summary>
        public void RenderModel()
        {         
            
        }

        /// <summary>
        /// Disposes a specific model
        /// </summary>
        public void DisposeModel() 
        {

        }
    }
}
