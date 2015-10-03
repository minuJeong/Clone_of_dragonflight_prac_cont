using UnityEngine;
using System.Collections;

public abstract class RepeatedlySpawnControl : MonoBehaviour
{
    protected bool m_IsInitialized;
    protected bool m_IsSpawning;
    protected float m_SpawnDelay;
    protected WaitForSeconds m_WaitTime;

    // Should implement pooling in child class
    public virtual void Initialize()
    {
        m_IsInitialized = true;
        m_WaitTime = new WaitForSeconds(m_SpawnDelay);
        StartCoroutine(SpawnCycle());
    }

    public virtual void StartSpawn()
    {
        if (m_IsInitialized)
        {
            m_IsSpawning = true;
        }
    }

    public virtual void StopSpawn()
    {
        m_IsSpawning = false;
    }

    protected virtual IEnumerator SpawnCycle()
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

    protected abstract void Spawn();
}
