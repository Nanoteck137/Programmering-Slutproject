using System;

using SFML.System;
using SFML.Graphics;

public class CustomizeScreen : Screen
{
    // The font used when rendering text
    private Font font;

    // The title in this screen
    private UIText title;

    // The back button, used for getting back to the main menu
    private UIButton backButton;

    // The left skin selector
    private UISkinSelector leftPaddleSelector;

    // The right skin selector
    private UISkinSelector rightPaddleSelector;

    public CustomizeScreen()
    {
        // Create and load the font needed for text rendering
        font = new Font("res/fonts/font.ttf");

        // Create the title
        CreateTitle();

        // Create the back button
        CreateBackButton();

        // Create the skin selectors
        CreateSkinSelectors();
    }

    private void CreateTitle()
    {
        // Get the window size
        Vector2u windowSize = Application.Instance.Window.Size;

        // Create the text and set the character size
        title = new UIText(new Vector2f(windowSize.X / 2.0f, 160.0f),
                           "customize.title", 50, font);
    }

    private void CreateBackButton()
    {
        // Get the window size
        Vector2u windowSize = Application.Instance.Window.Size;

        // Create the back button
        backButton = new UIButton(new Vector2f(windowSize.X / 2.0f,
                                               windowSize.Y - 120.0f),
                                  new Vector2f(240.0f, 60.0f),
                                  "customize.backbutton", 20, font);

        // Register a OnClick callback on the back button so the user 
        // can get back to the main menu
        backButton.RegisterOnClickAciton(OnBackButtonClicked);
    }

    private void CreateSkinSelectors()
    {
        // Get the window size
        Vector2u windowSize = Application.Instance.Window.Size;

        // Create the left skin selector 
        leftPaddleSelector = new UISkinSelector(
                                    new Vector2f(
                                        windowSize.X / 2.0f - 350.0f,
                                        windowSize.Y / 2.0f),
                                    font);

        // Create the left skin selector 
        rightPaddleSelector = new UISkinSelector(
                                    new Vector2f(
                                        windowSize.X / 2.0f + 350.0f,
                                        windowSize.Y / 2.0f),
                                    font);
    }

    private void OnBackButtonClicked()
    {
        // Change the screen back to the main menu
        ScreenManager.Instance.ChangeScreen(
            Application.Instance.MainMenuScreen);
    }

    public override void OnScreenShow()
    {
        // Reset the left skin id just to be sure
        leftPaddleSelector.PaddleSkinID
            = GameManager.Instance.LeftPaddleSkinID;

        // Reset the right skin id just to be sure
        rightPaddleSelector.PaddleSkinID =
            GameManager.Instance.RightPaddleSkinID;
    }

    public override void OnScreenHide()
    {
        // Save the left skin the user selected in the game manager
        GameManager.Instance.LeftPaddleSkinID
            = leftPaddleSelector.PaddleSkinID;

        // Save the right skin the user selected in the game manager
        GameManager.Instance.RightPaddleSkinID
            = rightPaddleSelector.PaddleSkinID;
    }

    public override void Update(float deltaTime)
    {
        // Update the back button
        backButton.Update(deltaTime);

        // Update the left skin selector
        leftPaddleSelector.Update(deltaTime);

        // Update the right skin selector
        rightPaddleSelector.Update(deltaTime);
    }

    public override void Render(RenderTarget renderTarget)
    {
        // Render the title
        title.Render(renderTarget);

        // Render the back button
        backButton.Render(renderTarget);

        // Render the left paddle selector
        leftPaddleSelector.Render(renderTarget);

        // Render the left paddle selector
        rightPaddleSelector.Render(renderTarget);
    }
}