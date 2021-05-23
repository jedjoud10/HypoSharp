using System.Numerics;

namespace HypoSharp.Core.Rendering
{
    /// <summary>
    /// A model that contains 3D information like vertices and UVs 
    /// </summary>
    public class Model
    {    
        //The vertices stored in this model
        public Vector3[] Vertices { get; set; }
        //The indices of each triangle in this model
        public uint[] Indices { get; set; }

        /// <summary>
        /// Load a model from a specific .hypo file
        /// </summary>
        public void LoadModel(string path) 
        {

        }
    }
}
