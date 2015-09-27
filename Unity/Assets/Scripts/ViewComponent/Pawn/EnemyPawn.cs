using UnityEngine;
using System.Collections;

public class EnemyPawn : Pawn
{
    private const float SPEED = 0.025F;
    private Rect VALID_RECT = new Rect(-0.6F, -2.0F, 1.2F, 4.0F);

    private Vector3 m_Velocity;

    protected override void Start()
    {
        m_Velocity = Vector3.down * SPEED;
    }

    protected override void Update()
    {
        if (transform.position.y < VALID_RECT.yMin)
        {
            Die();
        } else
        {
            transform.position += m_Velocity;
        }
    }

    protected override void Die()
    {
        if (null != EnemySpawnControl.EnemyPool)
        {
            gameObject.SetActive(false);
            EnemySpawnControl.EnemyPool.push(gameObject);
        }
    }
}
