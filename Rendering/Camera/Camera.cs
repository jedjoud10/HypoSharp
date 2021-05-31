using HypoSharp.Core;
using System;
using OpenTK.Mathematics;

namespace HypoSharp.Rendering
{
    /// <summary>
    /// A Camera object
    /// </summary>
    public class Camera : ITransform, IEntity
    {
        // ITransform
        private Transform transform;
        public Transform Transform { get { return transform; } set { transform = value; UpdateViewMatrix(); } }
        public Matrix4 ViewMatrix { get; set; }
        public Matrix4 ProjectionMatrix { get; set; }

        // Main camera vars

        // Horizontal Fov
        private float hFov = 90f;
        public float HorizontalFov { get { return hFov; } set { hFov = value; UpdateProjectionMatrix(); } }
        public float VerticalFov 
        { 
            get { return 2.0f * MathF.Atan(MathF.Tan(HorizontalFov / 2.0f) * AspectRatio); } 
            set { HorizontalFov = 2.0f * MathF.Atan(MathF.Tan(value / 2.0f) * (1f/AspectRatio)); }
        }

        // Aspect ratio (Height / Width)
        public float AspectRatio { get; set; } = 0.75f;

        // Near-Far clip planes
        public float NearClipPlane { get; set; } = 0.3f;
        public float FarClipPlane { get; set; } = 1000f;

        /// <summary>
        /// Initialization method
        /// </summary>
        public virtual void Initialize() 
        {
            //Default aspect ratio
            UpdateViewMatrix();
            Window.OnWindowResize += OnWindowResize;
        }

        /// <summary>
        /// When the window gets resized
        /// </summary>
        private void OnWindowResize(float aspectRatio) 
        {
            AspectRatio = aspectRatio;
            UpdateProjectionMatrix();
            Console.WriteLine("Camera: Update Projection Matrix");
        }

        /// <summary>
        /// Update the view matrix when the transform changes
        /// </summary>
        public void UpdateViewMatrix() { ViewMatrix = Matrix4.LookAt(Transform.Position, Transform.Position + Transform.Forward, Transform.Up); }

        /// <summary>
        /// Update the projection matrix when the Fov or the AspectRatio changes
        /// </summary>
        public void UpdateProjectionMatrix() 
        { 
            ProjectionMatrix = Matrix4.CreatePerspectiveFieldOfView(VerticalFov, AspectRatio, NearClipPlane, FarClipPlane);
        }

        /// <summary>
        /// The Loop method is ran every frame, before rendering
        /// </summary>
        public virtual void Loop() { }

        /// <summary>
        /// Called when this object is getting disposed of
        /// </summary>
        public virtual void Dispose() { }
    }
}
