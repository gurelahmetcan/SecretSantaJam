using System;
using System.Collections;
using System.Collections.Generic;
using SantaProject;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private Spawner[] _spawners;
    [SerializeField] private Transform bossSpawnPos;
    [SerializeField] private GameObject bossPrefab;

    private bool canSpawn = true;
    private bool eventSent;
    private bool bossSpawn;
    
    private int enemyCount;
    private int waveCount = 1;
    private int enemyAmount = 5;

    private List<GameObject> _enemies = new();
    
    private void Start()
    {
        EventManager.Instance.onEnemySpawned += CheckEnemyCount;
        EventManager.Instance.enemyDeadEvent += OnEnemyDead;
        EventManager.Instance.onWaveBreakEnds += PrepareNewWave;
    }

    private void OnDestroy()
    {
        EventManager.Instance.onEnemySpawned -= CheckEnemyCount;
        EventManager.Instance.enemyDeadEvent -= OnEnemyDead;
        EventManager.Instance.onWaveBreakEnds -= PrepareNewWave;
    }

    private void Update()
    {
        if (canSpawn && waveCount <= 3)
        {
            foreach (var spawner in _spawners)
            {
                spawner.SpawnEnemy();
            }
        }

        if (!canSpawn && _enemies.Count == 0 && !eventSent)
        {
            eventSent = true;
            waveCount++;

            if (waveCount == 3)
            {
                bossSpawn = true;
            }
            
            EventManager.Instance.onWaveEnd.Invoke(waveCount);
        }

        if (bossSpawn)
        {
            bossSpawn = false;
            SpawnBoss();
        }
    }

    private void CheckEnemyCount(GameObject enemy)
    {
        enemyCount++;
       _enemies.Add(enemy);
        if (enemyCount >= enemyAmount)
        {
            canSpawn = false;
        }
    }

    private void OnEnemyDead(GameObject transform1)
    {
        var enemy = _enemies.Find(x => x == transform1);
        _enemies.Remove(enemy);
    }

    private void PrepareNewWave()
    {
        enemyCount = 0;
        enemyAmount += 5;
        canSpawn = true;
        eventSent = false;
    }

    private void SpawnBoss()
    {
        Instantiate(bossPrefab, bossSpawnPos.position, bossSpawnPos.rotation);
    }
}
