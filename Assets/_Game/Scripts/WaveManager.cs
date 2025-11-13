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
    [SerializeField] private float intervalBetweenSpawns;
    [SerializeField] private float enemySpawnDistance;
    [SerializeField] private float degreeDeflection;

    [Header("***Elements***")]
    [SerializeField] private Transform enemyContainer;
    [SerializeField] private GameObject[] enemyPrefabs;

    [Header("%%%Enemies%%%")]
    [SerializeField] private GameObject zombie;

    [Header("***Events for level***")]
    [SerializeField] private LevelEventData[] levelEventDatas;

    [Header("---Runtime Variables---")]
    [SerializeField] private float elapsedTimeSinceLevelStarted;
    [SerializeField] private float timeSinceLastSpawn;
    [SerializeField] private Queue<int> lastSpawnDegrees = new();


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
        if (GameStateManager.Instance.CurrentGameState == GameState.Play)
        {
            PassLevelTime();
        }

    }

    private void FixedUpdate()
    {

        if (timeSinceLastSpawn > intervalBetweenSpawns)
        {
            timeSinceLastSpawn = 0f;
            SpawnEnemy();
            intervalBetweenSpawns -= 0.01f;
        }
    }

    private int GetEnemySpawnDegree()
    {
        if (lastSpawnDegrees.Count == 0)
            return Random.Range(0, 360);

        float sumX = 0f;
        float sumY = 0f;

        foreach (int deg in lastSpawnDegrees)
        {
            float rad = deg * Mathf.Deg2Rad;
            sumX += Mathf.Cos(rad);
            sumY += Mathf.Sin(rad);
        }

        float meanRad = Mathf.Atan2(sumY, sumX);
        float meanDeg = meanRad * Mathf.Rad2Deg;

        float deflection = Random.Range(-degreeDeflection, degreeDeflection);
        int result = Mathf.RoundToInt(meanDeg + deflection);

        if (result < 0) result += 360;
        if (result > 360) result -= 360;

        return result;
    }

    private Vector3 GetEnemySpawnCoordinate()
    {
        int degree = GetEnemySpawnDegree();
        float rad = degree * Mathf.Deg2Rad;

        float x = Mathf.Cos(rad);
        float y = Mathf.Sin(rad);

        //Debug.Log($"ENEMY WILL SPAWN AT {x:F2}, {y:F2} (degree: {degree})");

        return new Vector3(x, y, 0f) * enemySpawnDistance;
    }

    private void SpawnEnemy()
    {
        var enemy = Instantiate(zombie, GetEnemySpawnCoordinate(), Quaternion.identity);
        enemy.transform.SetParent(enemyContainer);
    }

    private void PassLevelTime()
    {
        timeSinceLastSpawn += Time.deltaTime;
        elapsedTimeSinceLevelStarted += Time.deltaTime;
    }

    public void Reset()
    {
        StartWave();
        RemoveAllEnemies();
    }

    private void RemoveAllEnemies()
    {
        for (int i = enemyContainer.childCount - 1; i >= 0; i--)
        {
            enemyContainer.GetChild(i).GetComponent<Enemy>().Detonate();
        }
    }

}
