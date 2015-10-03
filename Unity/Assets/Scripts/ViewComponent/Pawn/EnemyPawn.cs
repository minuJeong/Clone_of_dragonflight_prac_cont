using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public sealed class EnemyPawn : Pawn
{
    public RectTransform HPBarHUD;
    public string EnemyName;
    private Rect VALID_RECT;
    private SpriteRenderer View;
    private SpriteCollection SpriteCollection;
    private WaitForSeconds ShootDelay;

    private void Awake()
    {
        View = transform.FindChild("View").GetComponent<SpriteRenderer>();
        SpriteCollection = transform.FindChild("View").GetComponent<SpriteCollection>();
    }

    private void Start()
    {
        float w = Game.Instance.GameCamera.orthographicSize * Game.Instance.GameCamera.aspect * 2;
        float h = Game.Instance.GameCamera.orthographicSize * 2;

        VALID_RECT = new Rect(-w / 2, -h / 2, w, h);
    }

    private void Update()
    {
        if (! VALID_RECT.Contains(transform.position))
        {
            Die();
        } else
        {
            if (null == Data.Path)
            {
                transform.position += Data.Velocity;
            } else
            {
                transform.position = Data.Path.GetPositionGradually(Time.deltaTime);
            }

            HPBarHUD.position = RectTransformUtility.WorldToScreenPoint(Game.Instance.GameCamera, transform.position) - Vector2.up * 20.0F;
        }
    }

    public override void Die()
    {
        if (null != EnemySpawnControl.EnemyPool)
        {
            HPBarHUD.position = new Vector3(0, -1000, 0);
            gameObject.SetActive(false);
            EnemySpawnControl.EnemyPool.push(gameObject);
        }
    }

    public override void OnHPChanged(float before, float after)
    {
        base.OnHPChanged(before, after);

        HPBarHUD.GetComponent<Slider>().value = after / Data.MaxHP;
    }

    // Show/Hide HPBar HUD
    private void OnEnable()
    {
        // if data has sprite name key,
        const string SPRITENAME_KEY = "SpriteName";
        if (MonsterDatatable.Data [EnemyName].hasKey(SPRITENAME_KEY))
        {
            string SpriteName = MonsterDatatable.Data [EnemyName] [SPRITENAME_KEY];
            View.sprite = SpriteCollection.GetSpriteByName(SpriteName);
        }

        Data = new PawnDataModel(this);

        // if data has Speed key,
        const string SPEED_KEY = "Speed";
        if (MonsterDatatable.Data [EnemyName].hasKey(SPEED_KEY))
        {
            Data.Velocity = Vector3.down * MonsterDatatable.Data [EnemyName] [SPEED_KEY].AsFloat;
            Data.Velocity += Data.Acceleration;
        }

        // if data has HP key,
        const string HP_KEY = "HP";
        if (MonsterDatatable.Data [EnemyName].hasKey(HP_KEY))
        {
            Data.MaxHP = MonsterDatatable.Data [EnemyName] [HP_KEY].AsFloat;
            Data.HP = Data.MaxHP;

            if (null != HPBarHUD)
            {
                HPBarHUD.transform.SetParent(Game.Instance.HUDCanvas.transform);
                HPBarHUD.gameObject.SetActive(true);
            }
        } else
        {
            Data.IsInvincible = true;
            if (null != HPBarHUD)
            {
                HPBarHUD.gameObject.SetActive(false);
            }
        }

        // TODO: add hit radius key to data
        Data.HitRadius = 0.25F;

        // if data has missile key,
        const string MISSILE_KEY = "Missile";
        if (MonsterDatatable.Data [EnemyName].hasKey(MISSILE_KEY))
        {
            const string DELAY_KEY = "Delay";
            float Delay = MonsterDatatable.Data [EnemyName] [MISSILE_KEY] [DELAY_KEY].AsFloat;

            if (Delay > 0)
            {
                ShootDelay = new WaitForSeconds(Delay);
                StartCoroutine(SpawnMissile());
            }
        }
    }

    private IEnumerator SpawnMissile()
    {
        while (true)
        {
            yield return ShootDelay;

            Vector3 ShootPosition = transform.position;
            EnemyMissileSpawnControl.Instance.SpawnMissile(ShootPosition, EnemyName);
        }
    }
    
    private void OnDisable()
    {
        if (null != HPBarHUD)
        {
            HPBarHUD.gameObject.SetActive(false);
        }
    }
}
