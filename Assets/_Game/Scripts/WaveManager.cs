using UnityCommunity.UnitySingleton;
using UnityEngine;
using System;

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
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private float elapsedTimeSinceLevelStarted;

    [Header("%%%Enemies%%%")]
    [SerializeField] private GameObject zombie;

    [Header("***Events for level***")]
    [SerializeField] private LevelEventData[] levelEventDatas;

    [Header("---Runtime Variables---")]
    [SerializeField] private float timeSinceLastSpawn;


    void Start()
    {
        StartWave();
    }

    private void StartWave()
    {
        elapsedTimeSinceLevelStarted = 0f;
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

    private void SpawnEnemy()
    {
        var enemy = Instantiate(zombie, spawnPoint, Quaternion.identity);
        Debug.Log("***********************ZOMBIE CREATED");
    }

    private void PassLevelTime()
    {
        elapsedTimeSinceLevelStarted += Time.deltaTime;
    }
}
