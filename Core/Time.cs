using System.Numerics;

namespace HypoSharp.Core
{
    /// <summary>
    /// Information about current game time and delta time
    /// </summary>
    public static class Time
    {
        // Main time vars
        // Time between each frames in the game world
        public static float DeltaTime { get; set; }
        // Time (in second) from the start of the game
        public static float TimeSinceGameStart { get; set; }
        // How many tick in a second
        public const uint TICK_RATE = 4;
        // The total amount of ticks
        public static ulong Ticks { get; internal set; }
        // Time since the last tick
        public static float TimeSinceLastTick { get; set; }

        // ----Editor----
        // Time between each frames in the editor
        public static float EditorDeltaTime { get; set; }

    }
}
