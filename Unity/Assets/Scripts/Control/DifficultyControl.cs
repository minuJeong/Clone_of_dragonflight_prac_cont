using UnityEngine;
using System.Collections;

public class DifficultyControl
{
    private const int TIME_TO_DIFFICULTY_FACTOR = 30;
    private int Difficulty = 0;
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

    public string GetCurrentEnemyName()
    {
        Difficulty = (int)Time.timeSinceLevelLoad / TIME_TO_DIFFICULTY_FACTOR;

        return MonsterNames [Mathf.Clamp(Difficulty, 0, MonsterNames.Length - 1)];
    }
}
