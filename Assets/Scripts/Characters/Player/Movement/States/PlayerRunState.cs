using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRunState : PlayerGroundedState
{
    public PlayerRunState(PlayerMovementFSM fsm) : base(fsm)
    {
    }
    
    #region IState Members

    public override void Enter()
    {
        base.Enter();
        
        _board.shouldWalk = false;
        _board.targetSpeed = _board.runSpeed;
    }
    
    #endregion
    
    #region Main Methods
    
    protected override void OnSwitchWalkState(InputAction.CallbackContext context)
    {
        base.OnSwitchWalkState(context);
        
        _fsm.SwitchState(PlayerStates.Walking);
    }
    
    #endregion
}
