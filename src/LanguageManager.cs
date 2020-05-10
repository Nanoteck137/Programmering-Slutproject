using System;
using System.Collections.Generic;
using System.Diagnostics;

public class Language
{
    private string name;

    public string Name { get { return name; } }

    private Dictionary<string, string> translations;

    public Language(string name)
    {
        this.name = name;

        translations = new Dictionary<string, string>();
    }

    public void AddTranslation(string key, string translation)
    {
        translations[key] = translation;
    }

    public string GetTranslation(string key)
    {
        Debug.Assert(translations.ContainsKey(key),
                string.Format("'{0}' has no translation for '{1}'",
                              this.name, key));

        return translations[key];
    }

}

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

    private Language currentLanguage;
    private Dictionary<string, Language> languages;

    private Action onLanguageChangedAction;

    private LanguageManager()
    {
        languages = new Dictionary<string, Language>();
    }

    public void RegisterOnLanguageChangedCallback(Action callback)
    {
        onLanguageChangedAction += callback;
    }

    public void UnregisterOnLanguageChangedCallback(Action callback)
    {
        onLanguageChangedAction -= callback;
    }

    public void AddLanguage(Language language)
    {
        Debug.Assert(!languages.ContainsKey(language.Name),
                     "Language already registered in the LanguageManager");
        languages.Add(language.Name, language);
    }

    public void SetCurrentLanguage(string languageName)
    {
        Debug.Assert(languages.ContainsKey(languageName),
                     string.Format("No language with name '{0}'",
                                   languageName));

        currentLanguage = languages[languageName];

        if (onLanguageChangedAction != null)
            onLanguageChangedAction();
    }

    public string GetTranslation(string key)
    {
        Debug.Assert(currentLanguage != null, "No language selected");
        return currentLanguage.GetTranslation(key);
    }
}