using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class App : MonoBehaviour
{
    public static App Instance;
    public const string HOME_LEVEL = "Home";
    public StringTable m_StringTable;

    // Use this for initialization
    void Start()
    {
        Instance = this;
    }
}
