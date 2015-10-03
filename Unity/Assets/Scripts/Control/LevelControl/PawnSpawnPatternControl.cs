using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * Color of level texture to spawn pattern
 */
public class PawnSpawnPatternControl
{
    private const string SPAWN_DATA_PATH = "Data/SpawnPattern/";
    private static Color NONE = new Color(1, 1, 1);
    private static Color CURRENT_LEVEL = new Color(0, 0, 0);
    private static Color CURRENT_LEVEL_PLUS_1 = new Color((float)0x72 / 0xFF, 0, (float)0xA3 / 0xFF);
    private static Color CURRENT_LEVEL_PLUS_2 = new Color((float)0x16 / 0xFF, 0, (float)0x82 / 0xFF);
    private static Color OBSTACLE_BREAKABLE = new Color(1, 0, 0);
    private static Color OBSTACLE_UNBREAKABLE = new Color(1, (float)0xF2 / 0xFF, 0);

    private Dictionary <string, Texture2D> m_TextureCaching;

    public enum SpawnPattern
    {
        NONE,
        CURRENT_LEVEL,
        CURRENT_LEVEL_PLUS_1,
        CURRENT_LEVEL_PLUS_2,
        OBSTACLE_BREAKABLE,
        OBSTACLE_UNBREAKABLE
    }

    private static PawnSpawnPatternControl m_Instance;

    public static PawnSpawnPatternControl Instance
    {
        get
        {
            if (null == m_Instance)
            {
                m_Instance = new PawnSpawnPatternControl();
                m_Instance.m_TextureCaching = new Dictionary<string, Texture2D>();
            }
            return m_Instance;
        }
    }

    public SpawnDataModel[] ParseTexture(string LevelName, int Row)
    {
        Texture2D DataTexture;
        if (m_TextureCaching.ContainsKey(LevelName))
        {
            DataTexture = m_TextureCaching [LevelName];
        } else
        {
            DataTexture = Resources.Load<Texture2D>(SPAWN_DATA_PATH + LevelName);
        }

        Color[] Pixels = DataTexture.GetPixels();
        int len_x = DataTexture.width;
        SpawnDataModel[] SpawnDataSet = new SpawnDataModel[len_x];

        // Skip if row is bigger than texture height
        if (DataTexture.height - 1 < Row)
        {
            return SpawnDataSet;
        }

        for (int x = 0; x < len_x; x++)
        {
            SpawnDataSet [x] = new SpawnDataModel();
            SpawnDataSet [x].Position = x;
            Color c = Pixels [x + Row * len_x];
            if (c.Equals(NONE))
            {
                SpawnDataSet [x].SpawnPattern = SpawnPattern.NONE;
            } else if (c.Equals(CURRENT_LEVEL))
            {
                SpawnDataSet [x].SpawnPattern = SpawnPattern.CURRENT_LEVEL;
            } else if (c.Equals(CURRENT_LEVEL_PLUS_1))
            {
                SpawnDataSet [x].SpawnPattern = SpawnPattern.CURRENT_LEVEL_PLUS_1;
            } else if (c.Equals(CURRENT_LEVEL_PLUS_2))
            {
                SpawnDataSet [x].SpawnPattern = SpawnPattern.CURRENT_LEVEL_PLUS_2;
            } else if (c.Equals(OBSTACLE_BREAKABLE))
            {
                SpawnDataSet [x].SpawnPattern = SpawnPattern.OBSTACLE_BREAKABLE;
            } else if (c.Equals(OBSTACLE_UNBREAKABLE))
            {
                SpawnDataSet [x].SpawnPattern = SpawnPattern.OBSTACLE_UNBREAKABLE;
            } else
            {
                Debug.Log(CURRENT_LEVEL_PLUS_1);
                Debug.Log(CURRENT_LEVEL_PLUS_2);
                Debug.Log(OBSTACLE_BREAKABLE);
                Debug.Log(OBSTACLE_UNBREAKABLE);
                Debug.LogError("[-] No match color found." + c);
                Debug.Log(x + ", " + Row);
                Debug.Break();
            }
        }

        return SpawnDataSet;
    }
}

public class SpawnDataModel
{
    public int Position;
    public PawnSpawnPatternControl.SpawnPattern SpawnPattern;
}