using UnityEngine;

public class PlayerAirborneState : PlayerMovementState
{
    public PlayerAirborneState(FSM fsm) : base(fsm)
    {
    }

    #region IState Members

    public override void Enter()
    {
        base.Enter();

        // 降低空中的旋转速度
        _board.rotationSpeed = 90f;
    }

    public override void OnAnimatorMove()
    {
        // 防止覆盖原有的速度
    }

    #endregion
    
    #region Main Methods

    protected override void OnContactWithGround(Collider other)
    {
        _board.animator.SetBool("Ground",true);
        _board.fallingTriggered = false;
        
        if (_board.rb.linearVelocity.y < -_board.rollThreshold)
        {
            _fsm.SwitchState(PlayerStates.HardLand);
            return;
        }
        _fsm.SwitchState(PlayerStates.LightLand);
    }
    
    #endregion
}
