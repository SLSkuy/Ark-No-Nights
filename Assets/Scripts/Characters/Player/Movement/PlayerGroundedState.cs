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
    /// <param name="context"> InputSystem回调参数</param>
    protected void OnMovementCanceled(InputAction.CallbackContext context)
    {
        _fsm.SwitchState(PlayerStates.Idle);
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
