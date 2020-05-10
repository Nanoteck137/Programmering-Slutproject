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
    // The skin ID so we can later look up what skin the paddle should have
    public int SkinID { get; set; }

    /// <summary>
    /// Update the paddle
    /// </summary>
    /// <param name="deltaTime"></param>
    public abstract void Update(float deltaTime);
}
