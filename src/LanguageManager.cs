using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

/// <summary>
/// This class holds infomation about a language used in the program
/// </summary>
public class Language
{
    // The name of the language
    private string name;

    public string Name { get { return name; } }

    // Holds all the translation by a key for example 'game.title' is a 
    // key we use to look up a translation
    private Dictionary<string, string> translations;

    /// <summary>
    /// Creates a new language with a name
    /// </summary>
    /// <param name="name">The name of the language</param>
    public Language(string name)
    {
        this.name = name;

        // Initialize the translations dictionary
        translations = new Dictionary<string, string>();
    }

    /// <summary>
    /// Adds a new translation for this language
    /// </summary>
    /// <param name="key">A key so you can look up 
    /// this translation later</param>
    /// <param name="translation">The translation of this key</param>
    public void AddTranslation(string key, string translation)
    {
        translations[key] = translation;
    }

    /// <summary>
    /// Gets a translation from a key
    /// </summary>
    /// <param name="key">The key we want the translation of</param>
    /// <returns>Returns the translation</returns>
    public string GetTranslation(string key)
    {
        if (!translations.ContainsKey(key))
            return null;

        return translations[key];
    }

}

/// <summary>
/// This class manages Languages and it is used to look up translation 
/// and handle a selected language
/// </summary>
public class LanguageManager
{
    private static LanguageManager instance;
    public static LanguageManager Instance
    {
        get
        {
            if (instance == null)
                instance = new LanguageManager();
            return instance;
        }
    }

    // The language currently selected
    private Language currentLanguage;

    // A common language to get common translations
    private Language commonLanguage;

    // A dictionary holding languages by name
    private Dictionary<string, Language> languages;

    public Language CommonLanguage
    {
        get { return commonLanguage; }
        set { commonLanguage = value; }
    }

    public Dictionary<string, Language> Languages
    {
        get { return languages; }
    }

    // A action to notify users of language changes
    private Action onLanguageChangedAction;

    private LanguageManager()
    {
        // Initialize the dicationary
        languages = new Dictionary<string, Language>();

        string path = Path.Join(Directory.GetCurrentDirectory(), "res", "lang");
        string[] files = Directory.GetFiles(path);

        LanguageParser parser = new LanguageParser();

        foreach (string file in files)
        {
            string languageName = Path.GetFileNameWithoutExtension(file);
            Dictionary<string, string> translations = parser.ParseFile(file);

            Language language = new Language(languageName);
            foreach (var translation in translations)
            {
                language.AddTranslation(translation.Key, translation.Value);
            }

            if (languageName == "common")
                CommonLanguage = language;
            else
                AddLanguage(language);
        }
    }

    /// <summary>
    /// Register a language changed callback
    /// </summary>
    /// <param name="callback">The callback function to be registered</param>
    public void RegisterOnLanguageChangedCallback(Action callback)
    {
        onLanguageChangedAction += callback;
    }

    /// <summary>
    /// Unregister a language changed callback
    /// </summary>
    /// <param name="callback">The callback function to be unregistered</param>
    public void UnregisterOnLanguageChangedCallback(Action callback)
    {
        onLanguageChangedAction -= callback;
    }

    /// <summary>
    /// This method adds a language to the dictionary of languages
    /// </summary>
    /// <param name="language">The language to be added</param>
    public void AddLanguage(Language language)
    {
        Debug.Assert(!languages.ContainsKey(language.Name),
                     "Language already registered in the LanguageManager");
        languages.Add(language.Name, language);
    }

    /// <summary>
    /// Sets a language to use in the program
    /// </summary>
    /// <param name="languageName">The name of the language</param>
    public void SetCurrentLanguage(string languageName)
    {
        // Check if there is already a language with the that name
        Debug.Assert(languages.ContainsKey(languageName),
                     string.Format("No language with name '{0}'",
                                   languageName));

        // Set the current language to the selected language
        currentLanguage = languages[languageName];

        // Notify that the language has changed
        if (onLanguageChangedAction != null)
            onLanguageChangedAction();
    }

    /// <summary>
    /// Gets a translation by the key
    /// </summary>
    /// <param name="key">The key to look up</param>
    /// <returns>Returns the translation for the key</returns>
    public string GetTranslation(string key)
    {
        // Assert if we don't have selected a language
        Debug.Assert(currentLanguage != null, "No language selected");

        // Try to get the translation from the current selected language 
        string result = currentLanguage.GetTranslation(key);

        // If we don't get a translation from the current selected 
        // language then try to get a translation from the common language
        if (result == null && commonLanguage != null)
            result = commonLanguage.GetTranslation(key);

        // If we still don't get a translation then return the key as 
        // the translation, and send a warning that the key has no 
        // translation associated with it
        if (result == null)
        {
            Console.WriteLine("Warning: '{0}' has no translation for '{1}'",
                              currentLanguage.Name, key);
            return key;
        }

        return result;
    }
}