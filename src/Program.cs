using System;

using SFML.Window;
using SFML.Graphics;

class Program
{
    static void Main(string[] args)
    {
        RenderWindow window = new RenderWindow(new VideoMode(800, 600), "Best Pong", Styles.Default);

        window.Closed += Window_Closed;

        while (window.IsOpen)
        {
            window.DispatchEvents();

            window.Clear(Color.Red);
            window.Display();
        }

        window.Dispose();
    }

    private static void Window_Closed(object sender, EventArgs e)
    {
        RenderWindow window = (RenderWindow)sender;
        window.Close();
    }
}
