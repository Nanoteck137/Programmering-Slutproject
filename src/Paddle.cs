using System;

using SFML.System;

public abstract class Paddle
{
    public Vector2f Position { get; protected set; }
    public Vector2f Size { get; protected set; }

    public abstract void Update(float deltaTime);
}
