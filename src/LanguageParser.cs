using System;
using System.IO;
using System.Collections.Generic;

public class LanguageParser
{
    public LanguageParser() { }

    public Dictionary<string, string> ParseFile(string filePath)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException(string.Format("Could not load file '{0}'"));

        string[] lines = File.ReadAllLines(filePath);
        return ParseLines(lines);
    }

    public Dictionary<string, string> ParseLines(string[] lines)
    {
        Dictionary<string, string> result = new Dictionary<string, string>();

        foreach (string line in lines)
        {
            if (line.Length <= 0)
                continue;
            KeyValuePair<string, string> parsedData = ParseLine(line);
            result.Add(parsedData.Key, parsedData.Value);
        }

        return result;
    }

    private KeyValuePair<string, string> ParseLine(string line)
    {
        // Format: TranslationKey = Some Translation
        Console.WriteLine("Parsing line '{0}'", line);
        string[] parts = line.Split("=");

        string translationKey = parts[0].Trim();
        string translation = parts[1].Trim();

        Console.WriteLine("Parsing Result - TranslationKey = '{0}' Translation = '{1}'", translationKey, translation);

        return new KeyValuePair<string, string>(translationKey, translation);
    }
}