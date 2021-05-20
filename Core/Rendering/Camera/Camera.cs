using HypoSharp.Core;
using System;
using System.Numerics;

namespace HypoSharp.Core.Rendering
{
    /// <summary>
    /// A Camera object
    /// </summary>
    public class Camera : ITransform, IEntity
    {
        //ITransform
        public Transform Transform { get; set; }

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

        /// <summary>
        /// Initialization method
        /// </summary>
        public void Initialize() { }

        /// <summary>
        /// The Loop method is ran every frame, before rendering
        /// </summary>
        public void Loop() { }

        /// <summary>
        /// Called when this object is getting disposed of
        /// </summary>
        public void Dispose() { }
    }
}
