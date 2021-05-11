using System.Numerics;

namespace HypoSharp.Core.Rendering
{
    /// <summary>
    /// A point light used in rendering
    /// </summary>
    public class PointLight : ITransform
    {
        //ITransform implementations
        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }
    }
}
