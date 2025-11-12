using System;
using UnityCommunity.UnitySingleton;
using UnityEngine;

public enum GameState
{
    MainMenu,
    Play,
    Pause,
    LevelUp,
    GameOver
}

public class GameStateManager : PersistentMonoSingleton<GameStateManager>
{
    public static Action<GameState> OnGameStateChanged;
    private GameState _currentGameState;

    public GameState CurrentGameState => _currentGameState;

    private void Start()
    {
        SetGameState(GameState.MainMenu);
    }
    public void SetGameState(GameState state)
    {
        _currentGameState = state;
        CheckPauseCondition();
        OnGameStateChanged?.Invoke(state);
    }

    public void ContinueToGameCallback()
    {
        SetGameState(GameState.Play);
    }

    private void CheckPauseCondition()
    {
        if(CurrentGameState == GameState.MainMenu || CurrentGameState == GameState.Pause || CurrentGameState == GameState.GameOver || CurrentGameState == GameState.LevelUp)
        {
            Time.timeScale = 0;
        }
        else
        {
            Debug.Log("*****************GAME STATE CHANGED TO PLAY!!!");
            Time.timeScale = 1;
        }
    }

    void OnApplicationQuit()
    {
        System.GC.Collect();
        Resources.UnloadUnusedAssets();
    }

}
