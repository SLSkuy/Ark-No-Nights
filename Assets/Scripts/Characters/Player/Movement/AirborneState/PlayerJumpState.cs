using UnityEngine;

public class PlayerJumpState : PlayerAirborneState
{
    public PlayerJumpState(FSM fsm) : base(fsm)
    {
    }
    
    #region IState Members

    public override void Enter()
    {
        base.Enter();
        
        _board.animator.SetTrigger("Jump");
        _board.animator.SetBool("Ground",false);

        StartJump();
    }

    public override void OnAnimationTransitionEvent()
    {
        // 跳跃动画完成后进入坠落状态
        _fsm.SwitchState(PlayerStates.Falling);
        _board.animator.SetTrigger("Falling");
    }
    
    #endregion
    
    #region Main Methods
    
    private void StartJump()
    {
        _board.rb.AddForce(_board.player.up * 5, ForceMode.Impulse);
    }
    
    #endregion
}
