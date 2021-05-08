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
    /// An object that is in the World, it has multiple events that are subscribed to raylib events
    /// </summary>
    public abstract class EngineObject
    {
        /// <summary>
        /// Initialization method
        /// </summary>
        public virtual void Initialize() 
        {        
        }

        /// <summary>
        /// The tick method is ran a predefined amount per second
        /// </summary>
        public virtual void Tick() 
        {
        }        

        /// <summary>
        /// The Loop method is ran every frame, before rendering
        /// </summary>
        /// <param name="delta">How much time passed since last frame</param>
        public virtual void Loop(float delta) 
        {
        }

        /// <summary>
        /// Called when this object is getting disposed of
        /// </summary>
        public virtual void Dispose()
        {
        }
    }
}
