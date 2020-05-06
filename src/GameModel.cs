using System;

using SFML.System;
using SFML.Window;

public class GameModel
{
    public Paddle LeftPaddle { get; private set; }
    public Paddle RightPaddle { get; private set; }

    public Ball Ball { get; private set; }

    public GameModel()
    {
        Vector2u windowSize = Application.Instance.Window.Size;

        float ballRadius = 20.0f;
        Ball = new Ball(new Vector2f(windowSize.X / 2 - ballRadius / 2, windowSize.Y / 2 - ballRadius / 2), ballRadius);

        float offset = 20.0f;
        float paddleWidth = 16.0f;
        float paddleHeight = 200.0f;

        PlayerPaddle.Config leftConfig = new PlayerPaddle.Config()
        {
            InitialPosition = new Vector2f(offset, windowSize.Y / 2 - paddleHeight / 2),
            Size = new Vector2f(paddleWidth, paddleHeight),
            UpKey = Keyboard.Key.W,
            DownKey = Keyboard.Key.S,
        };

        LeftPaddle = new PlayerPaddle(leftConfig);

        PlayerPaddle.Config rightConfig = new PlayerPaddle.Config()
        {
            InitialPosition = new Vector2f(windowSize.X - paddleWidth - offset, windowSize.Y / 2 - paddleHeight / 2),
            Size = new Vector2f(paddleWidth, paddleHeight),
            UpKey = Keyboard.Key.Up,
            DownKey = Keyboard.Key.Down,
        };
        RightPaddle = new PlayerPaddle(rightConfig);
    }

    public void Update(float deltaTime)
    {
        LeftPaddle.Update(deltaTime);
        RightPaddle.Update(deltaTime);
        Ball.Update(deltaTime);
    }
}
