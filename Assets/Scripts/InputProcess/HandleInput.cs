using System;
using UnityEngine;

public class HandleInput : MonoBehaviour
{
    public InputSystem_Actions InputActions { get; private set; }
    public InputSystem_Actions.PlayerActions PlayerActions { get; private set; }

    void Awake()
    {
        InputActions = new InputSystem_Actions();
        PlayerActions = InputActions.Player;
    }

    private void OnEnable()
    {
        InputActions.Enable();
    }

    private void OnDisable()
    {
        InputActions.Disable();
    }
}
