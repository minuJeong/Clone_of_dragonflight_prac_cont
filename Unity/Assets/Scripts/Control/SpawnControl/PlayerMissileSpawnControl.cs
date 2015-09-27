using UnityEngine;
using System.Collections;

public class PlayerMissileSpawnControl : MissileSpawnControl
{
    protected void Start()
    {
        m_SpawnDelay = 0.25F;
        Initialize();
        StartSpawn();
    }
}
