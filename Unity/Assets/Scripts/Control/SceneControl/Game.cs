using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Game : MonoBehaviour
{
    public static Game Instance;
    public static EnemySpawnControl Instance_EnemySpawnControl;
    public static PlayerMoveInputControl Instance_InputControl;
    public Camera GameCamera;
    public Canvas HUDCanvas;

    public Text ScoreField;
    public Text CoinGainField;

    private int m_Score;
    public int Score
    {
        get
        {
            return m_Score;
        }

        set
        {
            m_Score = value;
            if (null != ScoreField)
            {
                ScoreField.text = "Score\n" + m_Score.ToString ();
            }
        }
    }

    private int m_Coin;
    public int CoinGain
    {
        get
        {
            return m_Coin;
        }
        set
        {
            m_Coin = value;
            if (null != CoinGainField)
            {
                CoinGainField.text = "Coin\n" + m_Coin.ToString ();
            }
        }
    }

    // Use this for initialization
    void Awake()
    {
        if (null == App.Instance)
        {
            Application.LoadLevel(App.HOME_LEVEL);
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

    void OnEnable()
    {
        Score = 0;
        CoinGain = 0;
    }
}
