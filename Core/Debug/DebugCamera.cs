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
        /// Initialization method
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
            InputManager.AddKeyMapping("cameraDebugUp", Keys.Space);
            InputManager.AddKeyMapping("cameraDebugDown", Keys.LeftShift);
            InputManager.AddKeyMapping("cameraDebugForward", Keys.W);
            InputManager.AddKeyMapping("cameraDebugBackward", Keys.S);
            InputManager.AddKeyMapping("cameraDebugLeft", Keys.A);
            InputManager.AddKeyMapping("cameraDebugRight", Keys.D);
        }

        /// <summary>
        /// The Loop method is ran every frame, before rendering
        /// </summary>
        public override void Loop()
        {
            base.Loop();

            //Debug camera controls
            if (InputManager.IsKeyMappingHeld("cameraDebugUp"))
            {
                World.Camera.Transform.Position += Vector3.UnitY * 0.01f;
                World.Camera.UpdateViewMatrix();
            }

            if (InputManager.IsKeyMappingHeld("cameraDebugDown"))
            {
                World.Camera.Transform.Position -= Vector3.UnitY * 0.01f;
                World.Camera.UpdateViewMatrix();
            }

            if (InputManager.IsKeyMappingHeld("cameraDebugForward"))
            {
                World.Camera.Transform.Position -= Vector3.UnitZ * 0.01f;
                World.Camera.UpdateViewMatrix();
            }

            if (InputManager.IsKeyMappingHeld("cameraDebugBackward"))
            {
                World.Camera.Transform.Position += Vector3.UnitZ * 0.01f;
                World.Camera.UpdateViewMatrix();
            }

            if (InputManager.IsKeyMappingHeld("cameraDebugLeft"))
            {
                World.Camera.Transform.Position -= Vector3.UnitX * 0.01f;
                World.Camera.UpdateViewMatrix();
            }

            if (InputManager.IsKeyMappingHeld("cameraDebugRight"))
            {
                World.Camera.Transform.Position += Vector3.UnitX * 0.01f;
                World.Camera.UpdateViewMatrix();
            }
        }
    }
}
