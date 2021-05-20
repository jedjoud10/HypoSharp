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
        public void Initialize() { }

        /// <summary>
        /// The Loop method is ran every frame, before rendering
        /// </summary>
        public void Loop() { }

        /// <summary>
        /// Render this object
        /// </summary>
        public void Render() { }

        /// <summary>
        /// Tick event, called 60 times a second
        /// </summary>

        public void Tick() { }

        /// <summary>
        /// Called when this object is getting disposed of
        /// </summary>
        public void Dispose() { }
    }
}
