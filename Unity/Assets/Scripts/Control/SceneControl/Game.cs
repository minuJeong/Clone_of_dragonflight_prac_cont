using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour
{
    public static Game Instance;
    public static EnemySpawnControl Instance_EnemySpawnControl;
    public static PlayerMoveInputControl Instance_InputControl;

    public Camera GameCamera;
    public Canvas HUDCanvas;

    // Use this for initialization
    void Awake()
    {
        if (null == App.Instance)
        {
            Application.LoadLevel (App.HOME_LEVEL);
            return;
        }

        Instance = this;
        Instance_EnemySpawnControl = GetComponent<EnemySpawnControl>();
        Instance_InputControl = GetComponent<PlayerMoveInputControl>();
    }

    void Start()
    {
        Instance_EnemySpawnControl.Initialize();
        Instance_EnemySpawnControl.StartSpawn();
    }
}
