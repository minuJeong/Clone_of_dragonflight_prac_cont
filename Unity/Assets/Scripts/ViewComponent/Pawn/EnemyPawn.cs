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
        if (null != HPBarHUD)
        {
            HPBarHUD.transform.SetParent(Game.Instance.HUDCanvas.transform);
            HPBarHUD.gameObject.SetActive(true);
        }

        string SpriteName = MonsterDatatable.Data [EnemyName] ["SpriteName"].ToString();
        float HP = float.Parse(MonsterDatatable.Data [EnemyName] ["HP"].ToString());
        float Speed = float.Parse(MonsterDatatable.Data [EnemyName] ["Speed"].ToString());

        Data = new PawnDataModel(this);
        Data.Velocity = Vector3.down * Speed;
        Data.Velocity += Data.Acceleration;
        Data.MaxHP = HP;
        Data.HP = Data.MaxHP;
        Data.HitRadius = 0.25F;

        View.sprite = SpriteCollection.GetSpriteByName(SpriteName);

        float Delay = float.Parse(MonsterDatatable.Data [EnemyName] ["Missile"] ["Delay"].ToString());
        if (Delay > 0)
        {
            ShootDelay = new WaitForSeconds(Delay);
            StartCoroutine("SpawnMissile");
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
