using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using System;

namespace HypoSharp.Core
{
    /// <summary>
    /// The main game window
    /// </summary>
    public class HypoSharpWindow : GameWindow
    {
        public GameWindowSettings WindowSettings { get; private set; }
        public NativeWindowSettings NativeWindowSettings { get; private set; }

        /// <summary>
        /// When we create the window
        /// </summary>
        public HypoSharpWindow(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {
            WindowSettings = gameWindowSettings;
            NativeWindowSettings = nativeWindowSettings;
        }

        /// <summary>
        /// When we load the window
        /// </summary>
        protected override void OnLoad()
        {
            //Creation of the world
            World.InitializeWorld(this);
            base.OnLoad();
        }

        /// <summary>
        /// When we want to render all the objects in the world
        /// </summary>
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            //Render the whole world
            World.RenderWorld();
            base.OnRenderFrame(args);
        }

        /// <summary>
        /// Called each frame when we update the window
        /// </summary>
        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            //Main game loop
            World.UpdateWorld(args.Time); 
            base.OnUpdateFrame(args);
        }

        /// <summary>
        /// When we unload the window and close the program
        /// </summary>
        protected override void OnUnload()
        {
            //Dipose the world
            World.DestroyWorld_AKA_NUKE();
            base.OnUnload();
        }
    }
}
