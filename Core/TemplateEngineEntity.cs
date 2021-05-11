using System.Numerics;

namespace HypoSharp.Core
{
    public class TemplateEngineEntity : IGameLogic, ITransform, IRenderable, ITickable
    {
        //ITransform implementations
        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }

        /// <summary>
        /// Initialization method
        /// </summary>
        public void Initialize()
        {

        }

        /// <summary>
        /// The tick method is ran a predefined amount per second
        /// </summary>
        public void Tick()
        {

        }

        /// <summary>
        /// The main game Loop called before rendering this EngineEntity
        /// </summary>
        public void Loop(float delta)
        {

        }

        /// <summary>
        /// Render this object
        /// </summary>
        public void Render()
        {

        }

        /// <summary>
        /// Called when this object is getting disposed of
        /// </summary>
        public void Dispose()
        {

        }
    }
}
