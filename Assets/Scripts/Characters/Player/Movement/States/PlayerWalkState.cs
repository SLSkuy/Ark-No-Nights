using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWalkState : PlayerGroundedState
{
    public PlayerWalkState(PlayerMovementFSM fsm) : base(fsm)
    {
    }

    #region IState Members

    public override void Enter()
    {
        base.Enter();

        _board.targetSpeed = _board.walkSpeed;
    }

    #endregion
    
    #region Main Methods
    
    protected override void OnSwitchWalkState(InputAction.CallbackContext context)
    {
        base.OnSwitchWalkState(context);
        
        _fsm.SwitchState(PlayerStates.Running);
    }
    
    #endregion
}
