using OpenTK.Windowing.Common;
using OpenTK.Mathematics;
using OpenTK.Graphics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace HypoSharp.Core.Input
{
    /// <summary>
    /// Class that manages all inputs from OpenTK, it also maps them using a certain in the user settings file
    /// </summary>
    public static class InputManager
    {
        // The current state of the keyboard
        public static KeyboardState KeyboardState { private get; set; }
        // The last state of the keyboard
        public static KeyboardState LastKeyboardState { private get; set; }
        // The current state of the mouse
        public static MouseState MouseState { get; set; }
        /// <summary>
        /// Called every frame to update the World's keyboard state
        /// </summary>
        public static void OnInputLoop()
        {
            //Debug FPS
            if (IsKeyMappingPressed(Keys.G)) Console.WriteLine($"FPS: {1f / Time.FrameDeltaTime}  UPS: {1f / Time.UpdateDeltaTime}");
            LastKeyboardState = KeyboardState.GetSnapshot();
        }

        /// <summary>
        /// Add a specific mapping for a mouse button
        /// </summary>
        /// <param name="name">Name of the mapping</param>
        /// <param name="defaultMouseButton">The default mouse button for this mapping</param>
        public static void AddMouseMapping(string name, MouseButton defaultMouseButton) { }

        /// <summary>
        /// Add a specific mapping for a keyboard key
        /// </summary>
        /// <param name="name">Name of the mapping</param>
        /// <param name="defaultKey">The default key for this mapping</param>
        public static void AddKeyMapping(string name, Keys defaultKey) { }

        /// <summary>
        /// Check if a certain key mapping is pressed 
        /// </summary>
        /// <returns></returns>
        public static bool IsKeyMappingPressed(Keys key) { return (KeyboardState[key] != LastKeyboardState[key]) && KeyboardState[key]; }

        /// <summary>
        /// Check if a certain key mapping is held
        /// </summary>
        /// <returns></returns>
        public static bool IsKeyMappingHeld(Keys key) { return KeyboardState[key]; }
    }
}
