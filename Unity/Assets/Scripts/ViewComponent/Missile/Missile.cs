﻿using UnityEngine;
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
        this.gameObject.SetActive (false);
        MissileSpawnControl.MissilePool.push (this.gameObject);
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
    public MovePath Path;

    // ctor
    public MissileDataModel(Vector3 velocity, Vector3 acceleration,
                            float friction, float collisionDistSqrt, float damage,
                            Rect validArea,
                            MovePath path = null)
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
