using UnityEngine;
using System.Collections;

public class DropItemSpawnControl : MonoBehaviour
{
    public ObjectPool<GameObject> DropItemPool;
    public GameObject m_PrototypeItem;
    private static DropItemSpawnControl m_Instance;

    public static DropItemSpawnControl Instance
    {
        get
        {
            if (null == m_Instance)
            {
                m_Instance = GameObject.FindObjectOfType<DropItemSpawnControl>();
                if (null == m_Instance)
                {
                    GameObject container = new GameObject();
                    m_Instance = container.AddComponent<DropItemSpawnControl>();
                }
            }
            return m_Instance;
        }
    }

    private void Start()
    {
        DropItemPool = new ObjectPool<GameObject>(10, () =>
        {
            GameObject spawnedItem = GameObject.Instantiate(m_PrototypeItem);
            spawnedItem.SetActive(false);
            spawnedItem.transform.SetParent(Game.Instance.transform.FindChild ("ItemPool"));
            return spawnedItem;
        });
    }

    public void Spawn(Vector3 pos, float rating)
    {
        GameObject spawnedItem = DropItemPool.pop ();
        spawnedItem.SetActive (true);
        spawnedItem.transform.position = pos;
    }
}
