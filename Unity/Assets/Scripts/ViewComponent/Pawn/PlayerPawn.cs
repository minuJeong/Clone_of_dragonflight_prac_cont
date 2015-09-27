using UnityEngine;
using System.Collections;

public sealed class PlayerPawn : Pawn
{
    #region const
    private const float LEFT_CAGE = -0.5F;
    private const float RIGHT_CAGE = 0.5F;
    #endregion

    #region public:
    public void MoveBy(Vector3 amount)
    {
        Vector3 NextPos = transform.position;
        NextPos += amount;
        NextPos.x = Mathf.Max(LEFT_CAGE, NextPos.x);
        NextPos.x = Mathf.Min(RIGHT_CAGE, NextPos.x);

        transform.position = NextPos;
    }
    #endregion


    protected override void Start()
    {
        base.Start();

        Data.MaxHP = 100.0F;
    }

    public override void Die()
    {
        Debug.Log("GAMEOVER: PlayerPawn is Dead.");
        base.Die();
    }
}