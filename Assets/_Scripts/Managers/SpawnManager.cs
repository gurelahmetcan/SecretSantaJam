using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private Spawner[] _spawners;

    private bool canSpawn = true;
    
    private void Update()
    {
        if (!canSpawn) return;
        foreach (var spawner in _spawners)
        {
            spawner.SpawnEnemy();
        }
    }
}
