using UnityEngine;
using System.Collections;

public sealed class PlayerMissile : Missile
{
    private const float SPEED = 0.035F;

    private void Start()
    {
        Data.Velocity = Vector3.up * SPEED;
        Data.Acceleration = Vector3.zero;
        Data.Friction = 1.0F;
        Data.ValidArea = new Rect(-0.5F, -1.2F, 1.0F, 2.4F);
        Data.CollisionDistanceSqrt = 0.01F;
        Data.Damage = 10.0F;
    }

    // Find enemy pawns
    protected override void UpdateTargetGroup()
    {
        EnemyPawn[] EnemyPawns = GameObject.FindObjectsOfType<EnemyPawn>();
        int len = EnemyPawns.Length;
        TargetGroup = new GameObject[len];
        for (int i = 0; i < len; i++)
        {
            TargetGroup [i] = EnemyPawns [i].gameObject;
        }
    }

    protected override void OnCollision(GameObject target)
    {
        EnemyPawn enemyPawn = target.GetComponent<EnemyPawn>();
        if (null == enemyPawn)
        {
            return;
        }

        enemyPawn.Data.HP -= Data.Damage;
        Die();
    }

    protected override void Die()
    {
        base.Die();
    }
}
