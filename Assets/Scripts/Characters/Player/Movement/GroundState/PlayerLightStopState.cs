using UnityEngine;

public class PlayerLightStopState : PlayerStopState
{
    public PlayerLightStopState(PlayerMovementFSM fsm) : base(fsm)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        _board.animator.SetInteger("Stop",1);
    }
}
