using Raylib_cs;

namespace HypoSharp.Core
{
    /// <summary>
    /// The main game window
    /// </summary>
    public static class Window
    {
        /// <summary>
        /// Main entry point for the game
        /// </summary>
        public static void InitializeWindow(string title, int targetFps)
        {
            Raylib.SetConfigFlags(ConfigFlag.FLAG_WINDOW_RESIZABLE);
            Raylib.InitWindow(1280, 720, title);
            if (targetFps > 1) Raylib.SetTargetFPS(targetFps);
            World.InitializeWorld();

            //Main game loop
            while (!Raylib.WindowShouldClose()) World.FrameWorld();

            World.DestroyWorld();
            Raylib.CloseWindow();
        }
    }
}
