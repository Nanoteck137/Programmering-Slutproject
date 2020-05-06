using System;

using SFML.Graphics;
using SFML.System;

public class Ball
{
    public Vector2f Position { get; private set; }
    public float Radius { get; private set; }

    public Vector2f Direction { get; set; }

    public Ball(Vector2f position, float radius)
    {
        Position = position;
        Radius = radius;

        Random random = new Random();
        Direction = new Vector2f(1, 1);
        //direction = new Vector2f(1.0f, 0.0f);
    }

    public void Update(float deltaTime)
    {
        float speed = 300.0f;

        Position += Direction * speed * deltaTime;

        Vector2u windowSize = Application.Instance.Window.Size;

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
