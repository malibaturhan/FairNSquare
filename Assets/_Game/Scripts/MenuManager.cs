using System;
using UnityCommunity.UnitySingleton;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuManager : PersistentMonoSingleton<MenuManager>
{

    [Header("***Elements***")]
    [SerializeField] private CanvasGroup levelUpMenuCG;
    [SerializeField] private CanvasGroup pauseMenuCG;
    [SerializeField] private CanvasGroup mainMenuCG;
    [SerializeField] private CanvasGroup gameOverCG;
    [SerializeField] private CanvasGroup[] allMenuCanvasGroupes;


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
                ShowMenu(mainMenuCG);
                break;

            case GameState.Play:
                ShowMenu(null);
                break;

            case GameState.Pause:
                ShowMenu(pauseMenuCG);
                break;

            case GameState.LevelUp:
                ShowMenu(levelUpMenuCG);
                break;

            case GameState.GameOver:
                ShowMenu(gameOverCG);
                break;

            default:
                Debug.LogWarning($"Unhandled GameState: {state}");
                break;
        }

    }

    private void ShowGameOver()
    {
        throw new NotImplementedException();
    }

    private void ShowMenu(CanvasGroup cgToShow)
    {
        foreach (var cg in allMenuCanvasGroupes)
        {
            cg.alpha = 0f;
            cg.interactable = false;
            cg.blocksRaycasts = false;
        }

        if (cgToShow == null)
        {
            return; // with this option we can hide all menus and just show game ui,
        }
        //Debug.Log("now shown: " +cgToShow.gameObject.name);
        cgToShow.alpha = 1f;
        cgToShow.interactable = true;
        cgToShow.blocksRaycasts = true;
    }

    public void StartGameButtonCallback()
    {
        Debug.Log("Start GAme");
        ResetGame();
        GameStateManager.Instance.SetGameState(GameState.Play);
    }

    public void ResetCallback()
    {
        PlayerHealth.Instance.Reset();
        DataManager.Instance.Reset();
        WaveManager.Instance.Reset();
    }

    public void ResumeGameButtonCallback()
    {
        GameStateManager.Instance.SetGameState(GameState.Play);
        Debug.Log("Resume GAme");
    }
    public void QuitGameButtonCallback()
    {
        Debug.Log("quit GAme");

    }
    private void ShowMainMenu()
    {
        mainMenuCG.alpha = 1f;
        mainMenuCG.interactable = true;
        mainMenuCG.blocksRaycasts = true;
    }

    private void HideMainMenu()
    {
        mainMenuCG.alpha = 0f;
        mainMenuCG.interactable = false;
        mainMenuCG.blocksRaycasts = false;
    }

    private void ShowUpgradeMenu()
    {
        levelUpMenuCG.alpha = 1f;
        levelUpMenuCG.interactable = true;
        levelUpMenuCG.blocksRaycasts = true;
    }

    private void HideUpgradeMenu()
    {
        levelUpMenuCG.alpha = 0f;
        levelUpMenuCG.interactable = false;
        levelUpMenuCG.blocksRaycasts = false;
    }

    private void ShowPauseMenu()
    {
        pauseMenuCG.alpha = 1f;
        pauseMenuCG.interactable = true;
        pauseMenuCG.blocksRaycasts = true;
    }

    private void HidePauseMenu()
    {
        pauseMenuCG.alpha = 0f;
        pauseMenuCG.interactable = false;
        pauseMenuCG.blocksRaycasts = false;
    }

    private void StartGameplay()
    {
        Time.timeScale = 1.0f;
    }
    private void PauseGameplay()
    {

        Time.timeScale = 0f; ;
    }

    private void ResetGame()
    {
        
    }

}
