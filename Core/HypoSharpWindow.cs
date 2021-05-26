using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using HypoSharp.Core.Input;
using System;
using OpenTK.Mathematics;

namespace HypoSharp.Core
{
    /// <summary>
    /// The main game window
    /// </summary>
    public class HypoSharpWindow : GameWindow
    {
        // Our window settings
        public GameWindowSettings WindowSettings { get; private set; }
        public NativeWindowSettings NativeWindowSettings { get; private set; }

        /// <summary>
        /// When we create the window
        /// </summary>
        public HypoSharpWindow(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {
            WindowSettings = gameWindowSettings;
            NativeWindowSettings = nativeWindowSettings;
            RenderFrequency = 60;
        }

        /// <summary>
        /// When we load the window
        /// </summary>
        protected override void OnLoad()
        {
            // Creation of the world
            World.InitializeWorld();
            base.OnLoad();
        }

        /// <summary>
        /// Right before we call the .Run method
        /// </summary>
        public void PreLoad() 
        {
            // Right before we call the run method
            World.PreInitializeWorld(this);
        }

        /// <summary>
        /// When the window gets resized
        /// </summary>
        protected override void OnResize(ResizeEventArgs e)
        {
            // Resize the window
            GL.Viewport(0, 0, Size.X, Size.Y);
            World.ResizeWindow(Size.Y, Size.X);
            base.OnResize(e);
        }

        /// <summary>
        /// When we want to render all the objects in the world
        /// </summary>
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            // Update the Inputs
            InputManager.OnFrameInputLoop();

            // Render the whole world
            World.RenderWorld(args.Time);
            base.OnRenderFrame(args);
        }

        /// <summary>
        /// Called an unlimited amount of times per second (Limited by the hardware). Only used for inputs
        /// </summary>
        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            // Read input from the keyboard
            InputManager.OnInputLoop();
        }

        /// <summary>
        /// When we unload the window and close the program
        /// </summary>
        protected override void OnUnload()
        {
            // Dipose the world
            World.DestroyWorld_AKA_NUKE();
            base.OnUnload();
        }
    }
}
