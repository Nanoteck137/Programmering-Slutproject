using System;
using System.Collections.Generic;

using SFML.System;
using SFML.Graphics;

public class SettingsScreen : Screen
{
    // The font is used to render the text in this screen
    // TODO(patrik) : Maybe move the font to a commen place because it's 
    // used more then once
    private Font font;

    // The title of the screen
    private UIText title;
    // The change language title is a subtitle 
    private UIText changeLanguageTitle;

    // List of buttons who change the language to the button assign language
    private List<UIButton> languageButtons;

    // The back button sends the user back to the main menu
    private UIButton backButton;

    public SettingsScreen()
    {
        // Create and load the font needed for text rendering
        font = new Font("res/fonts/font.ttf");

        // Initialize the list
        languageButtons = new List<UIButton>();

        // Create the titles
        CreateTitles();

        // Create the language butttons and add it to the list
        CreateLanguageButtons();

        // Create the back button
        CreateBackButton();
    }

    private void CreateTitles()
    {
        // Get the window size
        Vector2u windowSize = Application.Instance.Window.Size;

        // Create the title text 
        title = new UIText(new Vector2f(windowSize.X / 2.0f, 160.0f),
                           "settings.title", 50, font);

        // Create the Change language title text
        changeLanguageTitle = new UIText(
                                    new Vector2f(windowSize.X / 2.0f, 250.0f),
                                    "settings.changelanguagetitle", 30, font);
    }

    private void CreateLanguageButtons()
    {
        // Get the window size
        Vector2u windowSize = Application.Instance.Window.Size;

        // Get a copy of the languages in the language manager
        var languages = LanguageManager.Instance.Languages;

        // Store the next y position for a button
        float nextY = 0.0f;

        // Loop through all the language and create a button, so the user 
        // can change the language
        foreach (var language in languages)
        {
            // The language name
            string languageName = language.Key;

            // Create a button assosiated with the language
            UIButton button = new UIButton(
                                    new Vector2f(windowSize.X / 2.0f,
                                                 windowSize.Y / 2.0f + nextY),
                                    new Vector2f(240.0f, 60.0f),
                                    languageName,
                                    20,
                                    font);

            // Register an OnClick on the button so the button can change 
            // the language
            button.RegisterOnClickAciton(() =>
            {
                LanguageManager.Instance.SetCurrentLanguage(languageName);
            });

            // Add the button to the list
            languageButtons.Add(button);

            // Advance Y so a new button sits properly
            nextY += 60.0f + 20.0f;
        }
    }

    private void CreateBackButton()
    {
        // Get the window size
        Vector2u windowSize = Application.Instance.Window.Size;

        // Get the current translation for the back button
        string text =
            LanguageManager.Instance.GetTranslation("settings.backbutton");

        // Create the back button
        backButton = new UIButton(new Vector2f(windowSize.X / 2.0f,
                                               windowSize.Y - 120.0f),
                                  new Vector2f(240.0f, 60.0f),
                                  text, 20, font);

        // Register the back button click callback
        backButton.RegisterOnClickAciton(OnBackButtonClicked);

        // Register the OnClick so the button can send the user back to 
        // the main menu
        LanguageManager.Instance.RegisterOnLanguageChangedCallback(
            OnLanguageChanged);
    }

    private void OnLanguageChanged()
    {
        backButton.Text =
            LanguageManager.Instance.GetTranslation("settings.backbutton");
    }

    private void OnBackButtonClicked()
    {
        // Change the screen back to the main menu
        ScreenManager.Instance.ChangeScreen(
            Application.Instance.MainMenuScreen);
    }


    public override void Update(float deltaTime)
    {
        foreach (UIButton button in languageButtons)
            button.Update(deltaTime);

        backButton.Update(deltaTime);
    }

    public override void Render(RenderTarget renderTarget)
    {
        title.Render(renderTarget);
        changeLanguageTitle.Render(renderTarget);

        foreach (UIButton button in languageButtons)
            button.Render(renderTarget);

        backButton.Render(renderTarget);
    }
}