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

    // Keeps a referance to the game data
    public GameModel Model { get; private set; }

    // We need to store the skin id here, because the when we start a 
    // new game the model get reconstructed and dosen't keep the skin id so 
    // we need to give it the right id when we create a new model so 
    // the customization screen can work
    private int leftPaddleSkinID = 0;
    private int rightPaddleSkinID = 0;

    public int LeftPaddleSkinID
    {
        get { return leftPaddleSkinID; }
        set { leftPaddleSkinID = value; }
    }

    public int RightPaddleSkinID
    {
        get { return rightPaddleSkinID; }
        set { rightPaddleSkinID = value; }
    }

    private Action onNewGameAction;

    private GameManager()
    {
        // Start a new game
        NewGame();
    }

    /// <summary>
    /// Registers a callback function when a new game starts, its called 
    /// when the new data has been constructed
    /// </summary>
    /// <param name="callback">The callback function to be registered</param>
    public void RegisterOnNewGameCallback(Action callback)
    {
        onNewGameAction += callback;
    }

    /// <summary>
    /// Unregisters a callback function to the new game callback
    /// </summary>
    /// <param name="callback">The callback function to be unregistered</param>
    public void UnregisterOnNewGameCallback(Action callback)
    {
        onNewGameAction -= callback;
    }

    /// <summary>
    /// Starts a new game and resets if there was a game already started
    /// </summary>
    public void NewGame()
    {
        // Create a new model
        Model = new GameModel();

        // Reset the skin id to the right ids, so the customization 
        // screen works and can set the ids without the ids resetting
        Model.LeftPaddle.SkinID = leftPaddleSkinID;
        Model.RightPaddle.SkinID = rightPaddleSkinID;

        // Call the callback functions to notify that a new game has started
        if (onNewGameAction != null)
            onNewGameAction();
    }

    /// <summary>
    /// Update the game data
    /// </summary>
    /// <param name="deltaTime">Deltatime</param>
    public void Update(float deltaTime)
    {
        // Update the model 
        Model.Update(deltaTime);
    }
}
