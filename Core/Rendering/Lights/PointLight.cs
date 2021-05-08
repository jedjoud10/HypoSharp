using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;

namespace HypoSharp.Core.Rendering
{    
    /// <summary>
    /// A point light used in rendering
    /// </summary>
    public class PointLight : EngineObject
    {
        /// <summary>
        /// Initialization method
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// The main game Loop called before rendering this EngineEntity
        /// </summary>
        public override void Loop(float delta)
        {
            base.Loop(delta);
            
        }

        /// <summary>
        /// Called when this object is getting disposed of
        /// </summary>
        public override void Dispose()
        {
            base.Dispose();
        }
    }    
}
