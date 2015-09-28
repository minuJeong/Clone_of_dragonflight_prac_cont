using UnityEngine;
using System.Collections;

public sealed class EnemySpawnControl : SpawnControl
{
    public static ObjectPool<GameObject> EnemyPool;

    private const int SPAWN_COUNT = 5;
    private const float LEFT = -0.5F;
    private const float RIGHT = 0.5F;
    private const float TOP = 1.1F;
    private const float STEP = (RIGHT - LEFT) / SPAWN_COUNT;
    private float SpawnDelay = 2.5F;

    // Set in UnityEditor - prefab (not scene object)
    public GameObject m_PrototypeEnemyPawn;

    public override void Initialize()
    {
        m_SpawnDelay = SpawnDelay;

        // base.Initialize need to be called.
        base.Initialize();

        EnemyPool = new ObjectPool<GameObject>(10, () =>
        {
            GameObject enemyPawn = GameObject.Instantiate(m_PrototypeEnemyPawn);
            enemyPawn.SetActive(false);
            enemyPawn.transform.SetParent (Game.Instance.transform);
            return enemyPawn;
        });
    }

    protected override void Spawn()
    {
        for (int i = 0; i < SPAWN_COUNT; i++)
        {
            GameObject SpawnedEnemyPawn = EnemyPool.pop();
            SpawnedEnemyPawn.SetActive (true);

            Vector3 pos = new Vector3(LEFT + (STEP * (i + 0.5F)), TOP, 0);
            SpawnedEnemyPawn.transform.position = pos;
        }
    }

    protected override IEnumerator SpawnCycle()
    {
        while (true)
        {
            if (m_IsSpawning)
            {
                Spawn();
            }
            yield return m_WaitTime;
        }
    }
}
