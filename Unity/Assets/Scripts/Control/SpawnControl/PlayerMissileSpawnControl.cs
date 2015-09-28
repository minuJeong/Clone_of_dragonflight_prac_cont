using UnityEngine;
using System.Collections;

public sealed class PlayerMissileSpawnControl : MissileSpawnControl
{
    private const float SPAWN_DELAY = 0.06F;
    private GameObject PlayerPawn;

    private void Awake ()
    {
        PlayerPawn = GameObject.FindObjectOfType<PlayerPawn> ().gameObject;
    }

    private void Start()
    {
        m_SpawnDelay = SPAWN_DELAY;

        Initialize ();

        MissilePool = new ObjectPool<GameObject>(100, () =>
        {
            GameObject AllocatedMissile = GameObject.Instantiate(m_PrototypeMissile);
            AllocatedMissile.SetActive(false);
            AllocatedMissile.transform.SetParent(Game.Instance.transform.FindChild("PlayerMissilePool"));
            return AllocatedMissile;
        });

        StartCoroutine("SpawnCycle");

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

    protected override void Spawn()
    {
        GameObject SpawnedMissile = MissilePool.pop();
        SpawnedMissile.SetActive(true);
        SpawnedMissile.transform.position = PlayerPawn.transform.position;
    }
}
