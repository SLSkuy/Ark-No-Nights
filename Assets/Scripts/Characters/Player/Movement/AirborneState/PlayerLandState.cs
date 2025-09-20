using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLandState : PlayerGroundedState
{
    public PlayerLandState(PlayerMovementFSM fsm) : base(fsm)
    {
    }

    #region IState Members

    public override void Enter()
    {
        base.Enter();

        _board.targetSpeed = 0f;
        _board.currentSpeed = 0f;
        ResetVelocity();
    }
    
    public override void OnAnimationTransitionEvent()
    {
        _board.animator.SetInteger("Land",0);
        _fsm.SwitchState(PlayerStates.Idle);
    }
    
    public override void OnAnimatorMove()
    {
        Vector3 fixedVelocity = new  Vector3(_board.player.forward.x,0,_board.player.forward.z).normalized
                                * _board.animator.velocity.magnitude;
        fixedVelocity.y = _board.rb.linearVelocity.y;
        _board.rb.linearVelocity = fixedVelocity;
    }

    #endregion
    
    #region Input Actions

    // 默认着陆状态不允许其余行为打断
    protected override void AddInputActionsCallbacks()
    {
        _board.input.PlayerActions.SwitchMoveState.started += OnSwitchWalkState;
    }

    protected override void RemoveInputActionsCallbacks()
    {
        base.RemoveInputActionsCallbacks();
        
        _board.input.PlayerActions.SwitchMoveState.started -= OnSwitchWalkState;
    }

    #endregion
}
