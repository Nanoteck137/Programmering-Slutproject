using System;

using SFML.System;
using SFML.Graphics;

public class TestScreen01 : Screen
{
    private RectangleShape shape;

    public TestScreen01()
    {
        shape = new RectangleShape(new Vector2f(32.0f, 32.0f))
        {
            FillColor = Color.Yellow
        };
    }

    public override void Update(float deltaTime)
    {
    }

    public override void Render(RenderTarget renderTarget)
    {
        renderTarget.Draw(shape);
    }
}
