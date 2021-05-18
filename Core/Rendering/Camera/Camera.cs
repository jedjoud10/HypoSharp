using HypoSharp.Core;
using System;
using System.Numerics;

namespace HypoSharp.Core.Rendering
{
    /// <summary>
    /// A Camera object
    /// </summary>
    public class Camera : ITransform
    {
        //ITransform implementations
        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }

        //Main camera vars
        private float _fov;

        /// <summary>
        /// Camera constructor
        /// </summary>
        /// <param name="fov">Field of View (Horizontal) of this camera. Defaults to 90</param>
        public Camera(float fov = 60)
        {
            this._fov = fov;
        }
    }
}
