using HypoSharp.Core.Rendering;
using OpenTK.Mathematics;

namespace HypoSharp.Core.Primitives
{
    /// <summary>
    /// A Quad shape
    /// </summary>
    public class Quad : Shape
    {
        // Main quad vars
        public Vector2 Size { get; set; }

        /// <summary>
        /// Quad shape constructor
        /// </summary>
        /// <param name="size">Size of the quad</param>
        public Quad(Vector2 size)
        {
            // Set size
            Size = size;
        }

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
                new Vector3(1, 0, 0),
                new Vector3(1, 0, 1),
                new Vector3(0, 0, 1),
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
