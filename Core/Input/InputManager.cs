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
        // Mappings ([Name, Key/button] --> Map --> Value)
        internal static Dictionary<string, InputMapElement> KeyToMap { get; private set; }
        // The current state of the keyboard
        public static KeyboardState KeyboardState { private get; set; }
        // The last state of the keyboard
        public static KeyboardState LastKeyboardState { private get; set; }
        // The current state of the mouse
        public static MouseState MouseState { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        static InputManager()
        {
            // Setup the mappings
            KeyToMap = new Dictionary<string, InputMapElement>();
            AddKeyMapping("printFps", Keys.G);
            AddKeyMapping("quit", Keys.Escape);
        }

        /// <summary>
        /// Called every frame to update the World's keyboard state
        /// </summary>
        public static void OnInputLoop()
        {            
            //Debug FPS
            if (IsKeyMappingPressed("printFps")) Console.WriteLine($"FPS: {1f / Time.DeltaTime}");
            if (IsKeyMappingPressed("quit")) World.Context.Close();
            LastKeyboardState = KeyboardState.GetSnapshot();
        }

        /// <summary>
        /// Add a specific mapping for a mouse button
        /// </summary>
        /// <param name="name">Name of the mapping</param>
        /// <param name="defaultMouseButton">The default mouse button for this mapping</param>
        public static void AddMouseButtonMapping(string name, MouseButton defaultMouseButton) 
        {
            KeyToMap.Add(name, new InputMapElement { MappingType = KeyMapping.MOUSE_BUTTON, Mapped = defaultMouseButton });
        }

        /// <summary>
        /// Add a specific mapping for a keyboard key
        /// </summary>
        /// <param name="name">Name of the mapping</param>
        /// <param name="defaultKey">The default key for this mapping</param>
        public static void AddKeyMapping(string name, Keys defaultKey) 
        {
            Console.WriteLine($"InputManager: Add new key mapping. {name} -> {defaultKey}");
            KeyToMap.Add(name, new InputMapElement { MappingType = KeyMapping.KEY, Mapped = defaultKey });
        }



        /// <summary>
        /// Check if a certain key mapping is pressed 
        /// </summary>
        /// <returns></returns>
        public static bool IsKeyMappingPressed(string name) 
        {
            Keys key = (Keys)KeyToMap[name].Mapped;
            return (KeyboardState[key] != LastKeyboardState[key]) && KeyboardState[key];
        }

        /// <summary>
        /// Check if a certain key mapping is held
        /// </summary>
        /// <returns></returns>
        public static bool IsKeyMappingHeld(string name) 
        { 
            return KeyboardState[(Keys)KeyToMap[name].Mapped];
        }
    }

    [System.Serializable]
    //Each element in the mapping ()
    internal class InputMapElement 
    {
        // The type of mapping we're using
        public KeyMapping MappingType { get; set; }
        // The button/key this element maps to
        public object Mapped { get; set; }
    }

    [System.Serializable]
    // The type of key mapping
    internal enum KeyMapping 
    {
        KEY, MOUSE_BUTTON, MOUSE_DELTA, MOUSE_POSITION
    }
}
