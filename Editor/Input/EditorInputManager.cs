using HypoSharp.Core;
using System;
using System.Collections.Generic;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace HypoSharp.Editor.Input
{
    /// <summary>
    /// Class that manages all inputs from OpenTK, it also maps them using a certain in the user settings file
    /// </summary>
    public static class EditorInputManager
    {
        // The current state of the keyboard
        public static KeyboardState KeyboardState { get; set; }
        // The current state of the mouse
        public static MouseState MouseState { get; set; }
        // The position of the mouse last frame
        public static Vector2 LastMousePosition { get; set; }
        // The difference of the positions from last frame's mouse position
        public static Vector2 MouseDelta { get; private set; }

        /// <summary>
        /// Called during the Update loop, in the editor
        /// </summary>
        public static void OnInputLoop()
        {
            KeyboardState = Core.Window.Singleton.KeyboardState;
            MouseState = Core.Window.Singleton.MouseState;
        }

        /// <summary>
        /// Called during the Frame loop, in the editor
        /// </summary>
        public static void OnFrameInputLoop()
        {
            MouseDelta = MouseState.Position - LastMousePosition;
            LastMousePosition = MouseState.Position;
        }
    }
}
