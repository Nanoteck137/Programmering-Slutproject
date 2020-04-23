using System;

using SFML.Graphics;

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

    public Screen CurrentScreen { get; private set; }

    private ScreenManager() { }

    public void ChangeScreen(Screen screen)
    {
        CurrentScreen = screen;
    }

    public void Update(float deltaTime)
    {
        if (CurrentScreen == null)
            return;

        CurrentScreen.Update(deltaTime);
    }

    public void Render(RenderTarget renderTarget)
    {
        if (CurrentScreen == null)
            return;

        CurrentScreen.Render(renderTarget);
    }
}
