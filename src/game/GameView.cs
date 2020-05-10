using System;

using SFML.System;
using SFML.Graphics;

/// <summary>
/// This class handles how the game is renderered to the user
/// </summary>
public class GameView
{
    private GameModel model;

    private Font font;

    private RectangleShape leftPaddle;
    private RectangleShape rightPaddle;
    private CircleShape ball;

    private Text leftSideScore;
    private Text rightSideScore;

    public GameModel Model { get { return model; } }

    public GameView(GameModel model)
    {
        this.model = model;

        leftPaddle = new RectangleShape(model.LeftPaddle.Size);
        leftPaddle.Origin = new Vector2f(model.LeftPaddle.Size.X / 2.0f, model.LeftPaddle.Size.Y / 2.0f);

        rightPaddle = new RectangleShape(model.RightPaddle.Size);
        rightPaddle.Origin = new Vector2f(model.RightPaddle.Size.X / 2.0f, model.RightPaddle.Size.Y / 2.0f);

        ball = new CircleShape(model.Ball.Radius);
        ball.Origin = new Vector2f(model.Ball.Radius, model.Ball.Radius);

        font = new Font("res/fonts/font.ttf");

        Vector2u windowSize = Application.Instance.Window.Size;

        float offset = windowSize.X / 4.0f;

        leftSideScore = new Text("0", font);
        leftSideScore.CharacterSize = 30;
        leftSideScore.Position = new Vector2f(windowSize.X / 2.0f - offset, 90.0f);

        rightSideScore = new Text("0", font);
        rightSideScore.CharacterSize = 30;
        rightSideScore.Position = new Vector2f(windowSize.X / 2.0f + offset, 90.0f);

        UpdateScore();
    }

    private void UpdateScore()
    {
        leftSideScore.DisplayedString = model.LeftSideScore.ToString();
        FloatRect rect = leftSideScore.GetLocalBounds();
        leftSideScore.Origin = new Vector2f(rect.Width / 2.0f, rect.Height / 2.0f);

        rightSideScore.DisplayedString = model.RightSideScore.ToString();
        rect = rightSideScore.GetLocalBounds();
        rightSideScore.Origin = new Vector2f(rect.Width / 2.0f, rect.Height / 2.0f);
    }

    public void Render(RenderTarget renderTarget)
    {
        // Set the position and skin of the left paddle then render it
        leftPaddle.Position = Model.LeftPaddle.Position;

        Skin leftSkin = SkinManager.Instance.GetSkinFromID(Model.LeftPaddle.SkinID);
        leftPaddle.FillColor = leftSkin.Color;

        renderTarget.Draw(leftPaddle);

        // Set the position and skin of the right paddle then render it
        rightPaddle.Position = Model.RightPaddle.Position;

        Skin rightSkin = SkinManager.Instance.GetSkinFromID(Model.RightPaddle.SkinID);
        rightPaddle.FillColor = rightSkin.Color;

        renderTarget.Draw(rightPaddle);

        // Set the position of the ball and render it
        ball.Position = Model.Ball.Position;
        renderTarget.Draw(ball);

        renderTarget.Draw(leftSideScore);
        renderTarget.Draw(rightSideScore);

        UpdateScore();
    }
}
