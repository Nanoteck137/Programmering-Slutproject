﻿using System;

using SFML.Window;
using SFML.Graphics;
using SFML.System;
using System.Diagnostics;

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
    public RenderWindow Window { get; private set; }

    // Store the MainMenuScreen so we don't need to create a new one 
    // always when changing screens
    private MainMenuScreen mainMenuScreen;
    public MainMenuScreen MainMenuScreen { get { return mainMenuScreen; } }

    // Store the CustomizeScreen so we don't need to create a new one 
    // always when changing screens
    private CustomizeScreen customizeScreen;
    public CustomizeScreen CustomizeScreen { get { return customizeScreen; } }

    // Store the GameScreen so we don't need to create a new one 
    // always when changing screens
    private GameScreen gameScreen;
    public GameScreen GameScreen { get { return gameScreen; } }

    // Store the state if the application is running, and chaging this 
    // to false when the application is running should exit the app
    private bool running = false;

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

        // TODO(patrik): Move this to a better place
        Language enUS = new Language("en-US");
        enUS.AddTranslation("window.title", "The Best Pong");

        enUS.AddTranslation("mainmenu.title", "The Best Pong");
        enUS.AddTranslation("mainmenu.playbutton", "Play Game");
        enUS.AddTranslation("mainmenu.customizationbutton", "Customize");
        enUS.AddTranslation("mainmenu.settingsbutton", "Settings");
        enUS.AddTranslation("mainmenu.quitbutton", "Quit Game");

        enUS.AddTranslation("customize.title", "Customize");
        enUS.AddTranslation("customize.backbutton", "Back");

        // TODO(patrik): Move this to a better place
        Language seSE = new Language("se-SE");
        seSE.AddTranslation("window.title", "Bästa Pong");

        seSE.AddTranslation("mainmenu.title", "Bästa Pong");
        seSE.AddTranslation("mainmenu.playbutton", "Spela spelet");
        seSE.AddTranslation("mainmenu.customizationbutton", "Anpassa");
        seSE.AddTranslation("mainmenu.settingsbutton", "Inställningar");
        seSE.AddTranslation("mainmenu.quitbutton", "Avsluta");

        seSE.AddTranslation("customize.title", "Anpassa");
        seSE.AddTranslation("customize.backbutton", "Tillbacka");

        // TODO(patrik): Move this to a better place
        LanguageManager.Instance.AddLanguage(enUS);
        LanguageManager.Instance.AddLanguage(seSE);
        LanguageManager.Instance.SetCurrentLanguage("se-SE");

        Styles windowStyle = Styles.Titlebar | Styles.Close;

        // Create a RenderWindow and setup the event callbacks
        Window = new RenderWindow(new VideoMode(1280, 720),
                                  LanguageManager.Instance.GetTranslation("window.title"), windowStyle);

        // Register some callbacks
        Window.Closed += this.Window_Closed;
        Window.KeyPressed += this.Window_KeyPressed;
        Window.KeyReleased += this.Window_KeyReleased;
        Window.MouseButtonPressed += this.Window_MouseButtonPressed;
        Window.MouseButtonReleased += this.Window_MouseButtonReleased;

        // Create the screens used by this application
        mainMenuScreen = new MainMenuScreen();
        customizeScreen = new CustomizeScreen();
        gameScreen = new GameScreen();

        // Change the screen to the MainMenu
        ScreenManager.Instance.ChangeScreen(mainMenuScreen);

        SkinManager.Instance.AddSkin(new Skin(Color.White));
        SkinManager.Instance.AddSkin(new Skin(Color.Red));
        SkinManager.Instance.AddSkin(new Skin(Color.Blue));

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
            Window.DispatchEvents();

            // Clear the window
            Window.Clear(Color.Black);

            // Update and Render the SceneManager
            ScreenManager.Instance.Update(deltaTime);
            ScreenManager.Instance.Render(Window);

            // Update the GameManager
            GameManager.Instance.Update(deltaTime);

            if (Input.Instance.IsKeyPressed(Keyboard.Key.Num1))
            {
                Console.WriteLine("Changing the language to 'en-US'");
                LanguageManager.Instance.SetCurrentLanguage("en-US");
            }
            else if (Input.Instance.IsKeyPressed(Keyboard.Key.Num2))
            {
                Console.WriteLine("Changing the language to 'se-SE'");
                LanguageManager.Instance.SetCurrentLanguage("se-SE");
            }

            // Display the contents to the window
            Window.Display();

            // Update the input last so the keys gets updated 
            // in the right order
            Input.Instance.Update();
        }

        clock.Dispose();
        Window.Dispose();
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
