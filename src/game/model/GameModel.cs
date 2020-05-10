using System;

using SFML.System;
using SFML.Window;

/// <summary>
/// This class holds all the data for the game
/// </summary>
public class GameModel
{
    // The left paddle
    private Paddle leftPaddle;

    // The right paddle
    private Paddle rightPaddle;

    // The score of the left side
    private int leftSideScore;

    // The score of the right side
    private int rightSideScore;

    // The ball
    private Ball ball;

    public Paddle LeftPaddle { get { return leftPaddle; } }
    public Paddle RightPaddle { get { return rightPaddle; } }
    public int LeftSideScore { get { return leftSideScore; } }
    public int RightSideScore { get { return rightSideScore; } }
    public Ball Ball { get { return ball; } }

    /// <summary>
    /// Used for initializing the game data
    /// </summary>
    public GameModel()
    {
        // Get the window size
        Vector2u windowSize = Application.Instance.Window.Size;

        // Create the ball
        float ballRadius = 20.0f;
        ball = new Ball(new Vector2f(windowSize.X / 2, windowSize.Y / 2), ballRadius);
        ball.OnLeftSideHit += this.OnBallHitLeftSide;
        ball.OnRightSideHit += this.OnBallHitRightSide;

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
        leftPaddle = new PlayerPaddle(leftConfig);

        // Setup the right paddle config
        PlayerPaddle.Config rightConfig = new PlayerPaddle.Config()
        {
            InitialPosition = new Vector2f(windowSize.X - offset, windowSize.Y / 2),
            Size = new Vector2f(paddleWidth, paddleHeight),
            UpKey = Keyboard.Key.Up,
            DownKey = Keyboard.Key.Down,
        };

        // Create the paddle
        rightPaddle = new PlayerPaddle(rightConfig);
    }

    private void OnBallHitLeftSide()
    {
        // Increment the score for the right side
        rightSideScore++;

        // Reset the round
        ResetBall();
    }

    private void OnBallHitRightSide()
    {
        // Increment the score for the left side
        leftSideScore++;

        // Reset the round
        ResetBall();
    }

    private void ResetBall()
    {
        // Get the window size
        Vector2u windowSize = Application.Instance.Window.Size;

        // Reset the position of the ball
        ball.Position = new Vector2f(windowSize.X / 2, windowSize.Y / 2);
    }

    /// <summary>
    /// Check if the ball is colliding with the left paddle
    /// </summary>
    private void CheckBallCollisionLeftPaddle()
    {
        // Check if the ball is colliding with the left paddle
        if (ball.Position.X - ball.Radius < leftPaddle.Position.X + leftPaddle.Size.X / 2 &&
            ball.Position.X - ball.Radius > leftPaddle.Position.X &&
            ball.Position.Y + ball.Radius >= leftPaddle.Position.Y - leftPaddle.Size.Y / 2 &&
            ball.Position.Y - ball.Radius <= leftPaddle.Position.Y + leftPaddle.Size.Y / 2)
        {
            // If the ball is colliding with the left paddle then change 
            // the direction of the ball
            Vector2f dir = ball.Direction;
            dir.X *= -1;
            ball.Direction = dir;
        }
    }

    /// <summary>
    /// Check if the ball is colliding with the right paddle
    /// </summary>
    private void CheckBallCollisionRightPaddle()
    {
        // Check if the ball is colliding with the right paddle
        if (ball.Position.X + ball.Radius < rightPaddle.Position.X + rightPaddle.Size.X / 2 &&
            ball.Position.X + ball.Radius > rightPaddle.Position.X &&
            ball.Position.Y + ball.Radius >= rightPaddle.Position.Y - rightPaddle.Size.Y / 2 &&
            ball.Position.Y - ball.Radius <= rightPaddle.Position.Y + rightPaddle.Size.Y / 2)
        {
            // If the ball is colliding with the right paddle then change 
            // the direction of the ball
            Vector2f dir = ball.Direction;
            dir.X *= -1;
            ball.Direction = dir;
        }
    }

    /// <summary>
    /// Update the models
    /// </summary>
    /// <param name="deltaTime"></param>
    public void Update(float deltaTime)
    {
        // Update the paddles
        leftPaddle.Update(deltaTime);
        rightPaddle.Update(deltaTime);

        // Check if the ball is colliding with any of the paddles
        CheckBallCollisionLeftPaddle();
        CheckBallCollisionRightPaddle();

        // Update the ball
        ball.Update(deltaTime);
    }
}
