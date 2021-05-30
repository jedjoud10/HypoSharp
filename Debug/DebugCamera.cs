using HypoSharp.Core;
using HypoSharp.Input;
using HypoSharp.Rendering;
using System;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace HypoSharp.Debug
{
    /// <summary>
    /// A camera control system that allows you to move in 3D with all freedom
    /// </summary>
    public class DebugCamera : Camera
    {
        // Speed of the camera
        public float Speed { get; set; } = 10f;
        // Sensivity ov the camera rotation
        public float Sensivity { get; set; } = 0.1f;
        private Vector2 mouseDelta, rotation;

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
            InputManager.AddValueMapping("mouseDelta", ValueMapping.MOUSE_DELTA);
            InputManager.AddValueMapping("mousePosition", ValueMapping.MOUSE_POSITION);

            Transform.OnTransformUpdate += () => UpdateViewMatrix();
        }

        /// <summary>
        /// The Loop method is ran every frame, before rendering
        /// </summary>
        public override void Loop()
        {
            base.Loop();

            // Read the mouse delta from last frame
            mouseDelta = (Vector2)InputManager.ReadValueMapping("mouseDelta");
            // Sum up the mouse delta to get the rotation (As a 2D vector)
            rotation += mouseDelta * Sensivity;
            // Clamp the rotation so we can't break our neck
            rotation.Y = MathHelper.Clamp(rotation.Y, -90, 90);
            // Apply the camera rotation
            Transform.Rotation = Quaternion.FromAxisAngle(Vector3.UnitY, MathHelper.DegreesToRadians(rotation.X)) * Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(-rotation.Y));
            UpdateViewMatrix();
            // Debug camera controls
            if (InputManager.IsButtonMappingHeld("cameraDebugUp"))
                World.Camera.Transform.Position += Transform.Up * Time.DeltaTime * Speed;

            if (InputManager.IsButtonMappingHeld("cameraDebugDown"))
                World.Camera.Transform.Position -= Transform.Up * Time.DeltaTime * Speed;

            if (InputManager.IsButtonMappingHeld("cameraDebugForward"))
                World.Camera.Transform.Position += Transform.Forward * Time.DeltaTime * Speed;

            if (InputManager.IsButtonMappingHeld("cameraDebugBackward"))
                World.Camera.Transform.Position -= Transform.Forward * Time.DeltaTime * Speed;

            if (InputManager.IsButtonMappingHeld("cameraDebugLeft"))
                World.Camera.Transform.Position -= Transform.Right * Time.DeltaTime * Speed;            

            if (InputManager.IsButtonMappingHeld("cameraDebugRight"))
                World.Camera.Transform.Position += Transform.Right * Time.DeltaTime * Speed;
        }
    }
}
