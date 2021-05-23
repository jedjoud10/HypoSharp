using OpenTK.Graphics.OpenGL;
using System.Numerics;

namespace HypoSharp.Core.Rendering
{
    /// <summary>
    /// Renders a single model
    /// </summary>
    public class ModelRenderer
    {
        //The mesh that this renderer points to
        private Model _model;
        public Model Model { get { return _model; } set { _model = value; RefreshModel(); } }
        //This mesh's VAO
        public int VAO { get; private set; }
        //This mesh's VBO list
        public int VertVBO { get; private set; }
        public int ColrVBO { get; private set; }
        //This renderer's shader
        public Shader Shader { get; set; } 

        /// <summary>
        /// When the model gets refreshed
        /// </summary>
        public void RefreshModel() 
        {
            //Generate the vertex VBO for this mesh
            VertVBO = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertVBO);
            GL.BufferData(BufferTarget.ArrayBuffer, _model.Vertices.Length * sizeof(float) * 3, _model.Vertices, BufferUsageHint.StaticDraw);

            //Generate the global VAO for this mesh
            VAO = GL.GenVertexArray();
            GL.BindVertexArray(VAO);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, sizeof(float) * 3, 0);
            GL.EnableVertexAttribArray(0);

            //Setup the shader 
            Shader = new Shader(@"#version 330 core
layout (location = 0) in vec3 aPosition;

void main()
{
    gl_Position = vec4(aPosition, 1.0);
}
", @"#version 330 core
out vec4 FragColor;

void main()
{
    FragColor = vec4(1.0f, 0.5f, 0.2f, 1.0f);
}", "DefaultDiffuse");
            Shader.Use();
        }

        /// <summary>
        /// Renders the specific model
        /// </summary>
        public void RenderModel()
        {
            //Bind the VAO, and make sure to unbind after rendering it
            Shader.Use();
            GL.BindVertexArray(VAO);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
            GL.BindVertexArray(0);
        }

        /// <summary>
        /// Disposes all the GPU buffers
        /// </summary>
        public void DisposeModel() 
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DeleteBuffer(VertVBO);
        }
    }
}
