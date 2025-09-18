using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGroundedState : PlayerMovementState
{
    public PlayerGroundedState(PlayerMovementFSM fsm) : base(fsm)
    {
    }
    
    #region Main Methods
    
    /// <summary>
    /// 移动按键释放后执行操作
    /// </summary>
    /// <param name="context">InputSystem回调参数</param>
    protected virtual void OnMovementCanceled(InputAction.CallbackContext context)
    {
        _fsm.SwitchState(PlayerStates.Idle);
    }
    
    #endregion
    
    /// <summary>
    /// 冲刺状态转换
    /// </summary>
    /// <param name="obj">InputSystem回调参数</param>
    private void OnDashStarted(InputAction.CallbackContext obj)
    {
        _fsm.SwitchState(PlayerStates.Dashing);
    }

    
    #region Input Actions
    
    protected override void AddInputActionsCallbacks()
    {
        base.AddInputActionsCallbacks();
        
        _board.input.PlayerActions.Move.canceled += OnMovementCanceled;
        _board.input.PlayerActions.Dash.started += OnDashStarted;
    }
    
    protected override void RemoveInputActionsCallbacks()
    {
        base.RemoveInputActionsCallbacks();
        
        _board.input.PlayerActions.Move.canceled -= OnMovementCanceled;
        _board.input.PlayerActions.Dash.started -= OnDashStarted;
    }
    
    #endregion
}
