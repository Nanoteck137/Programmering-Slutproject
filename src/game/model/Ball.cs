using System;

using SFML.Graphics;
using SFML.System;

/// <summary>
/// This class is the data used for the ball in the game
/// </summary>
public class Ball
{
    // The position of the ball in the world
    private Vector2f position;

    // The radius of the ball
    private float radius;

    // The direction the ball is traveling in
    private Vector2f direction;

    public Vector2f Position
    {
        get { return position; }
        set { position = value; }
    }
    public Vector2f Direction
    {
        get { return direction; }
        set { direction = value; }
    }
    public float Radius { get { return radius; } }

    public event Action OnLeftSideHit;
    public event Action OnRightSideHit;

    /// <summary>
    /// Initializes the ball
    /// </summary>
    /// <param name="position">The initial position of the ball</param>
    /// <param name="radius">The radius of the ball</param>
    public Ball(Vector2f position, float radius)
    {
        // Sets the position and radius
        this.position = position;
        this.radius = radius;

        // Set the direction
        this.direction = new Vector2f(1, 1);
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
        position += direction * speed * deltaTime;

        // Get the window size
        Vector2u windowSize = Application.Instance.Window.Size;

        // Check if the ball is going out of the bounds of the window, 
        // and if it is then change the direction the ball is traveling at
        if (position.X - radius < 0.0f)
        {
            // dir.X *= -1;
            if (OnLeftSideHit != null)
                OnLeftSideHit();
        }

        if (position.X + radius > windowSize.X)
        {
            // dir.X *= -1;
            if (OnRightSideHit != null)
                OnRightSideHit();
        }

        if (position.Y - radius < 0.0f)
        {
            direction.Y *= -1;
        }

        if (position.Y + radius > windowSize.Y)
        {
            direction.Y *= -1;
        }
    }
}
