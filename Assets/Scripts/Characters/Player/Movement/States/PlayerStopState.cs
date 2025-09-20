using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStopState : PlayerGroundedState
{
    public PlayerStopState(PlayerMovementFSM fsm) : base(fsm)
    {
    }

    #region IState Members

    public override void Enter()
    {
        base.Enter();

        _board.targetSpeed = 0f;
        _board.currentSpeed = 0f;
    }

    public override void OnAnimationTransitionEvent()
    {
        _board.animator.SetInteger("Stop",0);
        _fsm.SwitchState(PlayerStates.Idle);
    }

    public override void OnAnimatorMove()
    {
        Vector3 fixedVelocity = new  Vector3(_board.player.forward.x,0,_board.player.forward.z).normalized
                                * _board.animator.velocity.magnitude;
        fixedVelocity.y = _board.rb.linearVelocity.y;
        _board.rb.linearVelocity = fixedVelocity;
    }
    
    /// <summary>
    /// 打断停止状态，进入移动状态
    /// </summary>
    /// <param name="obj"></param>
    private void OnMovementStarted(InputAction.CallbackContext obj)
    {
        if (_board.shouldWalk)
        {
            _fsm.SwitchState(PlayerStates.Walking);
        }
        else
        {
            _fsm.SwitchState(PlayerStates.Running);
        }
    }
    
    protected override void OnMovementCanceled(InputAction.CallbackContext context)
    {
        // 防止当前状态被打断，而不是进入默认的待机状态
    }

    #endregion
    
    #region Input Actions

    protected override void AddInputActionsCallbacks()
    {
        base.AddInputActionsCallbacks();

        _board.input.PlayerActions.Move.started += OnMovementStarted;
    }

    protected override void RemoveInputActionsCallbacks()
    {
        base.RemoveInputActionsCallbacks();
        
        _board.input.PlayerActions.Move.started -= OnMovementStarted;
    }

    #endregion
}
