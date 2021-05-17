using HypoSharp.Core;
using HypoSharp.Core.Primitives;
using HypoSharp.Core.Rendering;
using System;
using System.Numerics;

namespace HypoSharp.Debug
{
    /// <summary>
    /// A camera control system that allows you to move in 3D with all freedom
    /// </summary>
    public class DebugCamera : Camera
    {
        public const float defaultSpeed = 50f;
        public const float defaultSensivity = 0.04f;
        public float Speed { get; set; }
        public float Sensivity { get; set; }
        private Vector2 _mouseDelta;
        private Vector2 _lastMousePosition;
        private Vector2 _summedDelta;

        /// <summary>
        /// Debug camera constructor
        /// </summary>
        /// <param name="fov">Field of View (Horizontal) of this camera. Defaults to 90</param>
        /// <param name="speed">Speed of this debug camera. Defaults to 50</param>
        /// <param name="sensivity">The sensivity of the mouse when looking around. Defaults to 0.04</param>
        public DebugCamera(float fov = 60, float speed = defaultSpeed, float sensivity = defaultSensivity) : base(fov)
        {
            this.Speed = speed;
            this.Sensivity = sensivity;
        }

        /// <summary>
        /// The Loop method is ran every frame, before rendering
        /// </summary>
        public override void Loop()
        {
            //Movement
            Position += Vector3.Transform(Vector3.UnitZ, Rotation) * Time.DeltaTime * Speed;
            Position += Vector3.Transform(Vector3.UnitX, Rotation) * Time.DeltaTime * Speed;
            Position -= Vector3.Transform(Vector3.UnitZ, Rotation) * Time.DeltaTime * Speed;
            Position -= Vector3.Transform(Vector3.UnitX, Rotation) * Time.DeltaTime * Speed;

            //Rotation
            

            base.Loop();
        }
    }
}
