using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour
{
    public static Game Instance;
    public static EnemySpawnControl Instance_EnemySpawnControl;
    public static PlayerMoveInputControl Instance_InputControl;

    // Use this for initialization
    void Awake()
    {
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
