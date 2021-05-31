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
            EditorWorld.OnInitializeEditor += OnEditorInitializeWorld;
            World.OnInitializeWorld += OnGameInitializeWorld;
            GameWindowSettings settings = new GameWindowSettings();
            settings.IsMultiThreaded = false;
            NativeWindowSettings nativeWindowSettings = new NativeWindowSettings();
            nativeWindowSettings.Title = "HypoSharp GameEngine";
            //nativeWindowSettings.Size = new Vector2i(1920, 1080);
            using (Window game = new Window(settings, nativeWindowSettings, true))
            {
                game.Run();
            }
        }

        /// <summary>
        /// When the game engine editor gets initialized
        /// </summary>
        static void OnEditorInitializeWorld()
        {
            // Create debug camera
            EditorCamera camera = new EditorCamera()
            {
                Transform = new Transform()
                {
                    Position = new Vector3(0, 0, 3)
                }
            };
            camera.HorizontalFov = 70;

            // Set world camera
            EditorWorld.EditorCamera = camera;
            EditorWorld.AddObject(new Quad() 
            { 
                Transform = new Transform() 
                {                                    
                    Scale = Vector3.One * 10000,
                    Rotation = Quaternion.FromAxisAngle(Vector3.UnitX, 40)                
                } 
            });
            EditorWorld.AddObject(camera);
        }

        /// <summary>
        /// When the game gets initialized
        /// </summary>
        static void OnGameInitializeWorld() 
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
            World.AddObject(new Quad()
            {
                Transform = new Transform()
                {
                    Scale = Vector3.One * 10000,
                    Rotation = Quaternion.FromAxisAngle(Vector3.UnitX, 40)
                }
            });
            World.AddObject(camera);
        }
    }
}
