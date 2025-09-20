using UnityEngine;

public class PlayerHardLandState : PlayerLandState
{
    public PlayerHardLandState(PlayerMovementFSM fsm) : base(fsm)
    {
    }
    
    #region IState Members

    public override void Enter()
    {
        base.Enter();
        
        _board.animator.SetInteger("Land",2);
    }
    
    #endregion
    
}
