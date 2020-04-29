using System;

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

    public void NewGame() { }

    public void Update(float deltaTime)
    {
        Model.Update(deltaTime);
    }
}
