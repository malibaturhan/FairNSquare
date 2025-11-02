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

    [Header("***Events for level***")]
    [SerializeField] private LevelEventData[] levelEventDatas;



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

    private void PassLevelTime()
    {
        elapsedTimeSinceLevelStarted += Time.deltaTime;
    }
}
