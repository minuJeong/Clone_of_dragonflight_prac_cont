using UnityEngine;
using System.Collections;
using SimpleJSON;

/**
 * EnemyMissile should be initialized with Enemy Name
 */
public class EnemyMissile : Missile
{
    private const string VIEW_COMPONENT_NAME = "View";
    public string EnemyName = "";
    private SpriteRenderer m_SpriteRenderer;
    private SpriteCollection m_SpriteCollection;

    private void Awake()
    {
        m_SpriteRenderer = transform.FindChild(VIEW_COMPONENT_NAME).GetComponent<SpriteRenderer>();
        m_SpriteCollection = transform.FindChild(VIEW_COMPONENT_NAME).GetComponent<SpriteCollection>();

        TargetGroup = new GameObject[1];
        TargetGroup [0] = GameObject.FindObjectOfType<PlayerPawn>().gameObject;
    }

    // Use this for initialization
    private void OnEnable()
    {
        if ("" != EnemyName)
        {
            Data = EnemyMissileDataLoader.Instance.GetDataModel(EnemyName);
            m_SpriteRenderer.sprite = m_SpriteCollection.GetSpriteByName(EnemyMissileDataLoader.Instance.GetSpriteName(EnemyName));
        }
    }

    protected override void UpdatePosition()
    {
        transform.position += Data.Velocity * Time.deltaTime;

        if (! Data.ValidArea.Contains(transform.position))
        {
            Die();
        }
    }

    protected override void UpdateTargetGroup()
    {
        return;
    }

    protected override void OnCollision(GameObject target)
    {
        PlayerPawn player = target.GetComponent<PlayerPawn>();
        if (null == player)
        {
            return;
        }

        player.Data.HP -= Data.Damage;
        Die();
    }

    protected override void Die()
    {
        gameObject.SetActive(false);
        EnemyMissileSpawnControl.Instance.MissilePool.push(gameObject);
    }
}

public class EnemyMissileDataLoader
{
    private const string DATAKEY_MISSILE = "Missile";
    private const string DATAKEY_SPEED = "Speed";
    private const string DATAKEY_TYPE = "Type";
    private const string DATAKEY_DELAY = "Delay";
    private const string DATAKEY_SPRITENAME = "SpriteName";
    private const string DATAKEY_DAMAGE = "Damage";
    private static EnemyMissileDataLoader m_Instance;

    public static EnemyMissileDataLoader Instance
    {
        get
        {
            if (null == m_Instance)
            {
                m_Instance = new EnemyMissileDataLoader();
            }
            return m_Instance;
        }
    }

    private static JSONNode Data;

    public MissileDataModel GetDataModel(string enemyName)
    {
        MissileDataModel model = new MissileDataModel();
        if (! MonsterDatatable.Data.hasKey(enemyName))
        {
            Debug.Log("Enemy key not foud: " + enemyName);
        } else
        {
            JSONNode dat = MonsterDatatable.Data [enemyName] [DATAKEY_MISSILE];

            float w = Game.Instance.GameCamera.orthographicSize * Game.Instance.GameCamera.aspect * 2;
            float h = Game.Instance.GameCamera.orthographicSize * 2;

        
            float Speed = dat [DATAKEY_SPEED].AsFloat;
            model.Velocity = Vector3.down * Speed;
            model.Acceleration = Vector3.zero;
            model.Friction = 1.0F;
            model.ValidArea = new Rect(-w / 2, -h / 2, w + 0.01F, h);
            model.CollisionDistanceSqrt = 0.1F;
            model.Damage = dat [DATAKEY_DAMAGE].AsFloat;
        }
        return model;
    }

    public string GetSpriteName(string enemyName)
    {
        return MonsterDatatable.Data [enemyName] [DATAKEY_MISSILE] [DATAKEY_SPRITENAME];
    }
}
