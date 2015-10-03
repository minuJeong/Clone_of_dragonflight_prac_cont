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

    public ObjectPool<GameObject> MissilePool;
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
            enemyMissile.transform.SetParent(Game.Instance.transform.FindChild ("EnemyMissilePool"));
            return enemyMissile;
        });
    }

    public void SpawnMissile(Vector3 pos, string enemyName)
    {
        GameObject SpawnedMissile_1 = MissilePool.pop();
        SpawnedMissile_1.GetComponent<EnemyMissile>().EnemyName = enemyName;
        SpawnedMissile_1.transform.position = pos;
        SpawnedMissile_1.SetActive(true);
        SpawnedMissile_1.GetComponent<EnemyMissile> ().Data.Velocity.x += Mathf.Cos (60.0F * Mathf.Deg2Rad) * 5;

        GameObject SpawnedMissile_2 = MissilePool.pop();
        SpawnedMissile_2.GetComponent<EnemyMissile>().EnemyName = enemyName;
        SpawnedMissile_2.transform.position = pos;
        SpawnedMissile_2.SetActive(true);
        SpawnedMissile_2.GetComponent<EnemyMissile> ().Data.Velocity.x -= Mathf.Cos (60.0F * Mathf.Deg2Rad) * 5;
    }
}
