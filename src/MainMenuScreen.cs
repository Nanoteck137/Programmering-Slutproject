using System;

using SFML.System;
using SFML.Graphics;

public class MainMenuScreen : Screen
{
    private UIButton playButton;

    public MainMenuScreen()
    {
        playButton = new UIButton(new Vector2f(10, 10));
    }

    public override void Update(float deltaTime)
    {
        playButton.Update(deltaTime);
    }

    public override void Render(RenderTarget renderTarget)
    {
        playButton.Render(renderTarget);
    }
}
