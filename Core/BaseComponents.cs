﻿using HypoSharp.Core.Rendering;
using OpenTK.Mathematics;

namespace HypoSharp.Core
{
    /// <summary>
    /// Transform interface that actually holds the transform struct
    /// </summary>
    public interface ITransform
    {
        public Transform Transform { get; set; }
    }
    /// <summary>
    /// Transform data, holds position and rotation of the object in space
    /// </summary>
    public class Transform
    {
        //Current position of the transform
        public Vector3 Position { get; set; }
        //Current rotation of the transform
        private Quaternion _rotation;
        public Quaternion Rotation
        { 
            get { return _rotation; }
            set 
            {
                _rotation = value;
                Up = Vector3.Transform(Vector3.UnitY, _rotation);
                Left = Vector3.Transform(Vector3.UnitX, _rotation);
                Forward = Vector3.Transform(Vector3.UnitZ, _rotation);
            }
        }

        //The Up, Foward, and Left vectors of this transform
        public Vector3 Up { get; set; }
        public Vector3 Forward { get; set; }
        public Vector3 Left { get; set; }
    }

    public interface IEntity
    {
        /// <summary>
        /// Initialization method
        /// </summary>
        public virtual void Initialize(object entity)
        {
            if (entity is ITransform) 
            {
                ((ITransform)entity).Transform = new Transform();
            }
        }

        /// <summary>
        /// The Loop method is ran every frame, before rendering
        /// </summary>
        public void Loop();

        /// <summary>
        /// Called when this object is getting disposed of
        /// </summary>
        public void Dispose();
    }

    /// <summary>
    /// Component that let's you add the tick event to the specific object
    /// </summary>
    public interface ITickable
    {        
        /// <summary>
        /// Tick event, called 60 times a second
        /// </summary>
        public void Tick();
    }

    /// <summary>
    /// Renderable component, tells the renderer that it can render this object
    /// </summary>
    public interface IRenderable
    {
        /// <summary>
        /// Render this object
        /// </summary>
        public void Render(Camera camera);
    }
}
