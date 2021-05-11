using Raylib_cs;
using System.Numerics;

namespace HypoSharp.Core.Primitives
{
    /// <summary>
    /// A Primitive shape
    /// </summary>
    public abstract class Shape : ITransform, IRenderable
    {
        //ITransform implementations
        private Quaternion _rotation; private Vector3 _position;
        public Vector3 Position { get { return _position; } set { _position = value; Renderer.Position = _position; } }
        public Quaternion Rotation { get { return _rotation; } set { _rotation = value; Renderer.Rotation = _rotation; } }

        //Main shape vars
        public ModelRenderer Renderer { get; set; }

        /// <summary>
        /// Primitive shape constructor
        /// </summary>
        /// <param name="position">Position for this entity. Default is (0, 0, 0)</param>
        /// <param name="rotation">Rotation (Quaternion) for this entity. Default is (0, 0, 0, 0)</param>
        /// <param name="color">Color of the shape</param>
        public Shape(Vector3 position, Quaternion rotation, Color color)
        {
            Renderer = new ModelRenderer();
            Position = position;
            Rotation = rotation;
            Renderer.Tint = color;
            Renderer.Scale = 1;
        }

        /// <summary>
        /// Render this specific EngineEntity
        /// </summary>
        public virtual void Render()
        {
            Renderer.RenderModel();
        }
    }
}
