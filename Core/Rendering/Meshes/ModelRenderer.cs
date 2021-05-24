using OpenTK.Graphics.OpenGL;

namespace HypoSharp.Core.Rendering
{
    /// <summary>
    /// Renders a single model
    /// </summary>
    public class ModelRenderer
    {
        //The model that this renderer, well, renders
        private Model model;
        public Model Model { get { return model; } set { model = value; RefreshModel(); } }
        //This model's VAO
        public int VAO { get; private set; }
        //This model's VBO list
        public int VertVBO { get; private set; }
        public int ColrVBO { get; private set; }
        //This model's EBO
        public int EBO { get; private set; }
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
            GL.BufferData(BufferTarget.ArrayBuffer, model.Vertices.Length * sizeof(float) * 3, model.Vertices, BufferUsageHint.StaticDraw);

            //Generate the global VAO for this model
            VAO = GL.GenVertexArray();
            GL.BindVertexArray(VAO);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, sizeof(float) * 3, 0);
            GL.EnableVertexAttribArray(0);

            //Generate the global EBO for this mesh
            EBO = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);
            GL.BufferData(BufferTarget.ElementArrayBuffer, model.Indices.Length * sizeof(uint), model.Indices, BufferUsageHint.StaticDraw);

            //Setup the shader 
            Shader = new Shader(@"#version 330 core
layout (location = 0) in vec3 aPosition;
out vec3 color;
void main()
{
    gl_Position = vec4(aPosition, 1.0);
    color = aPosition;
}
", @"#version 330 core
out vec4 FragColor;
in vec3 color;
uniform float test;

void main()
{
    FragColor = vec4(color, 1.0);
}", "DefaultDiffuse");
            Shader.Use();
        }

        /// <summary>
        /// Renders the specific model
        /// </summary>
        public void RenderModel(Camera camera)
        {
            //Bind the VAO, and make sure to unbind after rendering it
            //Shader.SetMatrix4(Shader.GetAttribLocation("viewMatrix"), camera.ViewMatrix);
            Shader.SetFloat(Shader.GetAttribLocation("test"), 0.5f);
            Shader.Use();
            GL.BindVertexArray(VAO);
            GL.DrawElements(PrimitiveType.Triangles, model.Indices.Length, DrawElementsType.UnsignedInt, 0);
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
