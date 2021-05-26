using HypoSharp.Core.Rendering;
using OpenTK.Mathematics;

namespace HypoSharp.Core.Primitives
{
    /// <summary>
    /// A Cube shape
    /// </summary>
    public class Cube : Shape
    {
        // Main cube vars
        public Vector3 Size { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="size"></param>
        public Cube(Vector3 size)
        {
            // Set size
            Size = size;
        }
    }
}
