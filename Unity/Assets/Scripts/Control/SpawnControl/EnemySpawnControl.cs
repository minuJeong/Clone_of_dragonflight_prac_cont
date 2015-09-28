using UnityEngine;
using System.Collections;

public sealed class EnemySpawnControl : SpawnControl
{
    public static ObjectPool<GameObject> EnemyPool;
    public float SpawnDelay = 0.125F;
    private const int SPAWN_COUNT = 9;
    private Rect ValidRect;
    private int CurrentRow = 0;
    private float STEP = 0.0F;

    // Set in UnityEditor - prefab (not scene object)
    public GameObject m_PrototypeEnemyPawn;

    public override void Initialize()
    {
        float w = Game.Instance.GameCamera.orthographicSize * Game.Instance.GameCamera.aspect * 2;
        float h = Game.Instance.GameCamera.orthographicSize * 2;

        ValidRect = new Rect(-w / 2, -h / 2, w, h);

        m_SpawnDelay = SpawnDelay;

        // base.Initialize need to be called.
        base.Initialize();

        EnemyPool = new ObjectPool<GameObject>(10, () =>
        {
            GameObject enemyPawn = GameObject.Instantiate(m_PrototypeEnemyPawn);
            enemyPawn.SetActive(false);
            enemyPawn.transform.SetParent(Game.Instance.transform.FindChild ("EnemyPawnPool"));
            return enemyPawn;
        });
    }

    protected override void Spawn()
    {
        STEP = ValidRect.width / SPAWN_COUNT;

        SpawnDataModel[] dataSet = PawnSpawnPatternControl.Instance.ParseTexture("Pattern_1", CurrentRow);
        int len = dataSet.Length;
        Vector3 pos;
        string EnemyName;

        for (int i = 0; i < len; i++)
        {
            SpawnDataModel data = dataSet [i];
            if (null == data)
            {
                continue;
            }

            switch (data.SpawnPattern)
            {
                case PawnSpawnPatternControl.SpawnPattern.NONE:
                    continue;
//                    break;

                case PawnSpawnPatternControl.SpawnPattern.CURRENT_LEVEL:
                    pos = new Vector3(ValidRect.x + (STEP * (data.Position + 0.5F)), ValidRect.yMax - 0.01F, 0);
                    EnemyName = DifficultyControl.Instance.GetCurrentEnemyName(0);
                    SpawnOneEnemy(pos, EnemyName);
                    break;

                case PawnSpawnPatternControl.SpawnPattern.CURRENT_LEVEL_PLUS_1:
                    pos = new Vector3(ValidRect.x + (STEP * (data.Position + 0.5F)), ValidRect.yMax - 0.01F, 0);
                    EnemyName = DifficultyControl.Instance.GetCurrentEnemyName(1);
                    SpawnOneEnemy(pos, EnemyName);
                    break;

                case PawnSpawnPatternControl.SpawnPattern.CURRENT_LEVEL_PLUS_2:
                    pos = new Vector3(ValidRect.x + (STEP * (data.Position + 0.5F)), ValidRect.yMax - 0.01F, 0);
                    EnemyName = DifficultyControl.Instance.GetCurrentEnemyName(2);
                    SpawnOneEnemy(pos, EnemyName);
                    break;

                case PawnSpawnPatternControl.SpawnPattern.OBSTACLE_BREAKABLE:
                    continue;
//                    break;

                case PawnSpawnPatternControl.SpawnPattern.OBSTACLE_UNBREAKABLE:
                    continue;
//                    break;
            }
        }

        CurrentRow++;
    }

    private void SpawnOneEnemy(Vector3 position, string enemyName)
    {
        GameObject SpawnedEnemyPawn = EnemyPool.pop();
        SpawnedEnemyPawn.transform.position = position;
        SpawnedEnemyPawn.GetComponent<EnemyPawn>().EnemyName = enemyName;
        SpawnedEnemyPawn.SetActive(true);
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
