using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HypoSharp.Core.Primitives
{
    /// <summary>
    /// A Primitive shape
    /// </summary>
    public abstract class Shape : EngineEntity
    {
        //Main shape vars
        public ModelRenderer Renderer { get; set; }
        public Quaternion Rotation 
        { 
            get 
            {
                return base.Rotation;
            }
            set 
            {
                base.Rotation = value;
                Renderer.Rotation = base.Rotation;
            } 
        } 

        /// <summary>
        /// Primitive shape constructor
        /// </summary>
        /// <param name="position">Position for this entity. Default is (0, 0, 0)</param>
        /// <param name="rotation">Rotation (Quaternion) for this entity. Default is (0, 0, 0, 0)</param>
        /// <param name="color">Color of the shape</param>
        public Shape(Vector3 position, Quaternion rotation, Color color) : base(position, rotation)
        {
        
        }

        /// <summary>
        /// The main game Loop called before rendering this EngineEntity
        /// </summary>
        public override void Loop(float delta)
        {
            base.Loop(delta);
        }

        /// <summary>
        /// Render this specific EngineEntity
        /// </summary>
        public override void Render()
        {
            base.Render();            
        }
    }
}
