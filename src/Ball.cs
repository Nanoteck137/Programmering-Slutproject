using System;

using SFML.Graphics;
using SFML.System;

public class Ball
{
    public Vector2f Position { get; private set; }
    public float Radius { get; private set; }

    private Vector2f direction;

    public Ball(Vector2f position, float radius)
    {
        Position = position;
        Radius = radius;

        Random random = new Random();
        direction = new Vector2f(1, 1);
        //direction = new Vector2f(1.0f, 0.0f);
    }

    public void Update(float deltaTime)
    {
        float speed = 300.0f;

        Position += direction * speed * deltaTime;

        Vector2u windowSize = Application.Instance.Window.Size;
        if (Position.X < 0.0f)
        {
            direction.X *= -1;
        }

        if (Position.X + Radius > windowSize.X)
        {
            direction.X *= -1;
        }

        if (Position.Y < 0.0f)
        {
            direction.Y *= -1;
        }

        if (Position.Y + Radius > windowSize.Y)
        {
            direction.Y *= -1;
        }
    }
}
