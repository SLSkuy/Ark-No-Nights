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

        _board.targetSpeed = 0f;
        _board.animator.SetTrigger("Jump");
    }

    public override void Update()
    {
        _board.rb.AddForce(_board.player.up * 5, ForceMode.Impulse);
        _fsm.SwitchState(PlayerStates.Running);
    }

    #endregion
}
