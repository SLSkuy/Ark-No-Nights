using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSprintState : PlayerGroundedState
{
    private bool _keepSprinting;
    private float _startTime;
    private readonly float _sprintToRunTime = 0.45f;
    
    public PlayerSprintState(PlayerMovementFSM fsm) : base(fsm)
    {
    }

    #region IState Members

    public override void Enter()
    {
        base.Enter();
        
        _board.targetSpeed = _board.sprintSpeed;
        _startTime = Time.time;
    }

    public override void Exit()
    {
        base.Exit();

        _keepSprinting = false;
    }

    public override void Update()
    {
        base.Update();

        if (_keepSprinting) return;

        if (Time.time - _startTime < _sprintToRunTime) return;

        StopSprinting();
    }

    #endregion

    #region Main Methods
    
    private void StopSprinting()
    {
        if (_board.moveDirection == Vector3.zero)
        {
            _fsm.SwitchState(PlayerStates.Idle);
        }
        else
        {
            _fsm.SwitchState(PlayerStates.Running);
        }
    }
    
    private void OnSprintPerformed(InputAction.CallbackContext obj)
    {
        _keepSprinting = true;
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

        _board.input.PlayerActions.Sprint.performed += OnSprintPerformed;
        _board.input.PlayerActions.Sprint.canceled += OnSprintCancelled;
    }
    
    protected override void RemoveInputActionsCallbacks()
    {
        base.RemoveInputActionsCallbacks();
        
        _board.input.PlayerActions.Sprint.performed -= OnSprintPerformed;
        _board.input.PlayerActions.Sprint.canceled -= OnSprintCancelled;
    }
    
    #endregion
}
