using System;

using SFML.System;
using SFML.Window;
using SFML.Graphics;

class UIButton
{
    private RectangleShape shape;
    private Text text;

    private Action onClickAction;

    public UIButton(Vector2f position, Vector2f size, String text,
                    uint textSize, Font font)
    {
        shape = new RectangleShape(size);
        shape.Origin = size / 2.0f;

        shape.Position = position;
        shape.FillColor = Color.Black;
        shape.OutlineColor = Color.White;
        shape.OutlineThickness = 4.0f;

        this.text = new Text(text, font);
        this.text.CharacterSize = textSize;

        Vector2f textPos = position;
        FloatRect rect = this.text.GetLocalBounds();

        this.text.Origin = new Vector2f(rect.Width / 2.0f, rect.Height / 2.0f);
        this.text.Position = textPos;
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
        Vector2f mousePos = Application.Instance.Window.MapPixelToCoords(
                                Mouse.GetPosition(Application.Instance.Window));

        if (shape.GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
        {
            if (Input.Instance.IsButtonPressed(Input.LEFT_BUTTON))
            {
                if (onClickAction != null)
                    onClickAction();
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
        renderTarget.Draw(text);
    }
}