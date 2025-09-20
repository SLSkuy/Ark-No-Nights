using UnityEngine;

public class PlayerHardStopState : PlayerStopState
{
    public PlayerHardStopState(PlayerMovementFSM fsm) : base(fsm)
    {
    }

    public override void Enter()
    {
        base.Enter();

        _board.animator.SetInteger("Stop", 3);
    }
}
