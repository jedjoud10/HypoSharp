using System.Numerics;
using HypoSharp.Core.Rendering;

namespace HypoSharp.Core.Primitives
{
    /// <summary>
    /// A Primitive shape
    /// </summary>
    public abstract class Shape : ITransform, IEntity, IRenderable
    {
        //ITransform transform
        public Transform Transform { get; set; }


        //Main shape vars
        public Model Model { get; set; }
        public ModelRenderer Renderer { get; set; }

        /// <summary>
        /// Primitive shape constructor
        /// </summary>
        /// <param name="position">Position for this entity. Default is (0, 0, 0)</param>
        /// <param name="rotation">Rotation (Quaternion) for this entity. Default is (0, 0, 0, 0)</param>
        public Shape(Vector3 position, Quaternion rotation)
        {
            Transform.Position = position;
            Transform.Rotation = rotation;
            Renderer = new ModelRenderer()
            {
                Model = Model,
            };
        }

        /// <summary>
        /// Initialization method
        /// </summary>
        public virtual void Initialize() { }

        /// <summary>
        /// The Loop method is ran every frame, before rendering
        /// </summary>
        public virtual void Loop() { }

        /// <summary>
        /// Render this object
        /// </summary>
        public virtual void Render() { }

        /// <summary>
        /// Tick event, called 60 times a second
        /// </summary>

        public virtual void Tick() { }

        /// <summary>
        /// Called when this object is getting disposed of
        /// </summary>
        public virtual void Dispose() { Renderer.DisposeModel(); }
    }
}
