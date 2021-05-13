using System.Numerics;

namespace HypoSharp.Core
{
    /// <summary>
    /// Information about current game time and delta time
    /// </summary>
    public static class Time
    {
        //Main time vars
        public static float DeltaTime { get; set; }
        public static float TimeSinceGameStart { get; set; }
    }
}
