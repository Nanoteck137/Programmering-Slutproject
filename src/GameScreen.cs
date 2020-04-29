using System;
using SFML.Graphics;

public class GameScreen : Screen
{
    public GameView View { get; private set; }

    public GameScreen()
    {
        GameManager.Instance.NewGame();

        View = new GameView(GameManager.Instance.Model);
    }

    public override void Update(float deltaTime)
    {
    }

    public override void Render(RenderTarget renderTarget)
    {
        View.Render(renderTarget);
    }
}
