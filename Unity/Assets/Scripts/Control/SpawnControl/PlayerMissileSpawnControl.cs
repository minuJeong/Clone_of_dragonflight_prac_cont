using UnityEngine;
using System.Collections;

public sealed class PlayerMissileSpawnControl : MissileSpawnControl
{
    private const float SPAWN_DELAY = 0.05F;

    private void Start()
    {
        m_SpawnDelay = SPAWN_DELAY;
        Initialize();
        StartSpawn();
    }

    // override for new coroutine cycle
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
