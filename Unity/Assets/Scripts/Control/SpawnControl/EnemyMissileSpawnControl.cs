using UnityEngine;
using System.Collections;

/**
 * Different from MissileSpawnControl
 */
public class EnemyMissileSpawnControl : MonoBehaviour
{
    private static EnemyMissileSpawnControl m_Instance;

    public static EnemyMissileSpawnControl Instance
    {
        get
        {
            if (null == m_Instance)
            {
                m_Instance = GameObject.FindObjectOfType<EnemyMissileSpawnControl>();

                if (null == m_Instance)
                {
                    GameObject container = new GameObject();
                    m_Instance = container.AddComponent<EnemyMissileSpawnControl>();
                }
            }
            return m_Instance;
        }
    }

    private static ObjectPool<GameObject> MissilePool;
    public GameObject m_ProtoyupeEnemyMissile;

    void Awake()
    {
        if (null == m_Instance)
        {
            m_Instance = this;
        }
    }

    // Use this for initialization
    void Start()
    {
        MissilePool = new ObjectPool<GameObject>(30, () =>
        {
            GameObject enemyMissile = GameObject.Instantiate(m_ProtoyupeEnemyMissile);
            enemyMissile.SetActive(false);
            enemyMissile.transform.SetParent(Game.Instance.transform);
            return enemyMissile;
        });
    }

    public void SpawnMissile(Vector3 pos, string enemyName)
    {
        GameObject SpawnedMissile = MissilePool.pop();
        SpawnedMissile.GetComponent<EnemyMissile>().EnemyName = enemyName;
        SpawnedMissile.transform.position = pos;
        SpawnedMissile.SetActive(true);
    }
}
