using UnityEngine;
using System.Collections;

public abstract class Pawn : MonoBehaviour
{
    public PawnDataModel Data;

    public virtual void Die()
    {
        gameObject.SetActive(false);
    }

    public virtual void OnHPChanged(float before, float after)
    {
    }
}

public class PawnDataModel
{
    private Pawn OwnerPawn;
    private float m_HP;

    public bool IsInvincible;

    public float HP
    {
        get
        {
            return m_HP;
        }
        set
        {
            if (m_HP != value)
            {
                OwnerPawn.OnHPChanged(m_HP, value);
            }

            m_HP = value;
            if (m_HP < 0)
            {
                OwnerPawn.Die();
                m_HP = 0;
            } else if (m_HP > MaxHP)
            {
                m_HP = MaxHP;
            }
        }
    }

    private float m_MaxHP;

    public float MaxHP
    {
        get
        {
            return m_MaxHP;
        }

        set
        {
            float delta = value - m_MaxHP;
            HP += delta;
            m_MaxHP = value;
        }
    }

    public Vector3 Velocity, Acceleration;
    public MovePath Path;
    public float HitRadius;

    public float DropItemRating;
    public int ScoreGain;

    public PawnDataModel(Pawn ownerPawn)
    {
        OwnerPawn = ownerPawn;
    }
}