using OpenTK.Mathematics;
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
        void IEntity.Initialize(object entity) { }

        /// <summary>
        /// The Loop method is ran every frame, before rendering
        /// </summary>
        void IEntity.Loop() { }

        /// <summary>
        /// Render this object
        /// </summary>
        void IRenderable.Render(Camera camera)
        {
            Renderer.RenderModel(camera);
        }

        /// <summary>
        /// Called when this object is getting disposed of
        /// </summary>
        void IEntity.Dispose() { Renderer.DisposeModel(); }
    }
}
