using System.Numerics;

namespace HypoSharp.Core
{
    /// <summary>
    /// Transform component, holds position and rotation of the object in space
    /// </summary>
    public interface ITransform
    {
        //Main vars
        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }
    }

    public interface IGameLogic
    {
        /// <summary>
        /// Initialization method
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
        /// Tick event
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
        public void Render();
    }
}
