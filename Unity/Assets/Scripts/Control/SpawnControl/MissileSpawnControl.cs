using UnityEngine;
using System.Collections;

public abstract class MissileSpawnControl : SpawnControl
{
    public static ObjectPool<GameObject> MissilePool;

    public GameObject m_PrototypeMissile;

    public override void Initialize()
    {
        base.Initialize();

        MissilePool = new ObjectPool<GameObject>(100, () =>
        {
            GameObject AllocatedMissile = GameObject.Instantiate(m_PrototypeMissile);
            AllocatedMissile.SetActive(false);
            AllocatedMissile.transform.SetParent(transform);
            return AllocatedMissile;
        });
    }

    protected override void Spawn()
    {
        GameObject SpawnedMissile = MissilePool.pop();
        SpawnedMissile.SetActive(true);

        SpawnedMissile.transform.localPosition = Vector3.zero;
        SpawnedMissile.transform.SetParent(transform);
    }
}
