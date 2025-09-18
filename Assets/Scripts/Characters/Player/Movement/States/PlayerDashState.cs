using UnityEngine;

public class PlayerDashState : PlayerGroundedState
{
    public PlayerDashState(PlayerMovementFSM fsm) : base(fsm)
    {
    }

    #region IState Members
    
    public override void Enter()
    {
        base.Enter();

        StartDash();
    }

    public override void OnAnimationTransitionEvent()
    {
        base.OnAnimationTransitionEvent();
        
        if (_board.moveDirection == Vector3.zero)
        {
            _fsm.SwitchState(PlayerStates.Idle);
            return;
        }
   
        _fsm.SwitchState(PlayerStates.Sprinting);
    }
    
    #endregion

    #region Main Methods

    private void StartDash()
    {
        if (_board.moveDirection == Vector3.zero)
        {
            Vector3 characterRotationDir = _board.orientation.forward;
            characterRotationDir.y = 0;
            _board.player.forward = characterRotationDir;
        }
        
        // _board.targetSpeed = _board.dashSpeed;
        // _board.currentSpeed = _board.dashSpeed;
        
        // 测试使用
        OnAnimationTransitionEvent();
    }
    
    #endregion
}
