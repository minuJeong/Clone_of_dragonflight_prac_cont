using UnityEngine;
using System.Collections;

public abstract class MissileSpawnControl : SpawnControl
{
    public ObjectPool<GameObject> MissilePool;
    public GameObject m_PrototypeMissile;
    private static MissileSpawnControl m_Instance;

    public static MissileSpawnControl Instance
    {
        get
        {
            return m_Instance;
        }
    }

    public override void Initialize()
    {
        if (null == m_Instance)
        {
            m_Instance = this;
        }

        base.Initialize();

//        MissilePool = new ObjectPool<GameObject>(100, () =>
//        {
//            GameObject AllocatedMissile = GameObject.Instantiate(m_PrototypeMissile);
//            AllocatedMissile.SetActive(false);
//            AllocatedMissile.transform.SetParent(Game.Instance.transform);
//            return AllocatedMissile;
//        });
    }

    protected override void Spawn()
    {
        GameObject SpawnedMissile = MissilePool.pop();
        SpawnedMissile.SetActive(true);
        SpawnedMissile.transform.position = transform.position;
    }
}
