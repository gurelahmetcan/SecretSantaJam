using System;
using SantaProject;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;
public class Spawner : MonoBehaviour
{
    [SerializeField] private Enemy[] enemies;
    [SerializeField] private float _spawnTimer;
    
    private bool _canSpawn = true;
    private Vector3 _offset = Vector3.zero;
    private float baseTime;
    private double accumulatedWeights;
    private System.Random rand = new System.Random();

    #region Unity Methods

    private void Awake()
    {
        baseTime = _spawnTimer;
        CalculateWeights();
    }

    private void Update()
    {
        _spawnTimer -= Time.deltaTime;
    }

    #endregion
    
    public void SpawnEnemy()
    {
        if (!(_spawnTimer <= 0) || !_canSpawn) return;

        Enemy randomEnemy = enemies[GetRandomEnemyIndex()];
        
        Instantiate(randomEnemy.prefab, transform.position + GetOffset(), transform.rotation);
        
        _spawnTimer = baseTime;
    }

    #region Private Methods

    private void CalculateWeights()
    {
        accumulatedWeights = 0f;
        foreach (var enemy in enemies)
        {
            accumulatedWeights += enemy.chance;
            enemy.weight = accumulatedWeights;
        }
    }

    private int GetRandomEnemyIndex()
    {
        double r = rand.NextDouble() * accumulatedWeights;

        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].weight >= r)
            {
                return i;
            }
        }

        return 0;
    }

    private Vector3 GetOffset()
    {
        int number = Random.Range(-3, 3);
        _offset += new Vector3(number, 0f, number);
        
        return _offset;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _canSpawn = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _canSpawn = true;
        }
    }

    #endregion
}
