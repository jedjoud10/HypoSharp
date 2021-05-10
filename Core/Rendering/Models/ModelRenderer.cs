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
    /// A ModelRenderer that renders a single model
    /// </summary>
    public class ModelRenderer
    {
        //Properities
        public Model Model { get { return model; } set { model = value; } }
        public Vector3 Position { get; set; }
        public Matrix4x4 Transform { get; set; }
        public Quaternion Rotation 
        {
            get { return rotation; }
            set 
            {
                rotation = value;
                Transform = Matrix4x4.CreateFromQuaternion(rotation);
                model.transform = Transform;
            }
        }
        public float Scale { get; set; }
        public Color Tint { get; set; }
        public Shader Shader { get; set; }

        //Fields
        private Quaternion rotation;
        private Model model;

        /// <summary>
        /// Renders a specific model with a position, rotation and scale (Tint is optional)
        /// </summary>
        /// 
        public void RenderModel()
        {
            Raylib.BeginTextureMode
            Raylib.DrawModel(Model, Position, Scale, Tint);
        }
    }    
}
