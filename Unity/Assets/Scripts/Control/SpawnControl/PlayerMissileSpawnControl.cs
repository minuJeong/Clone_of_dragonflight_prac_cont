using UnityEngine;
using System.Collections;

public sealed class PlayerMissileSpawnControl : RepeatedlySpawnControl
{
    private const float SPAWN_DELAY = 0.06F;

    public ObjectPool<GameObject> MissilePool;
    public GameObject m_PrototypeMissile;
    private GameObject PlayerPawn;

    private static PlayerMissileSpawnControl m_Instance;
    
    public static PlayerMissileSpawnControl Instance
    {
        get
        {
            if (null == m_Instance)
            {
                m_Instance = GameObject.FindObjectOfType<PlayerMissileSpawnControl>();
                if (null == m_Instance)
                {
                    GameObject container = new GameObject();
                    m_Instance = container.AddComponent<PlayerMissileSpawnControl>();
                }
            }
            return m_Instance;
        }
    }

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
