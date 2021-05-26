using OpenTK.Mathematics;
using HypoSharp.Core.Rendering;

namespace HypoSharp.Core.Primitives
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
        public virtual void Dispose() { Renderer.DisposeModel(); }
    }
}
