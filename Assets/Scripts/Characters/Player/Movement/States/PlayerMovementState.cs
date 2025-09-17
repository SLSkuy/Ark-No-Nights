using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementState : IState
{
    protected PlayerControl _player;
    protected BlackBoard _board;

    public PlayerMovementState(PlayerControl player)
    {
        _player = player;
        _board = player.board;
    }
    
    #region IState Members
    
    public virtual void Enter()
    {
        Debug.Log(this.GetType().Name + " entered");

        AddInputActionsCallbacks();
    }

    public virtual void Exit()
    {
        RemoveInputActionsCallbacks();
    }

    public virtual void HandleInput()
    {
        // 获取输入方向
        Vector2 movement = _board.input.PlayerActions.Move.ReadValue<Vector2>();
        _board.moveDirection = new Vector3(movement.x, 0, movement.y);
    }

    public virtual void Update()
    {
        float curSpeed = _board.currentSpeed;
        float targetSpeed = _board.targetSpeed;
        // 当前速度递增到目标速度
        curSpeed = Mathf.Abs(curSpeed - targetSpeed) > 0.01f ? 
            Mathf.Lerp(curSpeed, targetSpeed, 0.1f) : targetSpeed;
        _board.animator.SetFloat("Speed", curSpeed);
        _board.currentSpeed = curSpeed;
    }

    public virtual void FixedUpdate()
    {

    }

    public virtual void OnAnimatorMove()
    {
        BasicMove();
    }
    
    #endregion

    #region Main Methods

    /// <summary>
    /// 人物基础移动功能
    /// 以Animator中的动画速度作为移动速度
    /// </summary>
    private void BasicMove()
    {
        if (_board.moveDirection == Vector3.zero || _board.targetSpeed == 0)
        {
            _board.currentSpeed = 0f;
            return;
        }
        
        // 根据输入与角色朝向计算移动方向
        Vector3 fixedDirection = _board.moveDirection.x * _board.orientation.right + _board.moveDirection.z * _board.orientation.forward;
        fixedDirection.Normalize();
        
        // 旋转模型朝向
        _player.transform.forward = Vector3.Slerp(_player.transform.forward, fixedDirection, Time.deltaTime * _board.rotationSpeed);

        // 添加速度
        Vector3 targetVelocity = fixedDirection * _board.animator.velocity.magnitude;
        Vector3 fixedVelocity = new Vector3(targetVelocity.x, _board.rb.linearVelocity.y, targetVelocity.z);
        _board.rb.linearVelocity = fixedVelocity;
    }

    /// <summary>
    /// 重置人物当前除Y轴方向的速度
    /// </summary>
    protected void ResetVelocity()
    {
        _board.rb.linearVelocity = new Vector3(0f, _board.rb.linearVelocity.y, 0f);
    }

    /// <summary>
    /// 切换人物以行走还是慢跑移动
    /// </summary>
    /// <param name="context"> InputSystem回调参数 </param>
    protected virtual void OnSwitchWalkState(InputAction.CallbackContext context)
    {
        _board.shouldWalk = !_board.shouldWalk;
    }

    #endregion
    
    #region Input Actions
    
    protected virtual void AddInputActionsCallbacks()
    {
        _board.input.PlayerActions.SwitchMoveState.started += OnSwitchWalkState;
    }
    
    protected virtual void RemoveInputActionsCallbacks()
    {
        _board.input.PlayerActions.SwitchMoveState.started -= OnSwitchWalkState;
    }
    
    #endregion
}
