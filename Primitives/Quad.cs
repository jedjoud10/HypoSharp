using HypoSharp.Rendering;
using OpenTK.Mathematics;

namespace HypoSharp.Primitives
{
    /// <summary>
    /// A Quad shape
    /// </summary>
    public class Quad : Shape
    {
        /// <summary>
        /// Initialization method
        /// </summary>
        public override void Initialize() 
        {
            // Quad
            Model = new Model();
            Model.Vertices = new Vector3[]
            {
                new Vector3(0, 0, 0),
                new Vector3(Transform.Scale.X, 0, 0),
                new Vector3(Transform.Scale.X, 0, Transform.Scale.Z),
                new Vector3(0, 0, Transform.Scale.Z),
            };
            Model.Indices = new uint[]
            {
                0, 1, 2,
                2, 3, 0,
            };
            base.Initialize();
        }
    }
}
