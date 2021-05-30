using HypoSharp.Core;
using HypoSharp.Debug;
using HypoSharp.Primitives;
using System;
using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;

namespace HypoSharp.Editor
{
    /// <summary>
    /// The Hypothermia program!
    /// </summary>
    public static class HypothermiaProgram
    {
        /// <summary>
        /// Called when the game starts
        /// </summary>
        static void Main(string[] args)
        {
            World.OnInitializeWorld += OnInitializeWorld;
            GameWindowSettings settings = new GameWindowSettings();
            settings.IsMultiThreaded = false;
            NativeWindowSettings nativeWindowSettings = new NativeWindowSettings();
            nativeWindowSettings.Title = "HypoSharp GameEngine";
            //nativeWindowSettings.Size = new Vector2i(1920, 1080);
            using (Window game = new Window(settings, nativeWindowSettings))
            {
                game.PreLoad();
                game.Run();
            }
        }

        /// <summary>
        /// When the world gets initialized, generate all the objects and stuff like that
        /// </summary>
        static void OnInitializeWorld()
        {
            // Create debug camera
            DebugCamera camera = new DebugCamera()
            {
                Transform = new Transform()
                {
                    Position = new Vector3(0, 0, 3)
                }
            };
            camera.HorizontalFov = 70;

            // Set world camera
            World.Camera = camera;

            // Create quad
            Quad quad = new Quad()
            {
                Transform = new Transform()
                {
                    Scale = Vector3.One * 10000,
                    Rotation = Quaternion.FromAxisAngle(Vector3.UnitX, 40)
                }
            };

            World.AddObject(quad);
            World.AddObject(camera);
        }
    }
}
