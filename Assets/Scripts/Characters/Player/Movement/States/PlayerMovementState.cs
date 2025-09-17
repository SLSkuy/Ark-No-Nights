using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementState : IState
{
    private PlayerMovementFSM _movementFSM;

    public PlayerMovementState(PlayerMovementFSM movementFSM)
    {
        _movementFSM = movementFSM;
    }
    
    public void Enter()
    {
        Debug.Log(this.GetType().Name + " entered");
    }

    public void Exit()
    {

    }

    public void HandleInput()
    {

    }

    public void Update()
    {

    }

    public void FixedUpdate()
    {

    }
}
