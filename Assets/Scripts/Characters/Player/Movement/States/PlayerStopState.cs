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
    }

    public override void OnAnimationTransitionEvent()
    {
        _fsm.SwitchState(PlayerStates.Idle);
    }

    protected override void OnMovementCanceled(InputAction.CallbackContext context)
    {
        // 防止当前状态被打断
    }

    #endregion
    

}
