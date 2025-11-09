using System;
using UnityCommunity.UnitySingleton;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : PersistentMonoSingleton<InputController>
{
    public float rotationInput { get; private set; }
    public bool pulsePressed { get; private set; }
    public bool spinAttackPressed { get; private set; }
    public bool powerUpPressed { get; private set; }


    [Header("***Events***")]
    public static Action<bool> SlowDownActivatedAction;

    private PlayerInput playerInput;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
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
                playerInput.SwitchCurrentActionMap("UI");
                break;

            case GameState.Play:
                playerInput.SwitchCurrentActionMap("Player");
                break;

            case GameState.Pause:
                playerInput.SwitchCurrentActionMap("UI");
                break;

            case GameState.LevelUp:
                playerInput.SwitchCurrentActionMap("UI");
                break;

            default:
                Debug.LogWarning($"Unhandled GameState: {state}");
                break;
        }

        Debug.Log($"Action Map is: {playerInput.currentActionMap.name}");
    }


    public void OnRotate(InputAction.CallbackContext ctx)
    {
        rotationInput = ctx.ReadValue<float>();
    }

    public void OnPulse(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            pulsePressed = true;
        if (ctx.canceled)
            pulsePressed = false;
    }

    public void OnSpinAttack(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            spinAttackPressed = true;
        if (ctx.canceled)
            spinAttackPressed = false;
    }

    public void OnUsePowerUp(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            powerUpPressed = true;
        if (ctx.canceled)
            powerUpPressed = false;
    }
    public void OnPause(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (GameStateManager.Instance.CurrentGameState == GameState.Play)
            {
                GameStateManager.Instance.SetGameState(GameState.Pause);
            }
            else
            {
                GameStateManager.Instance.SetGameState(GameState.Play);
            }
        }
    }
    public void OnChangeToRightButton(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            Debug.Log("Changed to right gun");
        }
    }
    public void OnChangeToLeftButton(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            Debug.Log("Changed to left gun");
        }
    }
}
