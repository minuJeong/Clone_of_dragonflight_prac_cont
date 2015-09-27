using UnityEngine;
using System.Collections;

public class EnemySpawnControl : SpawnControl
{
    public static ObjectPool<GameObject> EnemyPool;

    private const int SPAWN_COUNT = 5;
    private const float LEFT = -0.5F;
    private const float RIGHT = 0.5F;
    private const float TOP = 2.0F;
    private const float STEP = (RIGHT - LEFT) / SPAWN_COUNT;

    // Set in UnityEditor - prefab (not scene object)
    public GameObject m_PrototypeEnemyPawn;

    public override void Initialize()
    {
        m_SpawnDelay = 5.0F;

        // base.Initialize need to be called.
        base.Initialize();

        EnemyPool = new ObjectPool<GameObject>(10, () =>
        {
            GameObject enemyPawn = GameObject.Instantiate(m_PrototypeEnemyPawn);
            enemyPawn.SetActive(false);
            enemyPawn.transform.SetParent (transform);
            return enemyPawn;
        });
    }

    protected override void Spawn()
    {
        for (int i = 0; i < SPAWN_COUNT; i++)
        {
            GameObject SpawnedEnemyPawn = EnemyPool.pop();
            SpawnedEnemyPawn.SetActive (true);

            Vector3 pos = new Vector3(LEFT + (STEP * i), TOP, 0);
            SpawnedEnemyPawn.transform.position = pos;
        }
    }
}
