using System;

using SFML.System;
using SFML.Window;
using SFML.Graphics;

/// <summary>
/// This class represent a button and handles all the things a 
/// button need to function like if the mouse is in bounds and if user 
/// has clicked it
/// </summary>
class UIButton
{
    // A RectangleShape used for the rendering of the primitive of the button
    private RectangleShape shape;
    // A Text Object used to render text in SFML
    private Text text;

    // Action used for if the button is clicked on then the button can 
    // send out an event to the registered users
    private Action onClickAction;

    /// <summary>
    /// The constructor of the button
    /// </summary>
    /// <param name="position">The position of the button</param>
    /// <param name="size">The size of the button</param>
    /// <param name="text">The text inside the button</param>
    /// <param name="textSize">The text size</param>
    /// <param name="font">The font used for the text</param>
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

    /// <summary>
    /// Register a callback function to get called on then the button 
    /// is clicked on
    /// </summary>
    /// <param name="action">The callback function to get registered</param>
    public void RegisterOnClickAciton(Action action)
    {
        // Add the callback function to the `onClickAction` action
        onClickAction += action;
    }

    /// <summary>
    /// Unregister a callback function
    /// </summary>
    /// <param name="action">The callback function to get unregisted</param>
    public void UnregisterOnClickAction(Action action)
    {
        // Remove the callback function to the `onClickAction` action
        onClickAction -= action;
    }

    /// <summary>
    /// Updates the button, checks if the mouse is in bounds and 
    /// if the user has clicked the button
    /// </summary>
    /// <param name="deltaTime"></param>
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

    /// <summary>
    /// Render the button
    /// </summary>
    /// <param name="renderTarget">The RenderTarget the button 
    /// should render to</param>
    public void Render(RenderTarget renderTarget)
    {
        // Render the primitive shape of the button
        renderTarget.Draw(shape);

        // Render the text inside the button
        renderTarget.Draw(text);
    }
}