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

        LanguageManager.Instance.RegisterOnLanguageChangedCallback(
            OnLanguageChanged);
    }

    private void CreateTitle()
    {
        // Get the window size
        Vector2u windowSize = Application.Instance.Window.Size;

        // Create the text and set the character size
        title = new Text(
            LanguageManager.Instance.GetTranslation("customize.title"), font);
        title.CharacterSize = 50;

        // Get the bounds for the text
        FloatRect rect = title.GetLocalBounds();
        // Set the origin to the center of the text
        title.Origin = new Vector2f(rect.Width / 2, rect.Height / 2);

        // Set the position of the title
        title.Position = new Vector2f(windowSize.X / 2.0f, 160.0f);
    }

    private void CreateBackButton()
    {
        // Get the window size
        Vector2u windowSize = Application.Instance.Window.Size;

        string text =
            LanguageManager.Instance.GetTranslation("customize.backbutton");

        // Create the back button
        backButton = new UIButton(new Vector2f(windowSize.X / 2.0f,
                                               windowSize.Y - 120.0f),
                                  new Vector2f(240.0f, 60.0f),
                                  text, 20, font);

        // Register the back button click callback
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

    private void OnLanguageChanged()
    {
        // Set the title to the new translation
        title.DisplayedString =
            LanguageManager.Instance.GetTranslation("customize.title");

        // Get the bounds of the text
        FloatRect rect = title.GetLocalBounds();
        // Set the origin to the center of the text
        title.Origin = new Vector2f(rect.Width / 2, rect.Height / 2);

        backButton.Text =
            LanguageManager.Instance.GetTranslation("customize.backbutton");
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
        renderTarget.Draw(title);

        // Render the back button
        backButton.Render(renderTarget);

        // Render the left paddle selector
        leftPaddleSelector.Render(renderTarget);

        // Render the left paddle selector
        rightPaddleSelector.Render(renderTarget);
    }
}