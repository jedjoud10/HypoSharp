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
    /// Transform component
    /// </summary>
    public interface ITransform
    {
        //Main vars
        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }        
    }

    /// <summary>
    /// Renderable component
    /// </summary>
    public interface IRenderable 
    {
        
    }
}
