using UnityEngine;

public class PlayerFallingState : PlayerAirborneState
{
    public PlayerFallingState(FSM fsm) : base(fsm)
    {
    }

    #region IState Members

    public override void Enter()
    {
        base.Enter();
        
        _board.animator.SetBool("Ground", false);
    }

    #endregion
}
