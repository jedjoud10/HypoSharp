using System.Numerics;
using HypoSharp.Core.Rendering;

namespace HypoSharp.Core.Primitives
{
    /// <summary>
    /// A Primitive shape
    /// </summary>
    public abstract class Shape : ITransform, IGameLogic, IRenderable
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
        public Shape(Vector3 position, Quaternion rotation)
        {
            Renderer = new ModelRenderer();
            Position = position;
            Rotation = rotation;
        }

        /// <summary>
        /// Render this specific EngineEntity
        /// </summary>
        public virtual void Render()
        {
            Renderer.RenderModel();
        }

        public void Initialize()
        {
        }

        public void Loop()
        {
            Rotation = Quaternion.CreateFromYawPitchRoll(Time.TimeSinceGameStart, 0, 0);
        }

        public void Dispose()
        {
            Renderer.DisposeModel();
        }
    }
}
