using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputDeviceAutoSwitcher : MonoBehaviour
{
    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerInput.onControlsChanged += OnControlsChanged;
    }

    private void OnDestroy()
    {
        playerInput.onControlsChanged -= OnControlsChanged;
    }

    private void OnControlsChanged(PlayerInput input)
    {

        if (Keyboard.current != null && Keyboard.current.anyKey.wasPressedThisFrame)
        {
            input.SwitchCurrentControlScheme("Keyboard&Mouse");
            Debug.Log("Switched to Keyboard&Mouse");
        }
        else if (Gamepad.current != null && Gamepad.current.wasUpdatedThisFrame)
        {
            input.SwitchCurrentControlScheme("Gamepad");
            Debug.Log("Switched to Gamepad");
        }
    }
}
