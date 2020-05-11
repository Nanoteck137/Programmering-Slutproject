using System;

using SFML.System;
using SFML.Graphics;

/// <summary>
/// This class handles how the game is renderered to the user
/// </summary>
public class GameView
{
    // The game model we should render
    private GameModel model;

    // The font used in the text rendering
    private Font font;

    // The shape of the left paddle
    private RectangleShape leftPaddle;

    // The shape of the right paddle
    private RectangleShape rightPaddle;

    // The shape of the ball
    private CircleShape ball;

    // The text that renders the left side score
    private Text leftSideScore;

    // The text that renders the right side score
    private Text rightSideScore;

    // A message to the user, that say how thay can get back the the main menu
    private UIText helperMessage;

    // The time the helper message should be visable in seconds
    private float helperMessageMaxTime = 2.0f;

    // A timer for the helper message to disappear after 
    // `helperMessageMaxTime` seconds
    private float helperMessageTimer = 0.0f;

    // A flag to see if the message timer has expired
    private bool helperMessageTimerExpired = false;

    public GameModel Model { get { return model; } }

    public GameView(GameModel model)
    {
        // Set the model
        this.model = model;

        // Create the left paddle and set it's origin to the center
        leftPaddle = new RectangleShape(model.LeftPaddle.Size);
        leftPaddle.Origin = new Vector2f(model.LeftPaddle.Size.X / 2.0f,
                                         model.LeftPaddle.Size.Y / 2.0f);

        // Create the right paddle and set it's origin to the center
        rightPaddle = new RectangleShape(model.RightPaddle.Size);
        rightPaddle.Origin = new Vector2f(model.RightPaddle.Size.X / 2.0f,
                                          model.RightPaddle.Size.Y / 2.0f);

        // Create the ball and set it's origin to the center
        ball = new CircleShape(model.Ball.Radius);
        ball.Origin = new Vector2f(model.Ball.Radius, model.Ball.Radius);

        // Load a font
        font = new Font("res/fonts/font.ttf");

        // Get the window size
        Vector2u windowSize = Application.Instance.Window.Size;

        // Calculate a offset for the scores 
        float offset = windowSize.X / 4.0f;

        // Create the left side score and calculate the position
        leftSideScore = new Text("0", font);
        leftSideScore.CharacterSize = 30;
        leftSideScore.Position = new Vector2f(windowSize.X / 2.0f -
                                              offset, 90.0f);

        // Create the right side score and calculate the position
        rightSideScore = new Text("0", font);
        rightSideScore.CharacterSize = 30;
        rightSideScore.Position = new Vector2f(windowSize.X / 2.0f +
                                               offset, 90.0f);

        // Update the scores so their origins are in the center
        UpdateScore();

        // Create the helper message text
        helperMessage = new UIText(new Vector2f(windowSize.X / 2.0f,
                                                windowSize.Y - 70.0f),
                                                "game.exitmessage",
                                                20, font);
    }

    /// <summary>
    /// Updates the score texts to the new values and recalculate the origins
    /// </summary>
    private void UpdateScore()
    {
        // Set the left side score text to the new score
        leftSideScore.DisplayedString = model.LeftSideScore.ToString();
        // Get the new bounds of the text
        FloatRect rect = leftSideScore.GetLocalBounds();
        // Recalculate the origin
        leftSideScore.Origin = new Vector2f(rect.Width / 2.0f, rect.Height / 2.0f);

        // Set the left side score text to the new score
        rightSideScore.DisplayedString = model.RightSideScore.ToString();
        // Get the new bounds of the text
        rect = rightSideScore.GetLocalBounds();
        // Recalculate the origin
        rightSideScore.Origin = new Vector2f(rect.Width / 2.0f, rect.Height / 2.0f);
    }

    /// <summary>
    /// Update the game view
    /// </summary>
    /// <param name="deltaTime">Deltatime</param>    
    public void Update(float deltaTime)
    {
        // Check if the timer is over the max time and if the timer already 
        // has been expired
        if (helperMessageTimer > helperMessageMaxTime && !helperMessageTimerExpired)
        {
            // If the timer is done then change the Alpha of the helper 
            // message to 0, to hide it for the user
            Color color = helperMessage.Color;
            color.A = 0;
            helperMessage.Color = color;

            // Set the flag becuase the timer should only trigger once
            helperMessageTimerExpired = true;
        }
        else
        {
            // Increment the timer by the deltatime
            helperMessageTimer += deltaTime;
        }
    }

    /// <summary>
    /// Setup and render the left paddle
    /// </summary>
    /// <param name="renderTarget">The RenderTarget the left paddle 
    /// should render to</param>
    private void RenderLeftPaddle(RenderTarget renderTarget)
    {
        // Set the position and skin of the left paddle then render it
        leftPaddle.Position = Model.LeftPaddle.Position;

        // Get the skin for the left paddle
        Skin leftSkin = SkinManager.Instance.GetSkinFromID(Model.LeftPaddle.SkinID);
        // Apply the skin for the left paddle
        leftPaddle.FillColor = leftSkin.Color;

        // Render the left paddle
        renderTarget.Draw(leftPaddle);
    }

    /// <summary>
    /// Setup and render the right paddle
    /// </summary>
    /// <param name="renderTarget">The RenderTarget the right paddle 
    /// should render to</param>
    private void RenderRightPaddle(RenderTarget renderTarget)
    {
        // Set the position and skin of the right paddle then render it
        rightPaddle.Position = Model.RightPaddle.Position;

        // Get the skin for the right paddle
        Skin rightSkin = SkinManager.Instance.GetSkinFromID(Model.RightPaddle.SkinID);

        // Apply the skin for the right paddle
        rightPaddle.FillColor = rightSkin.Color;

        // Render the right paddle
        renderTarget.Draw(rightPaddle);
    }

    /// <summary>
    /// Render the game
    /// </summary>
    /// <param name="renderTarget">The RenderTarget the game should 
    /// render to</param>
    public void Render(RenderTarget renderTarget)
    {
        // Render the left paddle
        RenderLeftPaddle(renderTarget);

        // Render the right paddle
        RenderRightPaddle(renderTarget);

        // Set the position of the ball and render it
        ball.Position = Model.Ball.Position;
        renderTarget.Draw(ball);

        // Render the left score
        renderTarget.Draw(leftSideScore);

        // Render the right score
        renderTarget.Draw(rightSideScore);

        // Update the scores
        // TODO(patrik): Maybe we should not do this every frame?!?!??
        UpdateScore();

        helperMessage.Render(renderTarget);
    }
}
