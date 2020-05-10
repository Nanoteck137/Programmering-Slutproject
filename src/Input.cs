using System;
using SFML.Window;

/// <summary>
/// This class is a singleton and is used to manage the inputs from the user, for now it 
/// only manages the mouse buttons
/// </summary>
class Input
{
    private static Input instance;
    public static Input Instance
    {
        get
        {
            if (instance == null)
                instance = new Input();

            return instance;
        }
    }

    // Constants for the mouse buttons ids
    public const int LEFT_BUTTON = 0;
    public const int RIGHT_BUTTON = 1;
    public const int MIDDLE_BUTTON = 2;
    public const int XBUTTON1_BUTTON = 3;
    public const int XBUTTON2_BUTTON = 4;

    // `keyStates` is used to store the current frame keyboard state
    private bool[] keyStates;
    // `keyStates` is used to store the previous frame keyboard state
    private bool[] prevKeyStates;

    // `mouseButtonStates` is used to store the current 
    // frame mouse button states for all the buttons on the mouse 
    private bool[] mouseButtonStates;

    // `prevMouseButtonStates` is used to store the previous 
    // frame mouse button states for all the buttons on the mouse
    private bool[] prevMouseButtonStates;

    private Input()
    {
        // Get how many keys SFML supports
        int keyStateCount = (int)Keyboard.Key.KeyCount;

        // Initialize the key state arrys with the key count from SFML
        keyStates = new bool[keyStateCount];
        prevKeyStates = new bool[keyStateCount];


        // Get how many buttons SFML supports
        int buttonStateCount = (int)Mouse.Button.ButtonCount;

        // Initializes `mouseButtonStates` and `prevMouseButtonStates` 
        // the Button State arrays with the number of buttons SFML supports
        mouseButtonStates = new bool[buttonStateCount];
        prevMouseButtonStates = new bool[buttonStateCount];
    }

    public void Update()
    {
        // loop through the `keyStates` and setting the 
        // `prevKeyStates` to it's state so we can detect if a 
        // keyboard key has been down for more then one frame
        for (int i = 0; i < keyStates.Length; i++)
            prevKeyStates[i] = keyStates[i];

        // loop through the `mouseButtonStates` and setting the 
        // `prevMouseButtonStates` to it's state so we can detect if a mouse 
        // button has been down for more then one frame
        for (int i = 0; i < mouseButtonStates.Length; i++)
            prevMouseButtonStates[i] = mouseButtonStates[i];
    }

    /// <summary>
    /// Change the state of a key
    /// </summary>
    /// <param name="key">The key that should change state</param>
    /// <param name="state">The new state for the key</param>
    public void SetKeyState(int key, bool state)
    {
        keyStates[key] = state;
    }

    /// <summary>
    /// Check if the key is down in the current frame
    /// </summary>
    /// <param name="key">The key to check</param>
    /// <returns>Returns true if the key is down this frame</returns>
    public bool IsKeyDown(int key)
    {
        return keyStates[key];
    }

    /// <summary>
    /// Wrapper to convert from a SFML key
    /// </summary>
    /// <param name="key">The SFML key</param>
    /// <returns>Returns true if the key is down this frame</returns>
    public bool IsKeyDown(Keyboard.Key key)
    {
        return IsKeyDown((int)key);
    }

    /// <summary>
    /// Check if the key is down in the current frame and check if 
    /// the key was down the previous frame
    /// </summary>
    /// <param name="key">The key to check</param>
    /// <returns>Returns true if the key is down this frame 
    /// and the key was not down the previous frame</returns>
    public bool IsKeyPressed(int key)
    {
        return keyStates[key] && !prevKeyStates[key];
    }

    /// <summary>
    /// Wrapper to convert from a SFML key
    /// </summary>
    /// <param name="key">SFML key</param>
    /// <returns>Returns true if the key is down this frame 
    /// and the key was not down the previous frame</returns>
    public bool IsKeyPressed(Keyboard.Key key)
    {
        return IsKeyPressed((int)key);
    }

    /// <summary>
    /// Change the state of a button
    /// </summary>
    /// <param name="button">The button whos state should change</param>
    /// <param name="state">The state the button should have</param>
    public void SetButtonState(int button, bool state)
    {
        // Set the index in `mouseButtonStates` to the right state
        mouseButtonStates[button] = state;
    }

    /// <summary>
    /// Checks if a button is down the current frame
    /// </summary>
    /// <param name="button">The button to check</param>
    /// <returns>Returns true if the button is down the current frame</returns>
    public bool IsButtonDown(int button)
    {
        // Check if the button state is true for this frame
        return mouseButtonStates[button];
    }

    /// <summary>
    /// Checks if a button is down but only for one frame
    /// </summary>
    /// <param name="button">The button to check</param>
    /// <returns>Returns true if the button is down but only 
    /// for one frame</returns>
    public bool IsButtonPressed(int button)
    {
        // Check if the button state is true and check if the button was 
        // down the previous frame
        return mouseButtonStates[button] && !prevMouseButtonStates[button];
    }
}