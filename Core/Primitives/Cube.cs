using Raylib_cs;
using System.Numerics;

namespace HypoSharp.Core.Primitives
{
    /// <summary>
    /// A Cube shape
    /// </summary>
    public class Cube : Shape
    {
        //Main cube vars
        private Vector3 _size;
        public Vector3 Size
        {
            get { return _size; }
            set
            {
                _size = value;

                //Update mesh
                Renderer.Model = Raylib.LoadModelFromMesh(Raylib.GenMeshCube(_size.X, _size.Y, _size.Z));
            }
        }

        /// <summary>
        /// Cube shape constructor
        /// </summary>
        /// <param name="position">Position for this cube. Default is (0, 0, 0)</param>
        /// <param name="rotation">Rotation (Quaternion) for this cube. Default is (0, 0, 0, 0)</param>
        /// <param name="color">Color of this cube</param>
        public Cube(Vector3 position, Quaternion rotation, Color color, Vector3 size) : base(position, rotation, color)
        {
            Renderer.Model = Raylib.LoadModelFromMesh(Raylib.GenMeshCube(size.X, size.Y, size.Z));
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
