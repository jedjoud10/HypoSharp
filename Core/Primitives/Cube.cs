using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HypoSharp.Core.Primitives
{    
    /// <summary>
    /// A Cube shape
    /// </summary>
    public class Cube : Shape
    {
        //Main cube vars
        private Vector3 size;
        public Vector3 Size 
        {
            get { return size; }
            set 
            {
                size = value;

                //Update mesh
                Renderer.Model = Raylib.LoadModelFromMesh(Raylib.GenMeshCube(size.X, size.Y, size.Z));
            }
        }

        /// <summary>
        /// Cube shape constructor
        /// </summary>
        /// <param name="position">Position for this cube. Default is (0, 0, 0)</param>
        /// <param name="rotation">Rotation (Quaternion) for this cube. Default is (0, 0, 0, 0)</param>
        /// <param name="color">Color of this cube</param>
        public Cube(Vector3 position, Quaternion rotation, Color color, Vector3 size) : base(position, rotation, color)
        {
            Renderer = new ModelRenderer();
            Renderer.Model = Raylib.LoadModelFromMesh(Raylib.GenMeshCube(size.X, size.Y, size.Z));
            Renderer.Position = position;
            Renderer.Rotation = rotation;
            Renderer.Scale = 1;
            Renderer.Tint = color;
        }

        /// <summary>
        /// The main game Loop called before rendering this EngineEntity
        /// </summary>
        public override void Loop(float delta)
        {
            base.Loop(delta);
        }

        /// <summary>
        /// Render this specific EngineEntity
        /// </summary>
        public override void Render()
        {
            base.Render();
            Renderer.RenderModel();
        }
    }
}
