using HypoSharp.Core;
using Raylib_cs;
using System;
using System.Numerics;

namespace HypoSharp.Rendering
{
    /// <summary>
    /// A Camera object
    /// </summary>
    public class Camera : IGameLogic, ITransform
    {
        //ITransform implementations
        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }

        //Main camera vars
        private float _fov;
        private Camera3D _camera3D;
        public Camera3D Camera3D { get { return _camera3D; } set { _camera3D = value; } }
        public Vector3 Up { get; set; }
        public Vector3 Forward { get; set; }
        public Vector3 Left { get; set; }

        /// <summary>
        /// Camera constructor
        /// </summary>
        /// <param name="fov">Field of View (Horizontal) of this camera. Defaults to 90</param>
        public Camera(float fov = 60)
        {
            this._fov = fov;
        }

        /// <summary>
        /// Initialization method
        /// </summary>
        public void Initialize()
        {
            Position = new Vector3(0, 30, -100);
            //https://www.reddit.com/r/Planetside/comments/1xl1z5/brief_table_for_calculating_fieldofview_vertical/
            float verticalFov = Math.Abs((float)(2 * Math.Atan(Math.Tan((_fov * (Math.PI / 180f)) / 2) * ((float)Raylib.GetScreenWidth() / (float)Raylib.GetScreenHeight()))));
            Camera3D = new Camera3D(Position, Vector3.Zero, Vector3.UnitY, verticalFov * (180f / MathF.PI), CameraType.CAMERA_PERSPECTIVE);
            Raylib.SetCameraMode(Camera3D, CameraMode.CAMERA_CUSTOM);
        }

        /// <summary>
        /// Begin drawing to the render textures
        /// </summary>
        public virtual void Draw()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.WHITE);

            Raylib.UpdateCamera(ref _camera3D);
            Raylib.BeginMode3D(_camera3D);

            World.Renderer.Render();

            Raylib.DrawGrid(50, 10);
            Raylib.EndMode3D();

            //Draw fps
            Raylib.DrawText($"FPS: {Raylib.GetFPS()}", 12, 12, 20, Color.BLACK);
            Raylib.DrawText($"DELTA: {Time.DeltaTime}", 12, 32, 20, Color.BLACK);
            Raylib.DrawText($"CAM POS: {Position.ToString("0.0")}", 12, 52, 20, Color.BLACK);

            Raylib.EndDrawing();
        }

        /// <summary>
        /// The Loop method is ran every frame, before rendering
        /// </summary>
        public virtual void Loop()
        {
            Forward = Vector3.Transform(Vector3.UnitZ, Rotation);
            Up = _camera3D.up;
            Left = Vector3.Transform(Vector3.UnitX, Rotation);
            _camera3D.position = Position;
            _camera3D.up = Vector3.Transform(Vector3.UnitY, Rotation);
            _camera3D.target = Forward + Position;
        }

        /// <summary>
        /// Called when this object is getting disposed of
        /// </summary>
        public void Dispose()
        {
        }
    }
}
