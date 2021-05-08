using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;
using HypoSharp.Core;

namespace HypoSharp.Rendering
{    
    /// <summary>
    /// A Camera object
    /// </summary>
    public class Camera : EngineEntity
    {
        //Main camera vars
        private Camera3D camera3D;
        public Camera3D Camera3D { get { return camera3D; } set { camera3D = value; } }

        /// <summary>
        /// Camera constructor
        /// </summary>
        /// <param name="fov">Field of View (Horizontal) of this camera. Defaults to 90</param>
        public Camera(float fov = 60) 
        {
            //https://www.reddit.com/r/Planetside/comments/1xl1z5/brief_table_for_calculating_fieldofview_vertical/
            float verticalFov = Math.Abs((float)(2 * Math.Atan(Math.Tan((fov * (Math.PI / 180f)) / 2) * ((float)Raylib.GetScreenWidth() / (float)Raylib.GetScreenHeight()))));
            Console.Write(verticalFov);
            Camera3D = new Camera3D(Position, Vector3.Zero, Vector3.UnitY, verticalFov * (180f / MathF.PI), CameraType.CAMERA_PERSPECTIVE);
        }

        /// <summary>
        /// Initialization method
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
            Position = new Vector3(0, 30, -100);            
            Raylib.SetCameraMode(Camera3D, CameraMode.CAMERA_CUSTOM);
        }

        /// <summary>
        /// The main game Loop called before rendering this EngineEntity
        /// </summary>
        public override void Loop(float delta)
        {
            camera3D.position = Position;
            camera3D.up = Vector3.Transform(Vector3.UnitY, Rotation);
            camera3D.target = Vector3.Transform(Vector3.UnitZ, Rotation) + Position;
            base.Loop(delta);
        }

        /// <summary>
        /// Render this specific EngineEntity
        /// </summary>
        public override void Render()
        {            
            base.Render();
            Raylib.UpdateCamera(ref camera3D);
            Raylib.BeginMode3D(camera3D);
        }

        /// <summary>
        /// Called when this object is getting disposed of
        /// </summary>
        public override void Dispose()
        {
            base.Dispose();
        }
    }    
}
