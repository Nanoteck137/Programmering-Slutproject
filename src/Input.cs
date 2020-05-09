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

    // `mouseButtonState` is used to store the current 
    // frame mouse button states for all the buttons on the mouse 
    private bool[] mouseButtonState;

    // `prevMouseButtonState` is used to store the previous 
    // frame mouse button states for all the buttons on the mouse
    private bool[] prevMouseButtonState;

    private Input()
    {
        // Get how many buttons SFML supports
        int buttonStateCount = (int)Mouse.Button.ButtonCount;

        // Initializes `mouseButtonState` and `prevMouseButtonState` 
        // the Button State arrays with the number of buttons SFML supports
        mouseButtonState = new bool[buttonStateCount];
        prevMouseButtonState = new bool[buttonStateCount];
    }

    public void Update()
    {
        // loop through the `mouseButtonState` and setting the 
        // `prevMouseButtonState` to it's state so we can detect if a mouse 
        // button has been down for more then one frame
        for (int i = 0; i < mouseButtonState.Length; i++)
            prevMouseButtonState[i] = mouseButtonState[i];
    }

    /// <summary>
    /// Change the state of a button
    /// </summary>
    /// <param name="button">The button whos state should change</param>
    /// <param name="state">The state the button should have</param>
    public void SetButtonState(int button, bool state)
    {
        // Set the index in `mouseButtonState` to the right state
        mouseButtonState[button] = state;
    }

    /// <summary>
    /// Checks if a button is down the current frame
    /// </summary>
    /// <param name="button">The button to check</param>
    /// <returns>Returns true if the button is down the current frame</returns>
    public bool IsButtonDown(int button)
    {
        // Check if the button state is true for this frame
        return mouseButtonState[button];
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
        return mouseButtonState[button] && !prevMouseButtonState[button];
    }
}