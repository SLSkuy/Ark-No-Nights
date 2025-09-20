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

        StartJump();
    }
    
    #endregion
    
    #region Main Methods
    
    private void StartJump()
    {
        _board.rb.AddForce(_board.player.up * 5, ForceMode.Impulse);
    }
    
    #endregion
}
