using System;

using SFML.Window;
using SFML.Graphics;

/// <summary>
///  The Game Screen displays the game
/// </summary>
public class GameScreen : Screen
{
    public GameView View { get; private set; }

    public GameScreen()
    {
        GameManager.Instance.RegisterOnNewGameCallback(OnNewGame);
    }

    private void OnNewGame()
    {
        View = new GameView(GameManager.Instance.Model);
    }

    public override void OnScreenShow()
    {
        GameManager.Instance.NewGame();
    }

    public override void OnScreenHide() { }

    public override void Update(float deltaTime)
    {
        if (Input.Instance.IsKeyPressed(Keyboard.Key.Escape))
        {
            ScreenManager.Instance.ChangeScreen(Application.Instance.MainMenuScreen);
        }
    }

    public override void Render(RenderTarget renderTarget)
    {
        View.Render(renderTarget);
    }
}
