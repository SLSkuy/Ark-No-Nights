using UnityEngine;

public class PlayerIdleState : PlayerMovementState
{
    public PlayerIdleState(PlayerControl player) : base(player)
    {
    }

    #region IState Members
    
    public override void Enter()
    {
        base.Enter();
        _board.targetSpeed = 0f;
        
        ResetVelocity();
    }

    public override void Update()
    {
        base.Update();

        if (_board.moveDirection == Vector3.zero)
        {
            return;
        }

        OnMove();
    }
    
    #endregion

    #region Main Methods

    private void OnMove()
    {
        PlayerMovementFSM fsm = _player.movementFSM;
        if (_board.shouldWalk)
        {
            fsm.ChangeState(fsm.WalkState);
            return;
        }
        fsm.ChangeState(fsm.RunState);
    }

    #endregion
}
