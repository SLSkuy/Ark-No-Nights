using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLightLandState : PlayerLandState
{
    public PlayerLightLandState(PlayerMovementFSM fsm) : base(fsm)
    {
    }

    #region IState Members

    public override void Enter()
    {
        base.Enter();

        _board.animator.SetInteger("Land", 1);
    }

    #endregion
}
