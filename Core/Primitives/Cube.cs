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
                mesh = Raylib.GenMeshCube(size.X, size.Y, size.Z);
                model = Raylib.LoadModelFromMesh(mesh);
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
            mesh = Raylib.GenMeshCube(size.X, size.Y, size.Z);
            model = Raylib.LoadModelFromMesh(mesh);
        }

        /// <summary>
        /// Render the entity every frame
        /// </summary>
        public override void Frame(float delta)
        {
            base.Frame(delta);
        }

        /// <summary>
        /// Render this specific EngineEntity
        /// </summary>
        public override void Render()
        {
            base.Render();            
        }
    }
}
