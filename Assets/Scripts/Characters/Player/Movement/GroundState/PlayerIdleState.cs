using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(PlayerMovementFSM fsm) : base(fsm)
    {
    }

    #region IState Members
    
    public override void Enter()
    {
        base.Enter();
        
        _board.targetSpeed = 0f;
        _board.currentSpeed = 0f;
        _board.rotationSpeed = 360f;
        
        ResetVelocity();
    }

    public override void Update()
    {
        if (_board.moveDirection == Vector3.zero)
        {
            return;
        }

        OnMove();
    }
    
    #endregion

    #region Main Methods

    private void OnMove()
    {
        if (_board.shouldWalk)
        {
            _fsm.SwitchState(PlayerStates.Walking);
            return;
        }
        _fsm.SwitchState(PlayerStates.Running);
    }

    #endregion
}
