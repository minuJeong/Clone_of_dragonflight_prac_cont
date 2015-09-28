﻿using UnityEngine;
using System.Collections;
using LitJson;

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
            m_SpriteRenderer.sprite = m_SpriteCollection.GetSpriteByName(EnemyName);
        }
    }

    protected override void UpdateTargetGroup()
    {
        return;
    }

    protected override void OnCollision(GameObject target)
    {
        PlayerPawn player = target.GetComponent<PlayerPawn>();
        if (null != player)
        {
            player.Data.HP -= Data.Damage;
        }
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

    private static JsonData Data;

    public MissileDataModel GetDataModel(string enemyName)
    {
        JsonData dat = MonsterDatatable.Data [enemyName] [DATAKEY_MISSILE];

        float w = Game.Instance.GameCamera.orthographicSize * Game.Instance.GameCamera.aspect * 2;
        float h = Game.Instance.GameCamera.orthographicSize * 2;

        MissileDataModel model = new MissileDataModel();
        model.Velocity = Vector3.down * float.Parse(dat [DATAKEY_SPEED].ToString());
        model.Acceleration = Vector3.zero;
        model.Friction = 1.0F;
        model.ValidArea = new Rect(-w / 2, -h / 2, w + 0.01F, h);
        model.CollisionDistanceSqrt = 0.1F;
        model.Damage = float.Parse(dat [DATAKEY_DAMAGE].ToString());

        return model;
    }

    public string GetSpriteName(string enemyName)
    {
        return MonsterDatatable.Data [enemyName] [DATAKEY_MISSILE] [DATAKEY_SPRITENAME].ToString();
    }
}
