using UnityEngine;
using System.Collections;

public sealed class PlayerMissile : Missile
{
    private const float SPEED = 0.12F;

    private void OnEnable()
    {
        Data.Velocity = Vector3.up * SPEED;
        Data.Acceleration = Vector3.zero;
        Data.Friction = 1.0F;

        float w = Game.Instance.GameCamera.orthographicSize * Game.Instance.GameCamera.aspect * 2;
        float h = Game.Instance.GameCamera.orthographicSize * 2;

        Data.ValidArea = new Rect(-w / 2, -h / 2, w + 0.01F, h);
        Data.CollisionDistanceSqrt = 0.1F;
        Data.Damage = 15.0F;
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
