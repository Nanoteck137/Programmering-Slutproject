using System;

using SFML.System;
using SFML.Graphics;

public class CustomizeScreen : Screen
{
    // The font used when rendering text
    private Font font;

    // The title in this screen
    private Text title;

    // The back button, used for getting back to the main menu
    private UIButton backButton;

    // A preview of the left paddle, used to see the skin selected by the user
    private RectangleShape leftPaddlePreview;
    private UIButton skinChangeLeft;
    private UIButton skinChangeRight;

    // The left paddle skin id used to look up the skin when rendering a paddle
    private int leftPaddleSkinID = 0;

    public CustomizeScreen()
    {
        // Create and load the font needed for text rendering
        font = new Font("res/fonts/font.ttf");

        // Create the title
        CreateTitle();

        // Get the window size
        Vector2u windowSize = Application.Instance.Window.Size;

        // Create the back button
        backButton = new UIButton(new Vector2f(windowSize.X / 2.0f,
                                               windowSize.Y - 120.0f),
                                  new Vector2f(240.0f, 60.0f),
                                  "Back", 20, font);

        // Register the back button click callback
        backButton.RegisterOnClickAciton(OnBackButtonClicked);

        // Create some constants for the paddle size
        const float PADDLE_WIDTH = 16.0f;
        const float PADDLE_HEIGHT = 200.0f;

        // Create the left paddle preview
        leftPaddlePreview = new RectangleShape(new Vector2f(PADDLE_WIDTH,
                                                            PADDLE_HEIGHT));

        // Set the left paddle preview origin to the center of the rectangle
        leftPaddlePreview.Origin = new Vector2f(PADDLE_WIDTH / 2.0f,
                                                PADDLE_HEIGHT / 2.0f);

        // Set the position of the left preview paddle
        leftPaddlePreview.Position = new Vector2f(windowSize.X / 2.0f - 350.0f,
                                                  windowSize.Y / 2.0f);

        skinChangeLeft = new UIButton(new Vector2f(
                                            windowSize.X / 2.0f - 450.0f,
                                            windowSize.Y / 2.0f),
                                      new Vector2f(60.0f, 60.0f),
                                      "<", 20, font);
        skinChangeLeft.RegisterOnClickAciton(OnSkinChangeLeft);

        skinChangeRight = new UIButton(new Vector2f(
                                            windowSize.X / 2.0f - 250.0f,
                                            windowSize.Y / 2.0f),
                                      new Vector2f(60.0f, 60.0f),
                                      ">", 20, font);
        skinChangeRight.RegisterOnClickAciton(OnSkinChangeRight);
    }

    private void CreateTitle()
    {
        // Get the window size
        Vector2u windowSize = Application.Instance.Window.Size;

        // Create the text and set the character size
        title = new Text("Customize", font);
        title.CharacterSize = 50;

        // Get the bounds for the text
        FloatRect rect = title.GetLocalBounds();
        // Set the origin to the center of the text
        title.Origin = new Vector2f(rect.Width / 2, rect.Height / 2);

        // Set the position of the title
        title.Position = new Vector2f(windowSize.X / 2.0f, 160.0f);
    }

    private void OnBackButtonClicked()
    {
        // Change the screen back to the main menu
        ScreenManager.Instance.ChangeScreen(
            Application.Instance.MainMenuScreen);
    }

    private void OnSkinChangeLeft()
    {
        /// Decrement the skin id and check if the skin id is out of bounds 
        leftPaddleSkinID--;
        if (leftPaddleSkinID < 0)
            leftPaddleSkinID = 0;
    }

    private void OnSkinChangeRight()
    {
        /// Increment the skin id and check if the skin id is out of bounds 
        leftPaddleSkinID++;
        if (leftPaddleSkinID >= SkinManager.Instance.NumSkins)
            leftPaddleSkinID = SkinManager.Instance.NumSkins - 1;
    }

    public override void OnScreenShow()
    {
        // Reset the skin id to be sure
        leftPaddleSkinID = GameManager.Instance.LeftPaddleSkinID;
    }

    public override void OnScreenHide()
    {
        // Save the skin the user selected in the game manager
        GameManager.Instance.LeftPaddleSkinID = leftPaddleSkinID;
    }

    public override void Update(float deltaTime)
    {
        // Update the back button
        backButton.Update(deltaTime);

        // Update the skin change left button
        skinChangeLeft.Update(deltaTime);
        // Update the skin change right button
        skinChangeRight.Update(deltaTime);
    }

    public override void Render(RenderTarget renderTarget)
    {
        // Render the title
        renderTarget.Draw(title);

        // Render the back button
        backButton.Render(renderTarget);

        Skin skin = SkinManager.Instance.GetSkinFromID(leftPaddleSkinID);
        leftPaddlePreview.FillColor = skin.Color;

        // Render the paddle preview
        renderTarget.Draw(leftPaddlePreview);

        // Render the skinChangeLeft button
        skinChangeLeft.Render(renderTarget);

        // Render the skinChangeRight button
        skinChangeRight.Render(renderTarget);
    }
}