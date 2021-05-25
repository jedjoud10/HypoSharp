using HypoSharp.Core;
using HypoSharp.Core.Input;
using HypoSharp.Core.Primitives;
using HypoSharp.Core.Rendering;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;

namespace HypoSharp.Debug
{
    /// <summary>
    /// A camera control system that allows you to move in 3D with all freedom
    /// </summary>
    public class DebugCamera : Camera
    {
        // DebugCamera vars
        public const float defaultSpeed = 50f;
        public const float defaultSensivity = 0.04f;
        public float Speed { get; set; }
        public float Sensivity { get; set; }
        private Vector2 _mouseDelta;
        private Vector2 _lastMousePosition;
        private Vector2 _summedDelta;

        /// <summary>
        /// The Loop method is ran every frame, before rendering
        /// </summary>
        public override void Loop()
        {
            base.Loop();

            //Debug camera controls
            if (InputManager.IsKeyMappingHeld(Keys.Space))
            {
                World.Camera.Transform.Position += Vector3.UnitY * 0.01f;
                World.Camera.UpdateViewMatrix();
            }

            if (InputManager.IsKeyMappingHeld(Keys.LeftShift))
            {
                World.Camera.Transform.Position -= Vector3.UnitY * 0.01f;
                World.Camera.UpdateViewMatrix();
            }

            if (InputManager.IsKeyMappingHeld(Keys.W))
            {
                World.Camera.Transform.Position -= Vector3.UnitZ * 0.01f;
                World.Camera.UpdateViewMatrix();
            }

            if (InputManager.IsKeyMappingHeld(Keys.S))
            {
                World.Camera.Transform.Position += Vector3.UnitZ * 0.01f;
                World.Camera.UpdateViewMatrix();
            }

            if (InputManager.IsKeyMappingHeld(Keys.A))
            {
                World.Camera.Transform.Position -= Vector3.UnitX * 0.01f;
                World.Camera.UpdateViewMatrix();
            }

            if (InputManager.IsKeyMappingHeld(Keys.D))
            {
                World.Camera.Transform.Position += Vector3.UnitX * 0.01f;
                World.Camera.UpdateViewMatrix();
            }
        }
    }
}
