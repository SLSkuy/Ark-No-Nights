using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRunState : PlayerMovementState
{
    public PlayerRunState(PlayerControl fsm) : base(fsm)
    {
    }
    
    #region IState Members

    public override void Enter()
    {
        base.Enter();
        
        _board.targetSpeed = _board.runSpeed;
    }
    
    #endregion
    
    #region Main Methods
    
    protected override void OnSwitchWalkState(InputAction.CallbackContext context)
    {
        base.OnSwitchWalkState(context);
        
        _player.movementFSM.ChangeState(_player.movementFSM.WalkState);
    }

    protected void OnMovementCanceled(InputAction.CallbackContext context)
    {
        _player.movementFSM.ChangeState(_player.movementFSM.IdleState);
    }
    
    #endregion

    #region Input Actions

    protected override void AddInputActionsCallbacks()
    {
        base.AddInputActionsCallbacks();

        _board.input.PlayerActions.Move.canceled += OnMovementCanceled;
    }
    
    protected override void RemoveInputActionsCallbacks()
    {
        base.RemoveInputActionsCallbacks();
        
        _board.input.PlayerActions.Move.canceled -= OnMovementCanceled;
    }

    #endregion
}
