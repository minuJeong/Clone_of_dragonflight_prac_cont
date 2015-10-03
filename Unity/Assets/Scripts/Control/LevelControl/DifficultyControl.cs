using UnityEngine;
using System.Collections;

public class DifficultyControl
{
    public int Difficulty = 0;
    private static DifficultyControl m_Instance;

    public static DifficultyControl Instance
    {
        get
        {
            if (null == m_Instance)
            {
                m_Instance = new DifficultyControl();
            }
            return m_Instance;
        }
    }

    public string[] MonsterNames = new string[]
    {
        "Enemy_0",
        "Enemy_1",
        "Enemy_1_1",
        "Enemy_2",
        "Enemy_3"
    };
    public string[] ObstacleNames = new string[]
    {
        "Obstacle_0",
        "Obstacle_1"
    };

    public string GetCurrentEnemyName(int offset)
    {
        return MonsterNames [Mathf.Clamp(Difficulty + offset, 0, MonsterNames.Length - 1)];
    }

    public string GetCurrentObstacleName(bool invincible)
    {
        if (invincible)
        {
            return ObstacleNames [1];
        } else
        {
            return ObstacleNames [0];
        }
    }
}
