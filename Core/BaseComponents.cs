using HypoSharp.Rendering;
using System;
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
        /// <summary>
        /// Constructor of this transform
        /// </summary>
        public Transform() 
        {
            Rotation = Quaternion.Identity;
        }

        // Current position of the transform
        private Vector3 position;
        public Vector3 Position { get { return position; } set { position = value; OnTransformUpdate?.Invoke(); } }
        // Current rotation of the transform
        private Quaternion _rotation;
        public Quaternion Rotation
        { 
            get { return _rotation; }
            set 
            {
                _rotation = value;
                Up = Vector3.Transform(-Vector3.UnitY, _rotation);
                Right = Vector3.Transform(Vector3.UnitX, _rotation);
                Forward = Vector3.Transform(Vector3.UnitZ, _rotation);
                OnTransformUpdate?.Invoke();
            }
        }
        // Current scale of the object
        public Vector3 Scale { get; set; }

        // The Up, Foward, and Left vectors of this transform
        public Vector3 Up { get; set; }
        public Vector3 Forward { get; set; }
        public Vector3 Right { get; set; }

        public event Action OnTransformUpdate;
    }

    public interface IEntity
    {
        /// <summary>
        /// Initialization method (Called only from world)
        /// </summary>
        public void InitializeAbstract(object entity)
        {
            Console.WriteLine($"IEntity: Setup entity {entity}");
            if (entity is ITransform)
            {
                ITransform transformEntity = ((ITransform)entity);
                transformEntity.Transform ??= new Transform();
            }
            Initialize();
        }

        /// <summary>
        /// Initialization method (The one that you need to implement)
        /// </summary>
        public void Initialize();

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
}
namespace HypoSharp.Rendering 
{
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
namespace HypoSharp.Editor 
{
    /// <summary>
    /// Editor component, can get called from inside the editor
    /// </summary>
    public interface IEditorEntity
    {
        /// <summary>
        /// Initialization method (The one that you need to implement)
        /// </summary>
        public void EditorInitialize();

        /// <summary>
        /// The Loop method is ran every frame, before rendering
        /// </summary>
        public void EditorLoop();

        /// <summary>
        /// Called when this object is getting disposed of
        /// </summary>
        public void EditorDispose();
    }
}
