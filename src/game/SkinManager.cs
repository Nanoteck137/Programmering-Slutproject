using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Newtonsoft.Json;
using SFML.Graphics;

/// <summary>
/// A pack of skins laoded from json
/// </summary>
public struct SkinPackJson
{
    public string Name { get; set; }
    public List<SkinJson> Skins { get; set; }
}

/// <summary>
/// A skin is just data that later is used to create a `Skin` becuase 
/// there is not convertion between '#ffffff' color to SFML color object 
/// so we use this class to serialize/deserialize json and the other class 
/// for rendering
/// </summary>
public struct SkinJson
{
    public string Name { get; set; }
    public string Color { get; set; }
}

/// <summary>
/// A skin is just data that later is used when rendering
/// </summary>
public class Skin
{
    private string name;
    private Color color;

    public string Name { get { return name; } }
    public Color Color { get { return color; } }

    public Skin(string name, Color color)
    {
        this.name = name;
        this.color = color;
    }
}

/// <summary>
/// SkinManager manages skins and handles adding new skins and getting skins
/// </summary>
public class SkinManager
{
    private static SkinManager instance;
    public static SkinManager Instance
    {
        get
        {
            if (instance == null)
                instance = new SkinManager();
            return instance;
        }
    }

    // Store the skins with their ids
    private Dictionary<int, Skin> skins;

    // Keep track how many skins there are, used when generating new ids
    private int numSkins = 0;

    public int NumSkins { get { return skins.Count; } }

    /// <summary>
    /// Initialize the skin manager
    /// </summary>
    public SkinManager()
    {
        skins = new Dictionary<int, Skin>();

        // Load the skis
        LoadSkins();
    }

    /// <summary>
    /// Load skins from json files located 'res/skins'
    /// </summary>
    private void LoadSkins()
    {
        // Construct a path to the res/skins folder, becuase we need to 
        // load the json files there to construct the skins
        string path = Path.Join(Directory.GetCurrentDirectory(), "res", "skins");

        // Get all the files in the directory
        string[] packs = Directory.GetFiles(path);

        // Go through every file and see if it's a json, if it is a json 
        // file then try to load it with Newonsoft json library to 
        // deserialize it to a C# class and then create the skin object
        foreach (string packPath in packs)
        {
            // Check if the file has a .json extention
            if (Path.GetExtension(packPath).ToLower() == "json")
            {
                Console.WriteLine(
                    "Ignoring '{0}' becuase it has no .json extention",
                    packPath);
                continue;
            }

            Console.WriteLine("Trying to load pack '{0}'", packPath);

            // Read the json file
            string fileContent = File.ReadAllText(packPath);

            // Deserialize the json to a C# class
            SkinPackJson pack = JsonConvert.DeserializeObject<SkinPackJson>(fileContent);

            // Go through every skin in the pack and create a 
            // Skin object to later be added
            foreach (SkinJson skinData in pack.Skins)
            {
                // Convert the html style color to a uint color 
                // '#RRGGBBAA' -> 0xRRGGBBAS
                uint color = UInt32.Parse(skinData.Color.Replace("#", ""), NumberStyles.HexNumber);

                // Add the skin to the manager
                AddSkin(new Skin(skinData.Name, new Color(color)));
            }
        }
    }

    /// <summary>
    /// Adds a new skin to the collection
    /// </summary>
    /// <param name="skin">The skin to be added</param>
    /// <returns>Returns the new id of the skin in the collection</returns>
    public int AddSkin(Skin skin)
    {
        int id = numSkins++;
        skins[id] = skin;

        return id;
    }

    /// <summary>
    /// Get a skin from id
    /// </summary>
    /// <param name="id">The id of the skin to get</param>
    /// <returns>Returns the skin</returns>
    public Skin GetSkinFromID(int id)
    {
        if (!skins.ContainsKey(id))
            return null;

        return skins[id];
    }
}
