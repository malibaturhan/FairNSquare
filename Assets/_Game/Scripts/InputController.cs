using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    public float rotationInput { get; private set; }
    public bool pulsePressed { get; private set; }
    public bool spinAttackPressed { get; private set; }
    public bool powerUpPressed { get; private set; }

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
            Debug.Log("Pause triggered!");
            // Burada oyunu durdurma, menüyü açma vs. iþlemleri yapýlacak.
        }
    }
}
