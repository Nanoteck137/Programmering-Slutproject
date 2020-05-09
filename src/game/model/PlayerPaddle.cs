using System;

using SFML.System;
using SFML.Window;

/// <summary>
/// This class inherits the Paddle class and it's a Player controlled 
/// paddle so the players use the keyboard to control the paddle
/// </summary>
public class PlayerPaddle : Paddle
{
    /// <summary>
    /// This class stores data about the paddle and how the player 
    /// should control this specific paddle
    /// </summary>
    public class Config
    {
        public Vector2f InitialPosition { get; set; }
        public Vector2f Size { get; set; }

        public Keyboard.Key UpKey;
        public Keyboard.Key DownKey;
    }

    // The config associated with this paddle
    private Config config;

    /// <summary>
    /// Creates the paddle with the specific config and the 
    /// ball used in the game 
    /// </summary>
    /// <param name="config"></param>
    /// <param name="ball"></param>
    public PlayerPaddle(Config config)
    {
        Position = config.InitialPosition;
        Size = config.Size;

        this.config = config;
    }

    public override void Update(float deltaTime)
    {
        Vector2f position = Position;
        if (Keyboard.IsKeyPressed(config.UpKey))
        {
            position.Y -= 250.0f * deltaTime;
        }
        else if (Keyboard.IsKeyPressed(config.DownKey))
        {
            position.Y += 250.0f * deltaTime;
        }

        Position = position;
    }
}
