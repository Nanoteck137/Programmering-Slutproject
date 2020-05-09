using System;

using SFML.Graphics;
using SFML.System;

/// <summary>
/// This class is the data used for the ball in the game
/// </summary>
public class Ball
{
    // The position of the ball in the world
    public Vector2f Position { get; private set; }
    // The radius of the ball
    public float Radius { get; private set; }

    // The direction the ball is traveling in
    public Vector2f Direction { get; set; }

    /// <summary>
    /// Initializes the ball
    /// </summary>
    /// <param name="position">The initial position of the ball</param>
    /// <param name="radius">The radius of the ball</param>
    public Ball(Vector2f position, float radius)
    {
        // Sets the position and radius
        Position = position;
        Radius = radius;

        // Set the direction
        Direction = new Vector2f(1, 1);
    }

    /// <summary>
    /// Update the ball so it moves in the direction specified
    /// </summary>
    /// <param name="deltaTime"></param>
    public void Update(float deltaTime)
    {
        // The speed the ball should travel at
        float speed = 300.0f;

        // Update the position with use of the direction, speed and deltaTime
        Position += Direction * speed * deltaTime;

        // Get the window size
        Vector2u windowSize = Application.Instance.Window.Size;

        // Check if the ball is going out of the bounds of the window, 
        // and if it is then change the direction the ball is traveling at
        Vector2f dir = Direction;
        if (Position.X < 0.0f)
        {
            dir.X *= -1;
        }

        if (Position.X + Radius > windowSize.X)
        {
            dir.X *= -1;
        }

        if (Position.Y - Radius < 0.0f)
        {
            dir.Y *= -1;
        }

        if (Position.Y + Radius > windowSize.Y)
        {
            dir.Y *= -1;
        }

        Direction = dir;
    }
}
