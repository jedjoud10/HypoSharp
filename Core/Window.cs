using HypoSharp.Input;
using HypoSharp.Editor;
using HypoSharp.Editor.Input;
using System;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL;

namespace HypoSharp.Core
{
    /// <summary>
    /// The main game window
    /// </summary>
    public class Window : GameWindow
    {
        // Our window settings
        public GameWindowSettings WindowSettings { get; private set; }
        public NativeWindowSettings NativeWindowSettings { get; private set; }
        // The aspect ratio (Width / Height)
        public float AspectRatio { get { return (float)Size.X / (float)Size.Y; } }
        // When the window gets resized
        public static event Action<float> OnWindowResize;
        // If this is the game engine editor, or the actual game
        public static bool IsGameEngine { get; private set; }
        // The singleton of this window
        public static Window Singleton { get; private set; }
        // Going fullscreen
        private bool fullscreen;
        public bool Fullscreen
        {
            get { return fullscreen; }
            set
            {
                fullscreen = value;
                if (fullscreen)
                {
                    WindowState = WindowState.Fullscreen;
                    WindowBorder = WindowBorder.Hidden;
                    VSync = VSyncMode.On;
                }
                else
                {
                    WindowState = WindowState.Normal;
                    WindowBorder = WindowBorder.Resizable;
                    VSync = VSyncMode.On;
                }
            }
        }

        /// <summary>
        /// When we create the window
        /// </summary>
        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings, bool isGameEngine) : base(gameWindowSettings, nativeWindowSettings)
        {
            WindowSettings = gameWindowSettings;
            NativeWindowSettings = nativeWindowSettings;
            RenderFrequency = 60;
            IsGameEngine = isGameEngine;
            Singleton = this;

            // Setup the pre window stuff
            if (isGameEngine)
            {
                CursorGrabbed = false;
                VSync = VSyncMode.Off;
            }
            else
            {
                CursorGrabbed = true;
                VSync = VSyncMode.On;
            }
        }

        /// <summary>
        /// When we load the window
        /// </summary>
        protected override void OnLoad()
        {
            // Creation of the world
            if (IsGameEngine) EditorWorld.InitializeEditor();
            else World.InitializeWorld();
            base.OnLoad();
        }

        /// <summary>
        /// When the window gets resized
        /// </summary>
        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            // Resize the window
            GL.Viewport(0, 0, Size.X, Size.Y);
            OnWindowResize?.Invoke(AspectRatio);
        }

        /// <summary>
        /// When we want to render all the objects in the world
        /// </summary>
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            // Render the whole world
            if (IsGameEngine) 
            {
                EditorInputManager.OnFrameInputLoop();
                EditorWorld.RenderEditor(args.Time);
            }
            else
            {
                // Update the Inputs
                InputManager.OnFrameInputLoop();
                World.RenderWorld(args.Time); 
            }
            base.OnRenderFrame(args);
        }

        /// <summary>
        /// Called an unlimited amount of times per second (Limited by the hardware). Only used for inputs
        /// </summary>
        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            // Read input from the keyboard and mouse
            if (IsGameEngine) EditorInputManager.OnInputLoop();
            else InputManager.OnInputLoop();
        }

        /// <summary>
        /// When we unload the window and close the program
        /// </summary>
        protected override void OnUnload()
        {
            // Dipose the world
            if (IsGameEngine) EditorWorld.DestroyEditor();
            else World.DestroyWorld_AKA_NUKE();
            base.OnUnload();
        }
    }
}
