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
    [SerializeField] private TextMeshProUGUI gameOverScoreText;
    [SerializeField] private TextMeshProUGUI menuBestScore;
    [SerializeField] private TextMeshProUGUI gameOverBestScore;


    [Header("***COUNTERS - do NOT OVERRIDE!! ***")]
    [SerializeField] private int currentKillCount;

    void Start()
    {
        UpdateTexts();
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
        UpdateKillUI();
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

            case GameState.GameOver:
                //Update
                break;

            default:
                Debug.LogWarning($"Unhandled GameState: {state}");
                break;
        }

    }


    void UpdateTexts()
    {
        killMeterText.text =     $"Score: {currentKillCount.ToString()}";
        gameOverScoreText.text = $"Score: {currentKillCount.ToString()}";
        menuBestScore.text =     $"Best Score: {HighScore.ToString()}";
        gameOverBestScore.text = $"Best Score: {HighScore.ToString()}";
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

    internal void Reset()
    {
        SaveHighScore();
        currentKillCount = 0;
    }

    public int HighScore => PlayerPrefs.GetInt("HighScore");
}
