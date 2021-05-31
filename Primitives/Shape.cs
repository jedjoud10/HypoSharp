using HypoSharp.Core;
using HypoSharp.Rendering;
using OpenTK.Mathematics;

namespace HypoSharp.Primitives
{
    /// <summary>
    /// A Primitive shape
    /// </summary>
    public abstract class Shape : ITransform, IEntity, IRenderable
    {
        // ITransform transform
        public Transform Transform { get; set; }

        // Main shape vars
        public Model Model { get; set; }
        public ModelRenderer Renderer { get; set; }

        /// <summary>
        /// Initialization method
        /// </summary>
        public virtual void Initialize() 
        {
            Transform.OnTransformUpdate += () => Renderer.RecalculateModelMatrix(Transform);
        }

        /// <summary>
        /// Initialize anything render related to this object
        /// </summary>
        public virtual void RenderInitialize()
        {
            Renderer = new ModelRenderer()
            {
                Model = Model,
            };
        }

        /// <summary>
        /// The Loop method is ran every frame, before rendering
        /// </summary>
        public virtual void Loop() { }

        /// <summary>
        /// Render this object
        /// </summary>
        public virtual void Render(Camera camera) { Renderer.RenderModel(camera); }

        /// <summary>
        /// Called when this object is getting disposed of
        /// </summary>
        public virtual void Dispose() { }

        /// <summary>
        /// Dipose of anything related to this object
        /// </summary>
        public virtual void RenderDispose() { Renderer.DisposeModel(); }

    }
}
