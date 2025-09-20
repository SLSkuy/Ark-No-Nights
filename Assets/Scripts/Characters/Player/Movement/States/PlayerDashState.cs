using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDashState : PlayerGroundedState
{
    private bool _transToSprint;
    
    public PlayerDashState(PlayerMovementFSM fsm) : base(fsm)
    {
    }

    #region IState Members
    
    public override void Enter()
    {
        base.Enter();

        _transToSprint = true;
        StartDash();
    }

    public override void OnAnimatorMove()
    {
        DashMovement();
    }

    public override void OnAnimationTransitionEvent()
    {
        if (_board.moveDirection == Vector3.zero )
        {
            _fsm.SwitchState(PlayerStates.HardStop);
            return;
        }

        if (_transToSprint)
        {
            _fsm.SwitchState(PlayerStates.Sprinting);
            return;
        }
        
        _fsm.SwitchState(PlayerStates.Running);
    }

    #endregion

    #region Main Methods
    
    private void DashMovement()
    {
        Vector3 fixedVelocity = new  Vector3(_board.player.forward.x,0,_board.player.forward.z).normalized
                                * _board.animator.velocity.magnitude;
        fixedVelocity.y = _board.rb.linearVelocity.y;
        _board.rb.linearVelocity = fixedVelocity;
    }

    protected override void OnMovementCanceled(InputAction.CallbackContext context)
    {
        // 保持当前状态不受影响，进入停止状态
    }
    
    private void StartDash()
    {
        if (_board.moveDirection == Vector3.zero)
        {
            Vector3 characterRotationDir = _board.orientation.forward;
            characterRotationDir.y = 0;
            _board.player.forward = characterRotationDir;
        }
        
        _board.animator.SetTrigger("Dash");
    }

    private void OnDashCanceled(InputAction.CallbackContext context)
    {
        _transToSprint = false;
    }
    
    #endregion
    
    #region Input Actions
    
    protected override void AddInputActionsCallbacks()
    {
        base.AddInputActionsCallbacks();
        
        _board.input.PlayerActions.Sprint.canceled += OnDashCanceled;
    }
    
    protected override void RemoveInputActionsCallbacks()
    {
        base.RemoveInputActionsCallbacks();
        
        _board.input.PlayerActions.Sprint.canceled -= OnDashCanceled;
    }
    
    #endregion
}
