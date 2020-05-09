using System;

using SFML.System;
using SFML.Window;
using SFML.Graphics;

class UIButton
{
    private RectangleShape shape;

    private bool prevButtonDown = false;

    private Action onClickAction;

    public UIButton(Vector2f position)
    {
        shape = new RectangleShape(new Vector2f(200, 60));
        shape.Position = position;
        shape.FillColor = Color.Black;
        shape.OutlineColor = Color.White;
        shape.OutlineThickness = 4.0f;
    }

    public void RegisterOnClickAciton(Action action)
    {
        onClickAction += action;
    }

    public void UnregisterOnClickAction(Action action)
    {
        onClickAction -= action;
    }

    public void Update(float deltaTime)
    {
        Vector2f mousePos = Application.Instance.Window.MapPixelToCoords(Mouse.GetPosition(Application.Instance.Window));

        if (Input.Instance.IsButtonDown(Input.LEFT_BUTTON))
        {
            Console.WriteLine("Hello World");
        }

        if (shape.GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
        {
            if (!prevButtonDown && Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                if (onClickAction != null)
                    onClickAction();

                prevButtonDown = true;
            }
            else
            {
                shape.OutlineColor = Color.Red;
            }
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