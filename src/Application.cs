using System;

using SFML.Window;
using SFML.Graphics;
using SFML.System;
using System.Diagnostics;

public class Application
{
    private static Application instance;
    public static Application Instance { get { return instance; } }

    public RenderWindow Window { get; private set; }

    private MainMenuScreen mainMenuScreen;
    private GameScreen gameScreen;

    private bool running = false;

    public Application()
    {
        Debug.Assert(instance == null);
        instance = this;

        Window = new RenderWindow(new VideoMode(1280, 720), "Best Pong", Styles.Default);
        Window.Closed += this.Window_Closed;
        Window.MouseButtonPressed += this.Window_MouseButtonPressed;
        Window.MouseButtonReleased += this.Window_MouseButtonReleased;

        gameScreen = new GameScreen();
        mainMenuScreen = new MainMenuScreen();

        ScreenManager.Instance.ChangeScreen(mainMenuScreen);
    }

    public void ExitGame()
    {
        running = false;
    }

    public void Run()
    {
        running = true;
        Clock clock = new Clock();

        while (running)
        {
            float deltaTime = clock.Restart().AsSeconds();

            Window.DispatchEvents();

            Window.Clear(Color.Black);

            ScreenManager.Instance.Update(deltaTime);
            ScreenManager.Instance.Render(Window);

            GameManager.Instance.Update(deltaTime);

            Window.Display();

            Input.Instance.Update();
        }

        Window.Dispose();
    }

    private void Window_Closed(object sender, EventArgs e)
    {
        running = false;
    }

    private void Window_MouseButtonPressed(object sender, MouseButtonEventArgs e)
    {
        Input.Instance.SetButtonState((int)e.Button, true);
    }

    private void Window_MouseButtonReleased(object sender, MouseButtonEventArgs e)
    {
        Input.Instance.SetButtonState((int)e.Button, false);
    }

    static void Main(string[] args)
    {
        Application app = new Application();
        app.Run();
    }
}
