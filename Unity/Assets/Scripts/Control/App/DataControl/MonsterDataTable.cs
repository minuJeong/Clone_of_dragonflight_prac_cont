using UnityEngine;
using System.Collections;
using LitJson;


/**
 * This class is static class.
 */
[ExecuteInEditMode]
public class MonsterDatatable
{
    private static JsonData m_Data;
    
    public static JsonData Data
    {
        get
        {
            if (null == m_Data)
            {
                Initialize ();
            }
            return m_Data;
        }
    }
    
    private static void Initialize()
    {
        TextAsset LoadedStringTable = Resources.Load<TextAsset>("Data/MonsterData");
        m_Data = JsonMapper.ToObject(LoadedStringTable.text);
    }
}
