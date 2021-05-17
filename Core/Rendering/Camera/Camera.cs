using HypoSharp.Core;
using System;
using System.Numerics;

namespace HypoSharp.Core.Rendering
{
    /// <summary>
    /// A Camera object
    /// </summary>
    public class Camera : IGameLogic, ITransform
    {
        //ITransform implementations
        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }

        //Main camera vars
        private float _fov;
        public Vector3 Up { get; set; }
        public Vector3 Forward { get; set; }
        public Vector3 Left { get; set; }

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
        public void Initialize()
        {
            Position = new Vector3(0, 30, -100);
        }

        /// <summary>
        /// Begin drawing to the render textures
        /// </summary>
        public virtual void Draw()
        {
            DeferredRenderer.Render();
        }

        /// <summary>
        /// The Loop method is ran every frame, before rendering
        /// </summary>
        public virtual void Loop()
        {
            Up = Vector3.Transform(Vector3.UnitY, Rotation);
            Forward = Vector3.Transform(Vector3.UnitZ, Rotation);
            Left = Vector3.Transform(Vector3.UnitX, Rotation);
        }

        /// <summary>
        /// Called when this object is getting disposed of
        /// </summary>
        public void Dispose()
        {
        }
    }
}
