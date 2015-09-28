using UnityEngine;
using System.Collections;

public sealed class EnemySpawnControl : SpawnControl
{
    public static ObjectPool<GameObject> EnemyPool;
    public float SpawnDelay = 4.5F;

    private const int SPAWN_COUNT = 5;
    private Rect ValidRect;
    private float STEP = 0.0F;

    // Set in UnityEditor - prefab (not scene object)
    public GameObject m_PrototypeEnemyPawn;

    public override void Initialize()
    {
        float w = Game.Instance.GameCamera.orthographicSize * Game.Instance.GameCamera.aspect * 2;
        float h = Game.Instance.GameCamera.orthographicSize * 2;

        ValidRect = new Rect (-w/2, -h/2, w, h);

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
        STEP = ValidRect.width / SPAWN_COUNT;
        for (int i = 0; i < SPAWN_COUNT; i++)
        {
            GameObject SpawnedEnemyPawn = EnemyPool.pop();
            SpawnedEnemyPawn.SetActive (true);

            Vector3 pos = new Vector3(ValidRect.x + (STEP * (i + 0.5F)), ValidRect.yMax - 0.01F, 0);
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
