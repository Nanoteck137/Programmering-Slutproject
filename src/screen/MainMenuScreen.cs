using System;
using System.Collections.Generic;

using SFML.System;
using SFML.Graphics;

/// <summary>
/// The MainMenu Screen handles and displays the main menu for the game
/// </summary>
public class MainMenuScreen : Screen
{
    // The font used in the main menu
    private Font font;

    // The title for the main menu
    private Text title;

    // Dictionary to store the translation key and the buttons, 
    // used to update and render but also when the language is changed 
    // so we can get the right translation for the button 
    private Dictionary<string, UIButton> menuButtons;

    // The width of a Menu button
    private const float BUTTON_WIDTH = 290;
    // The height of a Menu button
    private const float BUTTON_HEIGHT = 60;
    // The margin between buttons in the Y axis
    private const float BUTTON_Y_MARGIN = 20;

    // The position for the next button created
    private Vector2f nextButtonPosition;

    public MainMenuScreen()
    {
        // Initialize the `menuButtons` dictionary
        menuButtons = new Dictionary<string, UIButton>();

        // Create and load the `font` used in the game
        font = new Font("res/fonts/font.ttf");

        Vector2u windowSize = Application.Instance.Window.Size;

        // Initialize the `nextButtonPosition` to a start 
        // position for the buttons
        nextButtonPosition = new Vector2f(windowSize.X / 2.0f,
                                          windowSize.Y / 2.0f);

        // Create some buttons for the menu
        CreateMenuButton("mainmenu.playbutton", 20, OnPlayButtonClicked);
        CreateMenuButton("mainmenu.customizationbutton", 20,
                         OnCustomizeButtonClicked);
        CreateMenuButton("mainmenu.settingsbutton", 20,
                         OnSettingsButtonClicked);
        CreateMenuButton("mainmenu.quitbutton", 20, OnQuitButtonClicked);

        // Create the title
        CreateTitle();

        // Register a language changed callback
        LanguageManager.Instance.RegisterOnLanguageChangedCallback(
            OnLanguageChanged);
    }

    private void CreateTitle()
    {
        // Get the window size
        Vector2u windowSize = Application.Instance.Window.Size;

        // Create the text and set the character size
        title = new Text(
            LanguageManager.Instance.GetTranslation("mainmenu.title"), font);
        title.CharacterSize = 50;

        // Get the bounds for the text
        FloatRect rect = title.GetLocalBounds();
        // Set the origin to the center of the text
        title.Origin = new Vector2f(rect.Width / 2, rect.Height / 2);

        // Set the position of the title
        title.Position = new Vector2f(windowSize.X / 2.0f, 160.0f);
    }

    private void CreateMenuButton(string textKey, uint textSize,
                                  Action clickAction)
    {
        string text = LanguageManager.Instance.GetTranslation(textKey);
        // Create the button
        UIButton button = new UIButton(nextButtonPosition,
                                       new Vector2f(BUTTON_WIDTH,
                                                    BUTTON_HEIGHT),
                                       text, textSize, font);

        // Register the click callback
        button.RegisterOnClickAciton(clickAction);

        // Add the button to the list
        menuButtons.Add(textKey, button);

        // Update the `nextButtonPosition` so the next button 
        // can sit under this one
        nextButtonPosition.Y += BUTTON_HEIGHT + BUTTON_Y_MARGIN;
    }

    private void OnLanguageChanged()
    {
        // Update the title with the new translation
        title.DisplayedString =
            LanguageManager.Instance.GetTranslation("mainmenu.title");

        // Get the bounds for the text for the new text
        FloatRect rect = title.GetLocalBounds();
        // Set the origin to the center of the text
        title.Origin = new Vector2f(rect.Width / 2, rect.Height / 2);

        // Loop through the buttons and update the content for the new language
        foreach (var button in menuButtons)
        {
            // Get and set the new translation for the button
            button.Value.Text =
                LanguageManager.Instance.GetTranslation(button.Key);
        }
    }

    private void OnPlayButtonClicked()
    {
        // Change the screen to the gameplay screen
        ScreenManager.Instance.ChangeScreen(Application.Instance.GameScreen);
    }

    private void OnCustomizeButtonClicked()
    {
        // Change the screen to the customize screen
        ScreenManager.Instance.ChangeScreen(
            Application.Instance.CustomizeScreen);
    }

    private void OnSettingsButtonClicked()
    {
        // Change the screen to the settings screen
        ScreenManager.Instance.ChangeScreen(
            Application.Instance.SettingsScreen);
    }

    private void OnQuitButtonClicked()
    {
        // Let the application handle the exiting of the game
        Application.Instance.ExitGame();
    }

    public override void Update(float deltaTime)
    {
        // Update all the menu buttons
        foreach (var button in menuButtons)
            button.Value.Update(deltaTime);
    }

    public override void Render(RenderTarget renderTarget)
    {
        // Render the title
        renderTarget.Draw(title);

        // Render all the menu buttons
        foreach (var button in menuButtons)
            button.Value.Render(renderTarget);
    }


}
