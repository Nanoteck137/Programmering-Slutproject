using System;

using SFML.Graphics;

/// <summary>
/// This class manages the Screens used in the application, and handles 
/// the if a screen needs to change. 
/// 
/// And its a singleton
/// </summary>
public class ScreenManager
{
    private static ScreenManager instance;
    public static ScreenManager Instance
    {
        get
        {
            if (instance == null)
                instance = new ScreenManager();
            return instance;
        }
    }

    /// <summary>
    /// The Current Screen
    /// </summary>
    /// <value>The Screen</value>
    public Screen CurrentScreen { get; private set; }

    private ScreenManager() { }

    /// <summary>
    /// Change the current screen
    /// </summary>
    /// <param name="screen"></param>
    public void ChangeScreen(Screen screen)
    {
        if (CurrentScreen != null)
        {
            // Send a screen hide event to the previous screen
            CurrentScreen.OnScreenHide();
        }

        // Set the new screen
        CurrentScreen = screen;

        // Send a screen show event to the new screen
        CurrentScreen.OnScreenShow();
    }

    /// <summary>
    /// Update the screen
    /// </summary>
    /// <param name="deltaTime"></param>
    public void Update(float deltaTime)
    {
        // If there is a screen then update it
        if (CurrentScreen == null)
            return;

        CurrentScreen.Update(deltaTime);
    }

    /// <summary>
    /// Render the screen
    /// </summary>
    /// <param name="renderTarget">The RenderTarget the screen should 
    /// render to</param>
    public void Render(RenderTarget renderTarget)
    {
        // If there is a screen then render it
        if (CurrentScreen == null)
            return;

        CurrentScreen.Render(renderTarget);
    }
}
