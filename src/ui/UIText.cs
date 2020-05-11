using System;

using SFML.System;
using SFML.Graphics;

/// <summary>
/// This class renders text to the screen, it handles language changes and 
/// resets the origin to the right place
/// </summary>
public class UIText
{
    // The SFML Text object used for rendering
    private Text text;

    // The translation key for this text, used when the language 
    // change we still can look up the new translation with this key
    private string translationKey;

    public string Text
    {
        get { return text.DisplayedString; }
        set { text.DisplayedString = value; UpdateOrigin(); }
    }

    public Color Color
    {
        get { return text.FillColor; }
        set { text.FillColor = value; }
    }

    public UIText(Vector2f position, string translationKey, uint size, Font font)
    {
        // Create the SFML Text object
        text = new Text(
            LanguageManager.Instance.GetTranslation(translationKey), font);

        // Set the character size
        text.CharacterSize = size;

        // Set the position
        text.Position = position;

        // Update the origin so it's in the center
        UpdateOrigin();

        // Set the translation key
        this.translationKey = translationKey;

        // Register the language change callback
        LanguageManager.Instance.RegisterOnLanguageChangedCallback(
            OnLanguageChanged);
    }

    /// <summary>
    /// When a language changes we need to update the origin on the text 
    /// so it still in the center
    /// </summary>
    private void OnLanguageChanged()
    {
        // Set the new translation to the property and then the 
        // property call `UpdateOrigin` to update the origin
        Text = LanguageManager.Instance.GetTranslation(translationKey);
    }

    /// <summary>
    /// Used to update the origin of the text to the center
    /// </summary>
    private void UpdateOrigin()
    {
        // Get the rect of the current text
        FloatRect rect = text.GetLocalBounds();

        // Update the origin to the center
        text.Origin = new Vector2f(rect.Width / 2, rect.Height / 2);
    }

    /// <summary>
    /// Renders a text to the screen
    /// </summary>
    /// <param name="renderTarget">The RenderTarget the text should 
    /// render to</param>
    public void Render(RenderTarget renderTarget)
    {
        // Render the text
        renderTarget.Draw(text);
    }
}