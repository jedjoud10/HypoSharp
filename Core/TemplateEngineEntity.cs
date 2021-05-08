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
    /// A EngineEntity template
    /// </summary>
    public class TemplateEngineEntity : EngineEntity
    {
        /// <summary>
        /// Render the entity every frame
        /// </summary>
        public override void Frame(float delta)
        {
            base.Frame(delta);
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
