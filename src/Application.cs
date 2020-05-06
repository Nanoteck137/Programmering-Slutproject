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

    private GameScreen gameScreen;

    public Application()
    {
        Debug.Assert(instance == null);
        instance = this;

        Window = new RenderWindow(new VideoMode(1280, 720), "Best Pong", Styles.Default);
        Window.Closed += this.Window_Closed;
        Window.KeyReleased += this.Window_KeyReleased;

        gameScreen = new GameScreen();

        ScreenManager.Instance.ChangeScreen(gameScreen);
    }

    private void Window_KeyReleased(object sender, KeyEventArgs e)
    {
    }

    public void Run()
    {
        Clock clock = new Clock();

        while (Window.IsOpen)
        {
            float deltaTime = clock.Restart().AsSeconds();

            Window.DispatchEvents();

            Window.Clear(Color.Black);

            ScreenManager.Instance.Update(deltaTime);
            ScreenManager.Instance.Render(Window);

            GameManager.Instance.Update(deltaTime);

            Window.Display();
        }

        Window.Dispose();
    }

    private void Window_Closed(object sender, EventArgs e)
    {
        RenderWindow window = (RenderWindow)sender;
        window.Close();
    }

    static void Main(string[] args)
    {
        Application app = new Application();
        app.Run();
    }
}
