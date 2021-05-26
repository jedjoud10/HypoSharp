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
        // Key Mappings ([Name, Key/Button] --> Map --> Value)
        internal static Dictionary<string, ButtonInputMapElement> ButtonToMap { get; private set; }
        // Value Mappings ([Name, MouseVar/JoystickVar] --> Map --> Value)
        internal static Dictionary<string, ValueInputMapElement> ValueToMap { get; private set; }
        // The current state of the keyboard
        public static KeyboardState KeyboardState { private get; set; }
        // The current state of the mouse
        public static MouseState MouseState { private get; set; }
        // The position of the mouse last frame
        public static Vector2 LastMousePosition { get; set; }
        // The difference of the positions from last frame's mouse position
        public static Vector2 MouseDelta { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        static InputManager()
        {
            // Setup the mappings
            ButtonToMap = new Dictionary<string, ButtonInputMapElement>();
            ValueToMap = new Dictionary<string, ValueInputMapElement>();
            AddKeyMapping("printFps", Keys.G);
            AddKeyMapping("quit", Keys.Escape);
        }

        /// <summary>
        /// Called during the Update loop
        /// </summary>
        public static void OnInputLoop()
        {
            //Debug FPS
            KeyboardState = World.Context.KeyboardState;
            MouseState = World.Context.MouseState;            
            if (IsButtonMappingPressed("printFps")) Console.WriteLine($"FPS: {1f / Time.DeltaTime}");
            if (IsButtonMappingPressed("quit")) World.Context.Close();
        }

        /// <summary>
        /// Called during the Frame loop
        /// </summary>
        public static void OnFrameInputLoop() 
        {
            MouseDelta = MouseState.Position - LastMousePosition;
            LastMousePosition = MouseState.Position;
        }

        /// <summary>
        /// Add a specific mapping for a mouse button
        /// </summary>
        /// <param name="name">Name of the mapping</param>
        /// <param name="defaultMouseButton">The default mouse button for this mapping</param>
        public static void AddMouseButtonMapping(string name, MouseButton defaultMouseButton) 
        {
            ButtonToMap.Add(name, new ButtonInputMapElement { MappingType = ButtonMapping.MOUSE_BUTTON, Mapped = defaultMouseButton });
        }

        /// <summary>
        /// Add a specific value mapping for a specific ValueMapping type
        /// </summary>
        /// <param name="name"></param>
        /// <param name="mappingType"></param>
        public static void AddValueMapping(string name, ValueMapping mappingType)
        {
            ValueToMap.Add(name, new ValueInputMapElement { MappingType = mappingType });
        }

        /// <summary>
        /// Add a specific mapping for a keyboard key
        /// </summary>
        /// <param name="name">Name of the mapping</param>
        /// <param name="defaultKey">The default key for this mapping</param>
        public static void AddKeyMapping(string name, Keys defaultKey) 
        {
            Console.WriteLine($"InputManager: Add new key mapping. {name} -> {defaultKey}");
            ButtonToMap.Add(name, new ButtonInputMapElement { MappingType = ButtonMapping.KEY, Mapped = defaultKey });
        }

        /// <summary>
        /// Reads a specified value mapping
        /// </summary>
        /// <param name="name">Name of the mapping</param>
        /// <returns>Object returned, you must cast it by yourself</returns>
        public static object ReadValueMapping(string name) 
        {
            switch (ValueToMap[name].MappingType)
            {
                case ValueMapping.MOUSE_POSITION:
                    return MouseState.Position;
                case ValueMapping.MOUSE_DELTA:                    
                    return MouseDelta;
                default:
                    return null;
            }
        }

        /// <summary>
        /// Check if a certain key mapping is pressed 
        /// </summary>
        /// <returns>Is the button pressed in the current frame?</returns>
        public static bool IsButtonMappingPressed(string name) 
        {
            switch (ButtonToMap[name].MappingType)
            {
                case ButtonMapping.KEY:
                    Keys key = (Keys)ButtonToMap[name].Mapped;
                    return KeyboardState.IsKeyPressed(key);
                case ButtonMapping.MOUSE_BUTTON:
                    MouseButton button = (MouseButton)ButtonToMap[name].Mapped;
                    return MouseState.IsButtonDown(button);
                default:
                    return false;
            }
            
            
        }

        /// <summary>
        /// Check if a certain button mapping is held
        /// </summary>
        /// <returns>Is the button held?</returns>
        public static bool IsButtonMappingHeld(string name) 
        {
            switch (ButtonToMap[name].MappingType)
            {
                case ButtonMapping.KEY:
                    return KeyboardState[(Keys)ButtonToMap[name].Mapped];
                case ButtonMapping.MOUSE_BUTTON:
                    return MouseState[(MouseButton)ButtonToMap[name].Mapped];
                default:
                    return false;
            }            
        }
    }
    
    /// <summary>
    /// Each element in the button mapping dictionary
    /// </summary>
    internal class ButtonInputMapElement 
    {
        // The type of mapping we're using
        public ButtonMapping MappingType { get; set; }
        // The button/key this element maps to
        public object Mapped { get; set; }
    }

    /// <summary>
    /// Each element in the mapping dictionary
    /// </summary>
    internal class ValueInputMapElement
    {
        // The type of mapping we're using
        public ValueMapping MappingType { get; set; }
    }

    /// <summary>
    /// Each type of key mapping, either a key mapping a mouse button mapping
    /// </summary>
    internal enum ButtonMapping 
    {
        KEY, MOUSE_BUTTON,
    }

    /// <summary>
    /// Each type of value mapping, either mouse mapping or joystick
    /// </summary>
    public enum ValueMapping
    {
        MOUSE_POSITION, MOUSE_DELTA,
    }
}
