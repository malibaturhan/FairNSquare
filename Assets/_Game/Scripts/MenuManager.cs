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
                HidePauseMenu();
                HideUpgradeMenu();
                ShowMainMenu();
                break;

            case GameState.Play:
                HidePauseMenu();
                HideUpgradeMenu();
                HideMainMenu();
                break;

            case GameState.Pause:
                HideMainMenu();
                HideUpgradeMenu();
                ShowPauseMenu();
                break;

            case GameState.LevelUp:
                HidePauseMenu();
                HideMainMenu();
                ShowUpgradeMenu();
                break;

            case GameState.GameOver:
                HidePauseMenu();
                HideMainMenu();
                HideUpgradeMenu();
                break;

            default:
                Debug.LogWarning($"Unhandled GameState: {state}");
                break;
        }

    }

    private void ShowMenu(CanvasGroup cgToShow)
    {
        foreach(var cg in allMenuCanvasGroupes)
        {
            cg.alpha = 0f;
            cg.interactable = false;
            cg.blocksRaycasts = false;
        }
        cgToShow.alpha = 0f;
        cgToShow.interactable = false;
        cgToShow.blocksRaycasts = false;
    }

    public void StartGameButtonCallback()
    {
        Debug.Log("Start GAme");
        GameStateManager.Instance.SetGameState(GameState.Play);
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
}
