using System;

using SFML.System;
using SFML.Graphics;

public class UISkinSelector
{
    // Create some constants for the paddle size
    const float PADDLE_WIDTH = 16.0f;
    const float PADDLE_HEIGHT = 200.0f;

    // Create some constants for the skin changing buttons
    const float CHANGE_BUTTON_WIDTH = 45.0f;
    const float CHANGE_BUTTON_HEIGHT = 45.0f;

    // The shape for the preview
    private RectangleShape paddlePreview;

    // The left button to change the skin on the preview
    private UIButton skinChangeLeft;

    // The right button to change the skin on the preview
    private UIButton skinChangeRight;

    // The paddle skin id used to look up the skin when rendering a paddle
    private int paddleSkinID = 0;

    public int PaddleSkinID
    {
        get { return paddleSkinID; }
        set { paddleSkinID = value; }
    }

    public UISkinSelector(Vector2f position, Font font)
    {
        // Create the paddle preview
        paddlePreview = new RectangleShape(new Vector2f(PADDLE_WIDTH,
                                                        PADDLE_HEIGHT));

        // Set the paddle preview origin to the center of the rectangle
        paddlePreview.Origin = new Vector2f(PADDLE_WIDTH / 2.0f,
                                            PADDLE_HEIGHT / 2.0f);

        // Set the position of the left preview paddle
        paddlePreview.Position = new Vector2f(position.X, position.Y);

        // Create the left skin change button
        skinChangeLeft = new UIButton(new Vector2f(
                                            position.X - 100.0f,
                                            position.Y),
                                      new Vector2f(
                                            CHANGE_BUTTON_WIDTH,
                                            CHANGE_BUTTON_HEIGHT),
                                      "skinselector.left", 25, font);

        // Register the callback when the button is clicked on
        skinChangeLeft.RegisterOnClickAciton(OnSkinChangeLeft);

        // Create the right skin change button
        skinChangeRight = new UIButton(new Vector2f(
                                            position.X + 100,
                                            position.Y),
                                       new Vector2f(
                                            CHANGE_BUTTON_WIDTH,
                                            CHANGE_BUTTON_HEIGHT),
                                       "skinselector.right", 25, font);

        // Register the callback when the button is clicked on
        skinChangeRight.RegisterOnClickAciton(OnSkinChangeRight);
    }

    /// <summary>
    /// Called when the user clicks the left change button
    /// </summary>
    private void OnSkinChangeLeft()
    {
        /// Decrement the skin id and check if the skin id is out of bounds 
        paddleSkinID--;
        if (paddleSkinID < 0)
            paddleSkinID = 0;
    }

    /// <summary>
    /// Called when the user clicks the right change button
    /// </summary>
    private void OnSkinChangeRight()
    {
        /// Increment the skin id and check if the skin id is out of bounds 
        paddleSkinID++;
        if (paddleSkinID >= SkinManager.Instance.NumSkins)
            paddleSkinID = SkinManager.Instance.NumSkins - 1;

    }

    /// <summary>
    /// Update the selector
    /// </summary>
    /// <param name="deltaTime"></param>
    public void Update(float deltaTime)
    {
        // Update the skin change left button
        skinChangeLeft.Update(deltaTime);
        // Update the skin change right button
        skinChangeRight.Update(deltaTime);
    }

    /// <summary>
    /// Render the selector
    /// </summary>
    /// <param name="renderTarget">The RenderTarget the selector should 
    /// render to</param>
    public void Render(RenderTarget renderTarget)
    {
        // Get the skin and apply it to the preview
        Skin skin = SkinManager.Instance.GetSkinFromID(paddleSkinID);
        paddlePreview.FillColor = skin.Color;

        // Render the paddle preview
        renderTarget.Draw(paddlePreview);

        // Render the skinChangeLeft button
        skinChangeLeft.Render(renderTarget);

        // Render the skinChangeRight button
        skinChangeRight.Render(renderTarget);
    }
}