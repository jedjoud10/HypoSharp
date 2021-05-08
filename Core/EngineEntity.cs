using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;

namespace HypoSharp.Core
{
    /// <summary>
    /// A 3D object in the world, could be a cube or even the player
    /// </summary>
    public abstract class EngineEntity : EngineObject
    {
        //Main vars
        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }

        /// <summary>
        /// Constructor for this entity
        /// </summary>
        /// <param name="position">Position for this entity. Default is (0, 0, 0)</param>
        /// <param name="rotation">Rotation (Quaternion) for this entity. Default is (0, 0, 0, 0)</param>
        public EngineEntity(Vector3 position = new Vector3(), Quaternion rotation = new Quaternion()) 
        {
            this.Position = position;
            this.Rotation = rotation;
        }

        /// <summary>
        /// Render the entity every frame
        /// </summary>
        public override void Loop(float delta)
        {
            base.Loop(delta);
            Render();
        }

        /// <summary>
        /// Render this specific EngineEntity
        /// </summary>
        public virtual void Render()
        {
        }

        
    }
}
