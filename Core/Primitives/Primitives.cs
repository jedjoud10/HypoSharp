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
    /// A Primitive shape
    /// </summary>
    public abstract class Shape : EngineEntity
    {
        //Main shape vars
        public Color color;
        public Model model;
        public Mesh mesh;

        /// <summary>
        /// Primitive shape constructor
        /// </summary>
        /// <param name="position">Position for this entity. Default is (0, 0, 0)</param>
        /// <param name="rotation">Rotation (Quaternion) for this entity. Default is (0, 0, 0, 0)</param>
        /// <param name="color">Color of the shape</param>
        public Shape(Vector3 position, Quaternion rotation, Color color) : base(position, rotation)
        {
            this.color = color;
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
            model.transform = Matrix4x4.CreateFromQuaternion(rotation);
            Raylib.DrawModel(model, position, 1, color);
        }
    }
}
