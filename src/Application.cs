using System;

using SFML.Window;
using SFML.Graphics;

public class Application
{
    private RenderWindow window;

    public Application()
    {
        this.window = new RenderWindow(new VideoMode(800, 600), "Best Pong", Styles.Default);
        window.Closed += Window_Closed;
    }

    public void Run()
    {
        while (window.IsOpen)
        {
            window.DispatchEvents();

            window.Clear(Color.Red);
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
