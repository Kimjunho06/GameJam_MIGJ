using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "SO/InputReader")]
public class InputReader : ScriptableObject, Controls.IPlayerActions
{
    public Vector2 MousePos;
    public Vector2 InputPos;

    public event Action JumpEvent;

    private Controls _controls;
    private void OnEnable()
    {
        if (_controls == null)
        {
            _controls = new Controls();
            _controls.Player.SetCallbacks(this);
        }
        _controls.Player.Enable();
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        MousePos = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
            JumpEvent?.Invoke();
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        InputPos = context.ReadValue<Vector2>();
    }
}
