using HypoSharp.Core.Rendering;
using System.Numerics;

namespace HypoSharp.Core
{
    public class TemplateEngineEntity : IEntity, ITransform, IRenderable, ITickable
    {

        //ITransform transform
        public Transform Transform { get; set; }

        /// <summary>
        /// Initialization method
        /// </summary>
        public virtual void Initialize() { }

        /// <summary>
        /// The Loop method is ran every frame, before rendering
        /// </summary>
        public virtual void Loop() { }

        /// <summary>
        /// Render this object
        /// </summary>
        public virtual void Render(Camera camera) { }

        /// <summary>
        /// Tick event, called 60 times a second
        /// </summary>

        public virtual void Tick() { }

        /// <summary>
        /// Called when this object is getting disposed of
        /// </summary>
        public virtual void Dispose() { }
    }
}
