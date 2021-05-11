﻿using Raylib_cs;
using System.Numerics;

namespace HypoSharp.Core
{
    /// <summary>
    /// A ModelRenderer that renders a single model
    /// </summary>
    public class ModelRenderer
    {
        //Main Model vars
        private Model _model;
        public Model Model { get { return _model; } set { _model = value; _model.transform = Transform; } }
        public Vector3 Position { get; set; }
        public Matrix4x4 Transform { get; set; }
        private Quaternion _rotation;
        public Quaternion Rotation
        {
            get { return _rotation; }
            set
            {
                _rotation = value;
                Transform = Matrix4x4.CreateFromQuaternion(_rotation);
            }
        }
        public float Scale { get; set; }
        public Color Tint { get; set; }
        public Shader Shader { get; set; }

        /// <summary>
        /// Renders a specific model with a position, rotation and scale (Tint is optional)
        /// </summary>
        public void RenderModel()
        {
            Raylib.DrawModel(Model, Position, Scale, Tint);
        }
    }
}
