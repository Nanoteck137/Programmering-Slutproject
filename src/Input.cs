using System;
using System.Diagnostics;
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

    private Input()
    {
        mouseButtonState = new bool[(int)Mouse.Button.ButtonCount];
    }

    public void Update()
    {

    }

    public void SetButtonState(int button, bool state)
    {
        mouseButtonState[button] = state;
    }

    public bool IsButtonDown(int button)
    {
        return mouseButtonState[button];
    }
}