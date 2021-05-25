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
        // ITransform
        private Transform transform;
        public Transform Transform { get { return transform; } set { transform = value; UpdateViewMatrix(); } }
        public Matrix4 ViewMatrix { get; set; }
        public Matrix4 ProjectionMatrix { get; set; }

        // Main camera vars

        // Vertrical Fov
        private float fov = 0.78f;
        public float FovY { get { return fov; } set { fov = value; UpdateProjectionMatrix(); } }

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
            UpdateProjectionMatrix();
            UpdateViewMatrix();
            World.OnWindowResize += OnWindowResize;
        }

        /// <summary>
        /// When the window gets resized
        /// </summary>
        public void OnWindowResize() 
        {
            AspectRatio = World.AspectRatio;
            UpdateProjectionMatrix();
        }

        /// <summary>
        /// Update the view matrix when the transform changes
        /// </summary>
        public void UpdateViewMatrix() { ViewMatrix = Matrix4.CreateFromQuaternion(Transform.Rotation) * Matrix4.CreateTranslation(-Transform.Position); }

        /// <summary>
        /// Update the projection matrix when the Fov or the AspectRatio changes
        /// </summary>
        public void UpdateProjectionMatrix() { ProjectionMatrix = Matrix4.CreatePerspectiveFieldOfView(FovY, AspectRatio, NearClipPlane, FarClipPlane); }

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
