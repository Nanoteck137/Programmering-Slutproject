using System;

using SFML.System;

public class Ball
{
    public Vector2f Position { get; private set; }
    public float Radius { get; private set; }

    public Ball(Vector2f position, float radius)
    {
        Position = position;
        Radius = radius;
    }

    public void Update(float deltaTime)
    {

    }
}
