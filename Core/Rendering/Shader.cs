using OpenTK.Graphics.OpenGL;
using System;
using System.IO;
using System.Numerics;
using System.Text;
using OpenTK.Mathematics;
using Vector4 = System.Numerics.Vector4;
using Vector3 = System.Numerics.Vector3;
using Vector2 = System.Numerics.Vector2;

namespace HypoSharp.Core.Rendering
{
    /// <summary>
    /// The shader handler that compiles the shaders at runtime
    /// </summary>
    public class Shader
    {
        private int handle;
        public string Name { get; set; }

        /// <summary>
        /// Shader constructor
        /// </summary>
        /// <param name="vertexPath">Path of the vertex shader</param>
        /// <param name="fragmentPath">Path of the fragment shader</param>
        public Shader(string vertShaderSource, string fragShaderSource, string name)
        {
            // Init shaders variables
            Name = name;
            int vertShader, fragShader;

            Console.WriteLine($"Shaders: Compiling shader {Name}...");

            // Generate the shaders
            vertShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertShader, vertShaderSource);

            fragShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragShader, fragShaderSource);

            // Check for errors
            GL.CompileShader(vertShader);
            string infoLogVert = GL.GetShaderInfoLog(vertShader);
            if (infoLogVert != string.Empty) System.Console.WriteLine(infoLogVert);

            GL.CompileShader(fragShader);
            string infoLogFrag = GL.GetShaderInfoLog(fragShader);
            if (infoLogFrag != string.Empty) System.Console.WriteLine(infoLogFrag);

            // Create the GPU program
            handle = GL.CreateProgram();
            GL.AttachShader(handle, vertShader);
            GL.AttachShader(handle, fragShader);
            GL.LinkProgram(handle);

            // Cleaning up
            GL.DetachShader(handle, vertShader);
            GL.DetachShader(handle, fragShader);
            GL.DeleteShader(fragShader);
            GL.DeleteShader(vertShader);

            Console.WriteLine($"Shaders: Shader {Name} compiled succsessfully");
        }

        /// <summary>
        /// Use this shader before rendering an object
        /// </summary>
        public void Use() { GL.UseProgram(handle); }

        /// <summary>
        /// Get an attribute location from it's name
        /// </summary>
        /// <param name="attribName">The attribute name</param>
        /// <returns></returns>
        public int GetAttribLocation(string attribName) { return GL.GetAttribLocation(handle, attribName); }

        #region Set Uniforms        

        // Sets a Uniform float
        public void SetFloat(int location, float value) { GL.Uniform1(location, value); }
        // Sets a Uniform int
        public void SetInt(int location, int value) { GL.Uniform1(location, value); }
        // Sets a Uniform Vector2
        public void SetVector2(int location, Vector2 value) { GL.Uniform2(location, value.X, value.Y); }
        // Sets a Uniform Vector3
        public void SetVector3(int location, Vector3 value) { GL.Uniform3(location, value.X, value.Y, value.Z); }
        // Sets a Uniform Vector4
        public void SetVector4(int location, Vector4 value) { GL.Uniform4(location, value.X, value.Y, value.Z, value.W); }
        // Sets a Uniform Matrix4
        public void SetMatrix4(int location, Matrix4 value) { GL.UniformMatrix4(location, false, ref value); }

        #endregion  

        /// <summary>
        /// Dispose of this shader
        /// </summary>
        public void Dispose() { GL.DeleteProgram(handle); }

        /// <summary>
        /// Destructor
        /// </summary>
        ~Shader() { GL.DeleteProgram(handle); }
    }
}
