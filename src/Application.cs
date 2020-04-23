using System;

using SFML.Window;
using SFML.Graphics;
using SFML.System;

public class Application
{
    private RenderWindow window;
    private TestScreen00 screen0;
    private TestScreen01 screen1;

    public Application()
    {
        this.window = new RenderWindow(new VideoMode(800, 600), "Best Pong", Styles.Default);
        window.Closed += this.Window_Closed;
        window.KeyReleased += this.Window_KeyReleased;

        screen0 = new TestScreen00();
        screen1 = new TestScreen01();

        ScreenManager.Instance.ChangeScreen(screen0);
    }

    private void Window_KeyReleased(object sender, KeyEventArgs e)
    {
        ScreenManager.Instance.ChangeScreen(screen1);
    }

    public void Run()
    {
        Clock clock = new Clock();

        while (window.IsOpen)
        {
            float deltaTime = clock.Restart().AsSeconds();

            window.DispatchEvents();

            window.Clear(Color.Red);

            ScreenManager.Instance.Update(deltaTime);
            ScreenManager.Instance.Render(window);

            window.Display();
        }

        window.Dispose();
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
