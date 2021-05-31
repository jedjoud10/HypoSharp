using HypoSharp.Core;
using HypoSharp.Editor.Input;
using HypoSharp.Rendering;
using System;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace HypoSharp.Editor
{
    /// <summary>
    /// A camera control system that allows you to move in 3D with all freedom
    /// </summary>
    public class EditorCamera : Camera, IEditorEntity
    {
        // Speed of the camera
        public float Speed { get; set; } = 10f;
        // Sensivity of the camera rotation
        public float Sensivity { get; set; } = 0.1f;
        // How much the mouse position changed since last frame
        private Vector2 mouseDelta;
        // The rotation of the camera (X is pitch and Y is yaw)
        private Vector2 rotation;
        // The velocity of the camera (Used for smoothing)
        private Vector3 velocity;
        // The direction the camera's going to
        private Vector3 direction;

        /// <summary>
        /// Initialize the editor camera
        /// </summary>
        public void EditorInitialize()
        {
            base.Initialize();
            Transform.OnTransformUpdate += () => UpdateViewMatrix();
        }

        /// <summary>
        /// The Loop method is ran every frame, before rendering, in Editor
        /// </summary>
        public void EditorLoop()
        {

            if (EditorInputManager.MouseState.IsButtonDown(MouseButton.Right))
            {
                Core.Window.Singleton.CursorGrabbed = true;
                // Read the mouse delta from last frame
                mouseDelta = EditorInputManager.MouseDelta;
                // Sum up the mouse delta to get the rotation (As a 2D vector)
                rotation += mouseDelta * Sensivity;
                // Clamp the rotation so we can't break our neck
                rotation.Y = MathHelper.Clamp(rotation.Y, -90, 90);
                // Apply the camera rotation
                Transform.Rotation = Quaternion.FromAxisAngle(Vector3.UnitY, MathHelper.DegreesToRadians(rotation.X)) * Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(-rotation.Y));
                UpdateViewMatrix();

                // Up and down
                if (EditorInputManager.KeyboardState.IsKeyDown(Keys.Space))
                    direction += Transform.Up * Time.EditorDeltaTime * Speed;

                if (EditorInputManager.KeyboardState.IsKeyDown(Keys.LeftShift))
                    direction -= Transform.Up * Time.EditorDeltaTime * Speed;

                // Forward and backwards
                if (EditorInputManager.KeyboardState.IsKeyDown(Keys.W))
                    direction += Transform.Forward * Time.EditorDeltaTime * Speed;

                if (EditorInputManager.KeyboardState.IsKeyDown(Keys.S))
                    direction -= Transform.Forward * Time.EditorDeltaTime * Speed;

                // Left and right
                if (EditorInputManager.KeyboardState.IsKeyDown(Keys.A))
                    direction -= Transform.Right * Time.EditorDeltaTime * Speed;

                if (EditorInputManager.KeyboardState.IsKeyDown(Keys.D))
                    direction += Transform.Right * Time.EditorDeltaTime * Speed;

            }
            else 
            {
                Core.Window.Singleton.CursorVisible = true;
                Core.Window.Singleton.CursorGrabbed = false; 
            }
            // Smoothing of the velocity
            velocity = Vector3.Lerp(velocity, direction, Time.EditorDeltaTime * 10);
            // Apply the velocity and reset the direction for the next frame
            Transform.Position += velocity;
            direction = Vector3.Zero;
        }

        /// <summary>
        /// Called when this editor camera is getting disposed of
        /// </summary>
        public void EditorDispose() { }
    }
}
