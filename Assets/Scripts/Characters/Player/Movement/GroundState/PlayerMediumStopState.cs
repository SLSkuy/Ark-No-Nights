using UnityEngine;

public class PlayerMediumStopState : PlayerStopState
{
    public PlayerMediumStopState(PlayerMovementFSM fsm) : base(fsm)
    {
    }

    public override void Enter()
    {
        base.Enter();

        _board.animator.SetInteger("Stop", 2);
    }
}
