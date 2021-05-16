using HypoSharp.Core;
using HypoSharp.Core.Primitives;
using HypoSharp.Rendering;
using Raylib_cs;
using System;
using System.Numerics;

namespace HypoSharp.Core
{
    /// <summary>
    /// The renderer
    /// </summary>
    public class DeferredRenderer
    {
        //Deferred renderer shader
        public static Shader Shader { get; set; }
        public static RenderTexture2D Normal { get; set; }
        /// <summary>
        /// Constructor
        /// </summary>
        public DeferredRenderer()
        {
            World.OnDestroyWorld += Dispose;
            Normal = Raylib.LoadRenderTexture(1, 1);
            Shader = Raylib.LoadShaderCode(@"
#version 330

// Input vertex attributes
in vec3 vertexPosition;
in vec2 vertexTexCoord;
in vec3 vertexNormal;
in vec4 vertexColor;

// Input uniform values
uniform mat4 mvp;
uniform mat4 matModel;

// Output vertex attributes (to fragment shader)
out vec3 fragPosition;
out vec2 fragTexCoord;
out vec4 fragColor;
out vec3 fragNormal;

// NOTE: Add here your custom variables

void main()
{
    // Send vertex attributes to fragment shader
    fragPosition = vec3(matModel*vec4(vertexPosition, 1.0));
    fragTexCoord = vertexTexCoord;
    fragColor = vertexColor;
    
    mat3 normalMatrix = transpose(inverse(mat3(matModel)));
    fragNormal = normalize(normalMatrix*vertexNormal);

    // Calculate final vertex position
    gl_Position = mvp*vec4(vertexPosition, 1.0);
}

", @"
#version 330

// Input vertex attributes (from vertex shader)
in vec2 fragTexCoord;
in vec3 fragPosition;
in vec4 fragColor;
in vec3 fragNormal;

// Input uniform values
uniform sampler2D texture0;
uniform sampler2D

// Output GBuffer
out vec3 gNormal;

// NOTE: Add here your custom variables

void main()
{
    // Texel color fetching from texture sampler
    vec4 texelColor = texture(texture0, fragTexCoord);
    
    gNormal = normalize(fragNormal);
}
");
        }

        /// <summary>
        /// Renders the scene (Deffered lighting)
        /// </summary>
        public void Render()
        {
            //Call the render method each IRenderable object
            foreach (var renderableObject in World.RenderObjects) renderableObject.Render();
        }

        /// <summary>
        /// Set a specific mode
        /// </summary>
        /// <param name="model">The model to edit</param>
        /// <param name="materialIndex">The material to edit</param>
        /// <param name="shader">The shader to use</param>
        public unsafe static void SetMaterialShader(ref Model model, int materialIndex, ref Shader shader)
        {
            Material* materials = (Material*)model.materials.ToPointer();
            materials[materialIndex].shader = shader;
        }


        /// <summary>
        /// Unload this deferred renderer
        /// </summary>
        private void Dispose() 
        {
            Raylib.UnloadShader(Shader);
        }
    }
}
