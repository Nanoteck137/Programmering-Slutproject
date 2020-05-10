using System;

using SFML.System;
using SFML.Graphics;

/// <summary>
/// This class handles how the game is renderered to the user
/// </summary>
public class GameView
{
    public GameModel Model { get; private set; }

    private RectangleShape leftPaddle;
    private RectangleShape rightPaddle;
    private CircleShape ball;

    public GameView(GameModel model)
    {
        Model = model;

        leftPaddle = new RectangleShape(model.LeftPaddle.Size);
        leftPaddle.Origin = new Vector2f(model.LeftPaddle.Size.X / 2.0f, model.LeftPaddle.Size.Y / 2.0f);

        rightPaddle = new RectangleShape(model.RightPaddle.Size);
        rightPaddle.Origin = new Vector2f(model.RightPaddle.Size.X / 2.0f, model.RightPaddle.Size.Y / 2.0f);

        ball = new CircleShape(model.Ball.Radius);
        ball.Origin = new Vector2f(model.Ball.Radius, model.Ball.Radius);
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
    }
}
