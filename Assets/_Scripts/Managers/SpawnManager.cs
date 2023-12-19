using System;
using System.Collections;
using System.Collections.Generic;
using SantaProject;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private Spawner[] _spawners;

    private bool canSpawn = true;
    private int enemyCount;
    private int waveCount = 1;
    
    private void Start()
    {
        EventManager.Instance.onEnemySpawned += CheckEnemyCount;
    }

    private void OnDestroy()
    {
        EventManager.Instance.onEnemySpawned -= CheckEnemyCount;
    }

    private void Update()
    {
        if (!canSpawn) return;
        foreach (var spawner in _spawners)
        {
            spawner.SpawnEnemy();
        }
    }

    private void CheckEnemyCount()
    {
        enemyCount++;
        Debug.Log(enemyCount);
    }
}
