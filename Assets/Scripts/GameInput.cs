using UnityEngine;
using System;

public class GameInput : MonoBehaviour
{

    private PlayerInputAction playerInputAction;
    public event EventHandler OnInteractionAction;
    public event EventHandler OnInteractionAlternateAction;

    private void Awake()
    {
        playerInputAction = new PlayerInputAction();
        playerInputAction.Player.Enable();
        playerInputAction.Player.Interact.performed += Interact_performed;
        playerInputAction.Player.InteractAlternate.performed += InteractAlternate_performed;
    }

    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractionAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractionAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputAction.Player.Move.ReadValue<Vector2>();

            inputVector = inputVector.normalized;
            return inputVector;
    }
}

