using System;

using SFML.System;
using SFML.Window;

public class PlayerPaddle : Paddle
{
    public class Config
    {
        public Vector2f InitialPosition { get; set; }
        public Vector2f Size { get; set; }

        public Keyboard.Key UpKey;
        public Keyboard.Key DownKey;
    }

    private Config config;

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
