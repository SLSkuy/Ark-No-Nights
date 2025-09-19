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
        _board.player.transform.position += _board.animator.deltaPosition;
    }
    
    protected override void OnMovementCanceled(InputAction.CallbackContext context)
    {
        // 防止当前状态被打断
    }

    #endregion
}
