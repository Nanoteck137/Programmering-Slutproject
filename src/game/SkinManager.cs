using System;
using System.Collections.Generic;

using SFML.Graphics;

/// <summary>
/// A skin is just data that later is used when rendering
/// </summary>
public class Skin
{
    public int ID { get; set; }
    public Color Color { get; }

    public Skin(Color color)
    {
        Color = color;
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
    }

    /// <summary>
    /// Adds a new skin to the collection
    /// </summary>
    /// <param name="skin">The skin to be added</param>
    /// <returns>Returns the new id of the skin in the collection</returns>
    public int AddSkin(Skin skin)
    {
        int id = numSkins++;
        skin.ID = id;
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