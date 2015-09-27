using UnityEngine;
using System.Collections;

public class PlayerMissile : Missile
{
    private const float SPEED = 0.015F;
    private void Start()
    {
        Data.Velocity = Vector3.up * SPEED;
        Data.Acceleration = Vector3.zero;
        Data.Friction = 1.0F;
    }

    // Find enemy pawns
    protected override void UpdateEnemyGroup()
    {
        EnemyPawn[] EnemyPawns = GameObject.FindObjectsOfType<EnemyPawn>();
        int len = EnemyPawns.Length;
        EnemyGroup = new GameObject[len];
        for (int i = 0; i < len; i++)
        {
            EnemyGroup [i] = EnemyPawns [i].gameObject;
        }
    }

    protected override void OnCollision(GameObject target)
    {

    }
}
