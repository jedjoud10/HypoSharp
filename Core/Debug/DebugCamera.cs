using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using HypoSharp.Core;
using HypoSharp.Rendering;

namespace HypoSharp.Debug
{
    /// <summary>
    /// A camera control system that allows you to move in 3D with all freedom
    /// </summary>
    public class DebugCamera : Camera
    {
        public const float defaultSpeed = 50f;
        public const float defaultSensivity = 0.04f;
        public float Speed { get; set; }
        public float Sensivity { get; set; }
        private Vector2 mouseDelta;
        private Vector2 lastMousePosition;
        private Vector2 summedDelta;

        /// <summary>
        /// Debug camera constructor
        /// </summary>
        /// <param name="fov">Field of View (Horizontal) of this camera. Defaults to 90</param>
        /// <param name="speed">Speed of this debug camera. Defaults to 50</param>
        /// <param name="sensivity">The sensivity of the mouse when looking around. Defaults to 0.04</param>
        public DebugCamera(float fov = 60, float speed = defaultSpeed, float sensivity = defaultSensivity) : base(fov)
        {
            this.Speed = speed;
            this.Sensivity = sensivity;
        }

        /// <summary>
        /// Initialization method
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// The tick method is ran a predefined amount per second
        /// </summary>
        public override void Tick()
        {
            base.Tick();
        }

        /// <summary>
        /// The main game Loop called before rendering this EngineEntity
        /// </summary>
        public override void Loop(float delta)
        {
            //Movement
            if (Raylib.IsKeyDown(KeyboardKey.KEY_W)) Position += Vector3.Transform(Vector3.UnitZ, Rotation) * delta * Speed;
            if (Raylib.IsKeyDown(KeyboardKey.KEY_A)) Position += Vector3.Transform(Vector3.UnitX, Rotation) * delta * Speed;
            if (Raylib.IsKeyDown(KeyboardKey.KEY_S)) Position -= Vector3.Transform(Vector3.UnitZ, Rotation) * delta * Speed;
            if (Raylib.IsKeyDown(KeyboardKey.KEY_D)) Position -= Vector3.Transform(Vector3.UnitX, Rotation) * delta * Speed;

            //Rotation
            Vector2 mousePos = Raylib.GetMousePosition();
            mouseDelta = mousePos - lastMousePosition;
            summedDelta += mouseDelta * Sensivity;
            summedDelta.Y = Math.Clamp(summedDelta.Y, -90, 90);
            Rotation = Quaternion.CreateFromYawPitchRoll(-summedDelta.X * (MathF.PI / 180), summedDelta.Y * (MathF.PI / 180), 0);
            lastMousePosition = mousePos;
            base.Loop(delta);
        }

        /// <summary>
        /// Render this specific EngineEntity
        /// </summary>
        public override void Render()
        {
            base.Render();
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
