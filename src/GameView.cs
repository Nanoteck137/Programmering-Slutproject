using System;

using SFML.System;
using SFML.Graphics;

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
        rightPaddle = new RectangleShape(model.RightPaddle.Size);
        ball = new CircleShape(model.Ball.Radius);
    }

    public void Render(RenderTarget renderTarget)
    {
        leftPaddle.Position = Model.LeftPaddle.Position;
        renderTarget.Draw(leftPaddle);

        rightPaddle.Position = Model.RightPaddle.Position;
        renderTarget.Draw(rightPaddle);

        ball.Position = Model.Ball.Position;
        renderTarget.Draw(ball);
    }
}
