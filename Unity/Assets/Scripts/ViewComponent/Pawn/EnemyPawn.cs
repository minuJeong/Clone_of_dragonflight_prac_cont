using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public sealed class EnemyPawn : Pawn
{
    private const float SPEED = 0.035F;
    private Rect VALID_RECT;
    public RectTransform HPBarHUD;

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

            HPBarHUD.position = RectTransformUtility.WorldToScreenPoint(Game.Instance.GameCamera, transform.position) - Vector2.up * 30.0F;
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

        Data = new PawnDataModel(this);
        Data.Velocity = Vector3.down * SPEED;
        Data.Velocity += Data.Acceleration;
//        Data.Path = new MovePath ();
//        Data.Path.MaxLifeTime = 5.0F;
//        Data.Path.LifeTime = Data.Path.MaxLifeTime;
//        Data.Path.Points = new Vector3[]
//        {
//            new Vector3 (-.5F, 0, 0),
//            new Vector3 (0, -.5F, 0),
//            new Vector3 (0, .5F, 0),
//            new Vector3 (.5F, .5F, 0)
//        };

        Data.MaxHP = 100.0F;
        Data.HP = Data.MaxHP;
        Data.HitRadius = 0.1F;
    }
    
    private void OnDisable()
    {
        if (null != HPBarHUD)
        {
            HPBarHUD.gameObject.SetActive(false);
        }
    }
}
