using System;

using SFML.Graphics;

/// <summary>
/// An abstract class that represent a screen
/// </summary>
public abstract class Screen
{
    /// <summary>
    /// Update the screen
    /// </summary>
    /// <param name="deltaTime"></param>
    public abstract void Update(float deltaTime);

    /// <summary>
    /// Render the screen
    /// </summary>
    /// <param name="renderTarget">The RenderTarget the screen should 
    /// render to</param>
    public abstract void Render(RenderTarget renderTarget);
}
