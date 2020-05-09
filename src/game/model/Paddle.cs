using System;

using SFML.System;

/// <summary>
/// This abstract class represents a Paddle
/// </summary>
public abstract class Paddle
{
    // The Position of the Paddle in the world
    public Vector2f Position { get; protected set; }
    // The Size of the Paddle in the world
    public Vector2f Size { get; protected set; }

    /// <summary>
    /// Update the paddle
    /// </summary>
    /// <param name="deltaTime"></param>
    public abstract void Update(float deltaTime);
}
