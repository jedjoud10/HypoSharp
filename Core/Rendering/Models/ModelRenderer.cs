using Raylib_cs;
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
        public Model Model { get { return _model; } set { _model = value; _model.transform = Transform; OnLoadModel(); } }
        public Vector3 Position { get; set; }
        private Matrix4x4 _transform;
        public Matrix4x4 Transform { get { return _transform; } set { _transform = value; _model.transform = value; } }
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
        /// Load a specific model
        /// </summary>
        private unsafe void OnLoadModel() 
        {
            Shader shader = DeferredRenderer.Shader;
            DeferredRenderer.SetMaterialShader(ref _model, 0, ref shader);
        }

        /// <summary>
        /// Renders a specific model with a position, rotation and scale (Tint is optional)
        /// </summary>
        public void RenderModel()
        {            
            Raylib.DrawModel(Model, Position, Scale, Tint);
        }

        /// <summary>
        /// Disposes a specific model
        /// </summary>
        public void DisposeModel() 
        {
            Raylib.UnloadModel(Model);
        }
    }
}
