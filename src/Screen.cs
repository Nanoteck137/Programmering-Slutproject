using System;

using SFML.Graphics;

public abstract class Screen
{
    public abstract void Update(float deltaTime);
    public abstract void Render(RenderTarget renderTarget);
}
