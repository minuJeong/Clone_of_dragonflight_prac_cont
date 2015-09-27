using UnityEngine;
using System.Collections;

public abstract class Missile : MonoBehaviour
{
    public MissileDataModel Data;
    public GameObject[] EnemyGroup;
    
    // Update is called once per frame
    protected virtual void Update()
    {
        // Divided for readability
        UpdatePosition();
        UpdateEnemyGroup();
        UpdateCollision();
    }

    #region Update Submethods
    protected virtual void UpdatePosition()
    {
        // when path is not set, calculate next position by velocity, acceleration and friction
        if (Data.Path == null)
        {
            transform.position += Data.Velocity;
            Data.Velocity *= Data.Friction;
            Data.Velocity += Data.Acceleration;

            if (! Data.ValidArea.Contains (transform.position))
            {
                Die ();
            }
        } else
        // otherwise, get path by set path position
        {
            transform.position = Data.Path.GetPositionGradually(Time.deltaTime);
        }
    }

    protected abstract void UpdateEnemyGroup();

    protected virtual void UpdateCollision()
    {
        if (null == EnemyGroup || 0 == EnemyGroup.Length)
        {
            return;
        }

        int len = EnemyGroup.Length;
        for (int i = 0; i < len; i++)
        {
            GameObject target = EnemyGroup [i];
            float sqrtDistance = (transform.position - target.transform.position).sqrMagnitude;
            if (sqrtDistance < Data.CollisionDistanceSqrt)
            {
                OnCollision(target);
            }
        }
    }
    #endregion

    protected abstract void OnCollision(GameObject target);

    protected virtual void Die ()
    {

    }
}

/**
 * When Path is set, other variables will be ignored.
 */
public struct MissileDataModel
{
    public Vector3 Velocity, Acceleration;
    public float Friction, CollisionDistanceSqrt, Damage;
    public Rect ValidArea;
    public MissilePath Path;

    // ctor
    public MissileDataModel(Vector3 velocity, Vector3 acceleration,
                            float friction = 1.0F, float collisionDistSqrt = 0, float damage = 0,
                            Rect validArea,
                            MissilePath path = null)
    {
        this.Velocity = velocity;
        this.Acceleration = acceleration;
        this.Friction = friction;
        this.CollisionDistanceSqrt = collisionDistSqrt;
        this.Damage = damage;
        this.ValidArea = validArea;

        this.Path = path;
    }
}

/**
 * 
 */
public class MissilePath
{
    public Vector3[] Points;

    private float m_MaxLifeTime;
    public float MaxLifeTime
    {
        get
        {
            return m_MaxLifeTime;
        }
        set
        {
            m_MaxLifeTime = value;
            LifeTime = m_MaxLifeTime;
        }
    }

    public float LifeTime;

    public virtual Vector3 GetPosition(float progress)
    {
        progress = Mathf.Clamp01(progress);

        Vector3 sum = Vector3.zero;
        int len = Points.Length;
        for (int i = 0; i < len - 1; i++)
        {
            sum += Points [i] - Points [i + 1];
        }

        float progressDistance = sum.magnitude * progress;
        for (int i = 0; i < len - 1; i++)
        {
            Vector3 delta = Points [i] - Points [i + 1];
            progressDistance -= delta.magnitude;

            if (progressDistance < 0)
            {
                return Vector3.Lerp(Points [i + 1], Points [i], (-progressDistance / delta.magnitude));
            }
        }

        Debug.LogError("MissilePath: Unknown error.");
        return Vector3.zero;
    }

    /*
     * Calling this method will consume LifeTime, and returns next position of the missile path.
     */
    public virtual Vector3 GetPositionGradually(float deltaTime)
    {
        LifeTime -= deltaTime;
        return GetPosition(LifeTime / MaxLifeTime);
    }
}
