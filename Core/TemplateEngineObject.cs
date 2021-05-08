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
    /// A EngineObject template
    /// </summary>
    public class TemplateEngineObject : EngineObject
    {
        /// <summary>
        /// Initialization method
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// The frame method is ran every frame
        /// </summary>
        public override void Frame(float delta)
        {
            base.Frame(delta);
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
