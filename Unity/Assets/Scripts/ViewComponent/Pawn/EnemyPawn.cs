using UnityEngine;
using System.Collections;

public sealed class EnemyPawn : Pawn
{
    private const float SPEED = 0.025F;
    private Rect VALID_RECT = new Rect(-0.6F, -2.0F, 1.2F, 4.0F);

    protected override void Start()
    {
        base.Start();
        if (null == Data.Path)
        {
            Data.Velocity = Vector3.down * SPEED;
            Data.Velocity += Data.Acceleration;
            Data.MaxHP = 100.0F;
            Data.HP = Data.MaxHP;
            Data.HitRadius = 0.01F;
        }
    }

    private void Update()
    {
        if (transform.position.y < VALID_RECT.yMin)
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
        }
    }

    public override void Die()
    {
        if (null != EnemySpawnControl.EnemyPool)
        {
            gameObject.SetActive(false);
            EnemySpawnControl.EnemyPool.push(gameObject);
        }
    }
}
