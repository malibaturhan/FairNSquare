using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(InputController))]
public class PlayerController : MonoBehaviour
{
    [Header("***Settings***")]
    [SerializeField] private float rotationSpeed = 180f; // derece/saniye

    private InputController inputController;

    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        inputController = GetComponent<InputController>();
    }



    private void Update()
    {
        RotatePlayer();
    }

    private void RotatePlayer()
    {
        float input = inputController.rotationInput;

        // Ölü bölge kontrolü (küçük stick salýnýmlarýný yok saymak için)
        if (Mathf.Abs(input) > 0.05f)
        {
            // 2D’de Z ekseni etrafýnda, 3D’de Y ekseni etrafýnda dönersin
            transform.Rotate(Vector3.forward, -input * rotationSpeed * Time.deltaTime);
        }
    }
}
