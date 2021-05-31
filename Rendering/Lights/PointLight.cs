using HypoSharp.Core;

namespace HypoSharp.Rendering
{
    /// <summary>
    /// A point light used in rendering
    /// </summary>
    public class PointLight : ITransform, IRenderable
    {
        //ITransform transform
        public Transform Transform { get; set; }

        /// <summary>
        /// Initialize anything render related to this object
        /// </summary>
        public void RenderInitialize() { }

        /// <summary>
        /// Render this object
        /// </summary>
        public void Render(Camera camera) { }

        /// <summary>
        /// Dipose of anything related to this object
        /// </summary>
        public void RenderDispose() { }
    }
}
