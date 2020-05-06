using System;

using SFML.System;
using SFML.Window;
using SFML.Graphics;

class UIButton
{
    private RectangleShape shape;

    public UIButton(Vector2f position)
    {
        shape = new RectangleShape(new Vector2f(200, 60));
        shape.Position = position;
        shape.FillColor = Color.Black;
        shape.OutlineColor = Color.White;
        shape.OutlineThickness = 4.0f;
    }

    public void Update(float deltaTime)
    {
        Vector2f mousePos = Application.Instance.Window.MapPixelToCoords(Mouse.GetPosition(Application.Instance.Window));

        if (shape.GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
        {
            shape.OutlineColor = Color.Red;
        }
        else
        {
            shape.OutlineColor = Color.White;
        }
    }

    public void Render(RenderTarget renderTarget)
    {
        renderTarget.Draw(shape);
    }
}