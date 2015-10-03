using UnityEngine;
using System.Collections;
using SimpleJSON;

/**
 * This class is static class.
 */
[ExecuteInEditMode]
public class MonsterDatatable
{
    private static JSONNode m_Data;
    
    public static JSONNode Data
    {
        get
        {
            if (null == m_Data)
            {
                Initialize();
            }
            return m_Data;
        }
    }
    
    private static void Initialize()
    {
        TextAsset LoadedStringTable = Resources.Load<TextAsset>("Data/MonsterData");
        m_Data = JSON.Parse(LoadedStringTable.text);
    }
}
