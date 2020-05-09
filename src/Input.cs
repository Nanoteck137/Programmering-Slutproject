using System;
using SFML.Window;

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

    public const int LEFT_BUTTON = 0;
    public const int RIGHT_BUTTON = 1;
    public const int MIDDLE_BUTTON = 2;
    public const int XBUTTON1_BUTTON = 3;
    public const int XBUTTON2_BUTTON = 4;

    private bool[] mouseButtonState;
    private bool[] prevMouseButtonState;

    private Input()
    {
        int buttonStateCount = (int)Mouse.Button.ButtonCount;
        mouseButtonState = new bool[buttonStateCount];

        prevMouseButtonState = new bool[buttonStateCount];
    }

    public void Update()
    {
        for (int i = 0; i < mouseButtonState.Length; i++)
        {
            prevMouseButtonState[i] = mouseButtonState[i];
        }
    }

    public void SetButtonState(int button, bool state)
    {
        mouseButtonState[button] = state;
    }

    public bool IsButtonDown(int button)
    {
        return mouseButtonState[button];
    }

    public bool IsButtonPressed(int button)
    {
        return mouseButtonState[button] && !prevMouseButtonState[button];
    }
}