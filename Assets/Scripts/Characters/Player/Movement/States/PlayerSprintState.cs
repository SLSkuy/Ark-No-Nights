using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSprintState : PlayerGroundedState
{
    private bool _keepSprinting;
    
    public PlayerSprintState(PlayerMovementFSM fsm) : base(fsm)
    {
    }

    #region IState Members

    public override void Enter()
    {
        base.Enter();
        
        _board.targetSpeed = _board.sprintSpeed;
        _keepSprinting = true;
    }

    public override void Update()
    {
        base.Update();

        if (_keepSprinting) return;
        
        StopSprinting();
    }

    #endregion

    #region Main Methods
    
    private void StopSprinting()
    {
        if (_board.moveDirection == Vector3.zero)
        {
            _fsm.SwitchState(PlayerStates.HardStop);
        }
        else
        {
            _fsm.SwitchState(PlayerStates.Running);
        }
    }

    protected override void OnMovementCanceled(InputAction.CallbackContext context)
    {
        _keepSprinting = false;
    }

    private void OnSprintCancelled(InputAction.CallbackContext obj)
    {
        _keepSprinting = false;
    }

    #endregion
    
    #region Input Actions
    
    protected override void AddInputActionsCallbacks()
    {
        base.AddInputActionsCallbacks();
        
        _board.input.PlayerActions.Sprint.canceled += OnSprintCancelled;
    }
    
    protected override void RemoveInputActionsCallbacks()
    {
        base.RemoveInputActionsCallbacks();
        
        _board.input.PlayerActions.Sprint.canceled -= OnSprintCancelled;
    }
    
    #endregion
}
