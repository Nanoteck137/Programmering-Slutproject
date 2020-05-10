using System;

/// <summary>
/// This class manages the game data
/// </summary>
public class GameManager
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
                instance = new GameManager();
            return instance;
        }
    }

    public GameModel Model { get; private set; }

    private GameManager()
    {
        Model = new GameModel();
    }

    public void NewGame()
    {
        Model = new GameModel();
    }

    public void Update(float deltaTime)
    {
        Model.Update(deltaTime);
    }
}
