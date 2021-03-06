﻿using System;

using SFML.Window;
using SFML.Graphics;
using SFML.System;
using System.Diagnostics;
using System.Collections.Generic;

/// <summary>
/// This class is used to manage the lifetime of the game and it's components
/// </summary>
public class Application
{
    // Store the instance of the application should always be 
    // one application running
    private static Application instance;
    public static Application Instance { get { return instance; } }

    // The SFML RenderWindow used for the rendering of the application
    private RenderWindow window;

    // Store the MainMenuScreen so we don't need to create a new one 
    // always when changing screens
    private MainMenuScreen mainMenuScreen;

    // Store the CustomizeScreen so we don't need to create a new one 
    // always when changing screens
    private CustomizeScreen customizeScreen;

    // Store the GameScreen so we don't need to create a new one 
    // always when changing screens
    private GameScreen gameScreen;

    // Store the SettingsScreen so we don't need to create a new one 
    // always when changing screens
    private SettingsScreen settingsScreen;

    // Store the state if the application is running, and chaging this 
    // to false when the application is running should exit the app
    private bool running = false;

    public RenderWindow Window { get { return window; } }
    public MainMenuScreen MainMenuScreen { get { return mainMenuScreen; } }
    public CustomizeScreen CustomizeScreen { get { return customizeScreen; } }
    public GameScreen GameScreen { get { return gameScreen; } }
    public SettingsScreen SettingsScreen { get { return settingsScreen; } }

    /// <summary>
    /// The constructor for the application, initialize all the components 
    /// needed for the app
    /// </summary>
    public Application()
    {
        // Check if there is another instance, becuase there should never be one
        Debug.Assert(instance == null);

        // Set the instance to this class because it should 
        // only be one instance
        instance = this;

        // Set the language to en_US as default
        LanguageManager.Instance.SetCurrentLanguage("en_US");

        Styles windowStyle = Styles.Titlebar | Styles.Close;

        string windowTitle = LanguageManager.Instance.GetTranslation("window.title");

        // Create a RenderWindow and setup the event callbacks
        window = new RenderWindow(new VideoMode(1280, 720),
                                  windowTitle, windowStyle);

        // Register some callbacks
        window.Closed += this.Window_Closed;
        window.KeyPressed += this.Window_KeyPressed;
        window.KeyReleased += this.Window_KeyReleased;
        window.MouseButtonPressed += this.Window_MouseButtonPressed;
        window.MouseButtonReleased += this.Window_MouseButtonReleased;

        // Create the screens used by this application
        mainMenuScreen = new MainMenuScreen();
        customizeScreen = new CustomizeScreen();
        gameScreen = new GameScreen();
        settingsScreen = new SettingsScreen();

        // Change the screen to the MainMenu
        ScreenManager.Instance.ChangeScreen(mainMenuScreen);

        // Register a language changed callback
        LanguageManager.Instance.RegisterOnLanguageChangedCallback(OnLanguageChanged);
    }

    /// <summary>
    /// Called when the language is changed so we can update the text 
    /// inside objects
    /// </summary>
    private void OnLanguageChanged()
    {
        Window.SetTitle(
            LanguageManager.Instance.GetTranslation("window.title"));
    }

    /// <summary>
    /// If called then the game should exit
    /// </summary>
    public void ExitGame()
    {
        // Just setting the running to false should exit the application
        running = false;
    }

    /// <summary>
    /// Called when the app should start running
    /// </summary>
    public void Run()
    {
        // Set running to true, so the app can start the processing
        running = true;

        // The clock is used too calculate the time taken since the last frame
        Clock clock = new Clock();

        // The game loop
        while (running)
        {
            // Get the deltatime from the clock
            float deltaTime = clock.Restart().AsSeconds();

            // Dispatch the window events to the callback registered 
            // in the constructor
            window.DispatchEvents();

            // Clear the window
            window.Clear(Color.Black);

            // Update and Render the SceneManager
            ScreenManager.Instance.Update(deltaTime);
            ScreenManager.Instance.Render(window);

            // Update the GameManager
            GameManager.Instance.Update(deltaTime);

            // Display the contents to the window
            window.Display();

            // Update the input last so the keys gets updated 
            // in the right order
            Input.Instance.Update();
        }

        clock.Dispose();
        window.Dispose();
    }

    private void Window_Closed(object sender, EventArgs e)
    {
        // When the window wants to be closed, just exit the game
        ExitGame();
    }

    private void Window_KeyPressed(object sender, KeyEventArgs e)
    {
        Input.Instance.SetKeyState((int)e.Code, true);
    }

    private void Window_KeyReleased(object sender, KeyEventArgs e)
    {
        Input.Instance.SetKeyState((int)e.Code, false);
    }

    private void Window_MouseButtonPressed(object sender,
                                           MouseButtonEventArgs e)
    {
        // Update the button state in Input to true for the specific button
        Input.Instance.SetButtonState((int)e.Button, true);
    }

    private void Window_MouseButtonReleased(object sender,
                                            MouseButtonEventArgs e)
    {
        // Update the button state in Input to false for the specific button
        Input.Instance.SetButtonState((int)e.Button, false);
    }

    static void Main(string[] args)
    {
        // Create an application
        Application app = new Application();

        // Start the application
        app.Run();
    }
}
