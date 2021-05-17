using System.Numerics;

namespace HypoSharp.Core.Primitives
{
    /// <summary>
    /// A Cube shape
    /// </summary>
    public class Cube : Shape
    {
        //Main cube vars
        public Vector3 Size { get; set; }

        /// <summary>
        /// Cube shape constructor
        /// </summary>
        /// <param name="position">Position for this cube. Default is (0, 0, 0)</param>
        /// <param name="rotation">Rotation (Quaternion) for this cube. Default is (0, 0, 0, 0)</param>
        public Cube(Vector3 position, Quaternion rotation, Vector3 size) : base(position, rotation)
        {

        }

        /// <summary>
        /// Render this specific EngineEntity
        /// </summary>
        public override void Render()
        {
            base.Render();
            Renderer.RenderModel();
        }
    }
}
