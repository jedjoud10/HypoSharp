﻿using HypoSharp.Core;
using System;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace HypoSharp.Rendering
{
    /// <summary>
    /// Renders a single model
    /// </summary>
    public class ModelRenderer
    {
        // The model that this renderer, well, renders
        private Model model;
        public Model Model { get { return model; } set { model = value; RefreshModel(); } }
        // This model's VAO
        public int VAO { get; private set; }
        // This model's VBO list
        public int VertVBO { get; private set; }
        public int ColorVBO { get; private set; }
        // This model's EBO
        public int EBO { get; private set; }
        // This renderer's shader
        public Shader Shader { get; set; } 
        // This model's MoodelMatrix
        public Matrix4 ModelMatrix { get; set; } = Matrix4.Identity;

        /// <summary>
        /// Recalculates the model matrix from a transform
        /// </summary>
        /// <param name="transform"></param>
        public void RecalculateModelMatrix(Transform transform) 
        {
            ModelMatrix = Matrix4.CreateScale(transform.Scale) * Matrix4.CreateTranslation(transform.Position) * Matrix4.CreateFromQuaternion(transform.Rotation);
        }

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
            Shader = new Shader(@"
#version 330 core
layout (location = 0) in vec3 pos;
uniform mat4 projection;
uniform mat4 view;
uniform mat4 model;
out vec3 vertex;
void main()
{
    vec4 worldPos = model * vec4(pos, 1.0);
    gl_Position = projection * view * worldPos;
    vertex = worldPos.xyz;
}
", @"
#version 330 core
out vec4 fragColor;
in vec3 vertex;
void main()
{
    float value = fract((floor(vertex.x * 10) + floor(vertex.z * 10)) * 0.5) * 2.0;
    float value2 = fract((floor(vertex.x) + floor(vertex.z)) * 0.5) * 2.0;
    value *= value2;
    fragColor = vec4(value, value, value, 1.0);
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
            Shader.Use();
            Shader.SetMatrix4(Shader.GetUniformLocation("projection"), camera.ProjectionMatrix);
            Shader.SetMatrix4(Shader.GetUniformLocation("view"), camera.ViewMatrix);
            Shader.SetMatrix4(Shader.GetUniformLocation("model"), ModelMatrix);
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
