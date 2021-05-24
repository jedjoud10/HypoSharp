using HypoSharp.Core;
using System;
using OpenTK.Mathematics;

namespace HypoSharp.Core.Rendering
{
    /// <summary>
    /// A Camera object
    /// </summary>
    public class Camera : ITransform, IEntity
    {
        //ITransform
        public Transform Transform { get; set; }
        public Matrix4 ViewMatrix { get; set; }
        public Matrix4 ProjectionMatrix { get; set; }

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
        void IEntity.Initialize(object entity) { }

        /// <summary>
        /// The Loop method is ran every frame, before rendering
        /// </summary>
        void IEntity.Loop() 
        {
            ViewMatrix = Matrix4.CreateTranslation(Transform.Position);
        }

        /// <summary>
        /// Called when this object is getting disposed of
        /// </summary>
        void IEntity.Dispose() { }
    }
}
