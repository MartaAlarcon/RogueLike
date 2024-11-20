using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerInputManager : MonoBehaviour, InputController.IPlayerActions
{
    public InputController inputs;
    public Vector2 direction;
    public Vector2 speed;

    private void Awake()
    {
        inputs = new InputController();
        inputs.Player.SetCallbacks(this);
    }

    private void OnEnable()
    {
        inputs.Enable();
    }

    private void OnDisable()
    {
        inputs.Disable();
    }
    public void OnMovement(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            direction = context.ReadValue<Vector2>();
            Debug.Log("Direction: " + direction);
        }
        else if (context.canceled)
        {
            direction = Vector2.zero;
        }
    }
}
