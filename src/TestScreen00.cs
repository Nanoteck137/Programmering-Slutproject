using System;

using SFML.System;
using SFML.Graphics;

public class TestScreen00 : Screen
{
    private RectangleShape shape;

    public TestScreen00()
    {
        shape = new RectangleShape(new Vector2f(32.0f, 32.0f))
        {
            FillColor = Color.Blue
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
