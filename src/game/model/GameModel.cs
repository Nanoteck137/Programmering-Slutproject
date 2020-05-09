using System;

using SFML.System;
using SFML.Window;

/// <summary>
/// This class holds all the data for the game
/// </summary>
public class GameModel
{
    // The left paddle
    public Paddle LeftPaddle { get; private set; }

    // The right paddle
    public Paddle RightPaddle { get; private set; }

    // The ball
    public Ball Ball { get; private set; }

    /// <summary>
    /// Used for initializing the game data
    /// </summary>
    public GameModel()
    {
        // Get the window size
        Vector2u windowSize = Application.Instance.Window.Size;

        // Create the ball
        float ballRadius = 20.0f;
        Ball = new Ball(new Vector2f(windowSize.X / 2, windowSize.Y / 2), ballRadius);

        // Setup some common paddle variables
        float offset = 25.0f;
        float paddleWidth = 16.0f;
        float paddleHeight = 200.0f;

        // Setup the left paddles config
        PlayerPaddle.Config leftConfig = new PlayerPaddle.Config()
        {
            InitialPosition = new Vector2f(offset, windowSize.Y / 2),
            Size = new Vector2f(paddleWidth, paddleHeight),
            UpKey = Keyboard.Key.W,
            DownKey = Keyboard.Key.S,
        };

        // Create the left paddle with the config
        LeftPaddle = new PlayerPaddle(leftConfig);

        // Setup the right paddle config
        PlayerPaddle.Config rightConfig = new PlayerPaddle.Config()
        {
            InitialPosition = new Vector2f(windowSize.X - offset, windowSize.Y / 2),
            Size = new Vector2f(paddleWidth, paddleHeight),
            UpKey = Keyboard.Key.Up,
            DownKey = Keyboard.Key.Down,
        };

        // Create the paddle
        RightPaddle = new PlayerPaddle(rightConfig);
    }

    /// <summary>
    /// Check if the ball is colliding with the left paddle
    /// </summary>
    private void CheckBallCollisionLeftPaddle()
    {
        // Check if the ball is colliding with the left paddle
        if (Ball.Position.X - Ball.Radius < LeftPaddle.Position.X + LeftPaddle.Size.X / 2 &&
            Ball.Position.X - Ball.Radius > LeftPaddle.Position.X &&
            Ball.Position.Y + Ball.Radius >= LeftPaddle.Position.Y - LeftPaddle.Size.Y / 2 &&
            Ball.Position.Y - Ball.Radius <= LeftPaddle.Position.Y + LeftPaddle.Size.Y / 2)
        {
            // If the ball is colliding with the left paddle then change 
            // the direction of the ball
            Vector2f dir = Ball.Direction;
            dir.X *= -1;
            Ball.Direction = dir;
        }
    }

    /// <summary>
    /// Check if the ball is colliding with the right paddle
    /// </summary>
    private void CheckBallCollisionRightPaddle()
    {
        // Check if the ball is colliding with the right paddle
        if (Ball.Position.X + Ball.Radius < RightPaddle.Position.X + RightPaddle.Size.X / 2 &&
            Ball.Position.X + Ball.Radius > RightPaddle.Position.X &&
            Ball.Position.Y + Ball.Radius >= RightPaddle.Position.Y - RightPaddle.Size.Y / 2 &&
            Ball.Position.Y - Ball.Radius <= RightPaddle.Position.Y + RightPaddle.Size.Y / 2)
        {
            // If the ball is colliding with the right paddle then change 
            // the direction of the ball
            Vector2f dir = Ball.Direction;
            dir.X *= -1;
            Ball.Direction = dir;
        }
    }

    /// <summary>
    /// Update the models
    /// </summary>
    /// <param name="deltaTime"></param>
    public void Update(float deltaTime)
    {
        // Update the paddles
        LeftPaddle.Update(deltaTime);
        RightPaddle.Update(deltaTime);

        // Check if the ball is colliding with any of the paddles
        CheckBallCollisionLeftPaddle();
        CheckBallCollisionRightPaddle();

        // Update the ball
        Ball.Update(deltaTime);
    }
}
