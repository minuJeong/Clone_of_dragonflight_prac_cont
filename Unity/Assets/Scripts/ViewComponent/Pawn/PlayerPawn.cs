using UnityEngine;
using System.Collections;

public sealed class PlayerPawn : Pawn
{
    #region const
    private float LEFT_CAGE;
    private float RIGHT_CAGE;
    #endregion

    void Start()
    {
        float arm = Game.Instance.GameCamera.orthographicSize * Game.Instance.GameCamera.aspect;
        LEFT_CAGE = - arm;
        RIGHT_CAGE = arm;
    }

    private bool IsInvincible = false;

    void Update()
    {
        /// DEBUG CHEAT
        if (Input.GetKeyDown(KeyCode.Space))
        {
            IsInvincible = ! IsInvincible;
        }
        ///////

        EnemyPawn[] enemies = GameObject.FindObjectsOfType <EnemyPawn>();
        foreach (var enemy in enemies)
        {
            float sqrDistance = (transform.position - enemy.transform.position).sqrMagnitude;

            if (sqrDistance < enemy.Data.HitRadius)
            {
                Data.HP -= enemy.Data.HP;
                enemy.Die();
            }
        }

        DropItem[] items = GameObject.FindObjectsOfType<DropItem>();
        foreach (var item in items)
        {
            float sqrDistance = (transform.position - item.transform.position).sqrMagnitude;
            if (sqrDistance < item.m_HitRadius)
            {
                item.OnGather ();
                item.Die();
            }
        }
    }

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


    private void OnEnable()
    {
        Data = new PawnDataModel(this);
        Data.MaxHP = 1.0F;
        Data.HP = Data.MaxHP;
    }

    public override void Die()
    {
        // Debug cheat
        if (IsInvincible)
        {
            Data.HP = Data.MaxHP;
            return;
        }

        Debug.Log("GAMEOVER: PlayerPawn is Dead.");
        base.Die();

        Application.LoadLevel(App.RESULT_LEVEL);
    }
}