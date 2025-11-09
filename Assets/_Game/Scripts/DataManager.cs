using System;
using TMPro;
using UnityCommunity.UnitySingleton;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DataManager : PersistentMonoSingleton<DataManager>
{
    [Header("***Elements***")]
    [SerializeField] private TextMeshProUGUI killMeterText;


    [Header("***COUNTERS - do NOT OVERRIDE!! ***")]
    [SerializeField] private int currentKillCount;

    void Start()
    {

    }

    private void OnEnable()
    {
        GameStateManager.OnGameStateChanged += GameStateChangedCallback;
    }

    private void OnDisable()
    {
        GameStateManager.OnGameStateChanged -= GameStateChangedCallback;
    }

    private void GameStateChangedCallback(GameState state)
    {
        switch (state)
        {
            case GameState.MainMenu:
                ResetKillCount();
                break;

            case GameState.Play:
                break;

            case GameState.Pause:
                break;

            case GameState.LevelUp:
                break;

            default:
                Debug.LogWarning($"Unhandled GameState: {state}");
                break;
        }

    }


    void Update()
    {

    }

    public void IncreaseKill()
    {
        currentKillCount++;
        UpdateKillUI();
    }

    private void UpdateKillUI()
    {
        killMeterText.text = currentKillCount.ToString();
    }

    private void SaveHighScore()
    {
        var currentHighScore = PlayerPrefs.GetInt("HighScore", 0);
        if (currentHighScore < currentKillCount)
        {
            PlayerPrefs.SetInt("HighScore", currentKillCount);
        }
    }

    private void ResetKillCount()
    {
        currentKillCount = 0;
    }
}
