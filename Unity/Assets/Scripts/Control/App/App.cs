using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class App : MonoBehaviour
{
    public static App Instance;
    public const string HOME_LEVEL = "Home";
    public const string GAME_LEVEL = "GameHome";
    public const string RESULT_LEVEL = "Result";
    public StringTable m_StringTable;

    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad (this);
        Instance = this;
    }
}
