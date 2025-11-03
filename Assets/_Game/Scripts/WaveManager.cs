using UnityCommunity.UnitySingleton;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public enum WaveEventType
{
    None,
    SpawnHorde,
    SpawnBoss
}

public class WaveManager : PersistentMonoSingleton<WaveManager>
{

    [Header("***Settings***")]
    [SerializeField] private Vector2 spawnPoint = Vector3.up * 2;
    [SerializeField] private float intervalBetweenSpawns;

    [Header("***Elements***")]
    [SerializeField] private Transform enemyContainer;
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private float elapsedTimeSinceLevelStarted;

    [Header("%%%Enemies%%%")]
    [SerializeField] private GameObject zombie;

    [Header("***Events for level***")]
    [SerializeField] private LevelEventData[] levelEventDatas;
    [SerializeField] private float enemySpawnDistance;

    [Header("---Runtime Variables---")]
    [SerializeField] private float timeSinceLastSpawn;
    [SerializeField] private Queue<int> lastSpawnDegrees = new();
    [SerializeField] private float degreeDeflection;


    void Start()
    {
        StartWave();
    }

    private void StartWave()
    {
        elapsedTimeSinceLevelStarted = 0f;
        timeSinceLastSpawn = intervalBetweenSpawns - 0.5f;
    }

    void Update()
    {
        PassLevelTime();
        
    }

    private void FixedUpdate()
    {
        timeSinceLastSpawn += Time.fixedDeltaTime;
        if(timeSinceLastSpawn > intervalBetweenSpawns) 
        {
            timeSinceLastSpawn = 0f;
            SpawnEnemy();
        }
    }

    private int GetEnemySpawnDegree()
    {
        if (lastSpawnDegrees.Count < 1) return 0;
        int degree = 0;
        double meanDegree = lastSpawnDegrees.Average();
        degree += (int)meanDegree + 10;
        return (int)(degree + Mathf.Ceil(Random.Range(0, degreeDeflection)));

    }

    private Vector3 GetEnemySpawnCoordinate()
    {
        int degree = GetEnemySpawnDegree();
        int x = (int)Mathf.Cos(degree);
        int y = (int)Mathf.Sin(degree);
        Debug.Log($"ENEMY WILL SPAWN AT {x},{y} DEGREE: {degree}");
        return new Vector3(x, y, 0) * enemySpawnDistance;
    }

    private void SpawnEnemy()
    {
        var enemy = Instantiate(zombie, GetEnemySpawnCoordinate(), Quaternion.identity);
    }

    private void PassLevelTime()
    {
        elapsedTimeSinceLevelStarted += Time.deltaTime;
    }


}
