using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementState : IState
{
    protected FSM _fsm;
    protected BlackBoard _board;

    public PlayerMovementState(FSM fsm)
    {
        _fsm = fsm;
        _board = fsm.board;
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
        GetInputDirection();
    }

    public virtual void Update()
    {
        SpeedControl();
    }

    public virtual void FixedUpdate()
    {

    }

    public virtual void OnAnimatorMove()
    {
        BasicMove();
    }

    public virtual void OnAnimationEnterEvent()
    {
        throw new System.NotImplementedException();
    }

    public virtual void OnAnimationExitEvent()
    {
        throw new System.NotImplementedException();
    }

    public virtual void OnAnimationTransitionEvent()
    {
        
    }

    #endregion

    #region Main Methods

    /// <summary>
    /// 更新输入方向
    /// </summary>
    private void GetInputDirection()
    {
        Vector2 movement = _board.input.PlayerActions.Move.ReadValue<Vector2>();
        _board.moveDirection = new Vector3(movement.x, 0, movement.y);
    }

    /// <summary>
    /// 速度控制函数，使当前速度插值变换到目标速度
    /// </summary>
    private void SpeedControl()
    {
        float curSpeed = _board.currentSpeed;
        float targetSpeed = _board.targetSpeed;
        
        // 当前速度递增到目标速度
        curSpeed = Mathf.Abs(curSpeed - targetSpeed) > 0.01f ? 
            Mathf.Lerp(curSpeed, targetSpeed, 0.1f) : targetSpeed;
        _board.animator.SetFloat("Speed", curSpeed);
        _board.currentSpeed = curSpeed;
    }

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
        
        // 旋转模型到当前移动方向
        Quaternion targetRotation = Quaternion.LookRotation(fixedDirection);
        Quaternion newRotation = Quaternion.RotateTowards(
            _board.rb.rotation,
            targetRotation,
            _board.rotationSpeed * Time.fixedDeltaTime
        );
        // 增加收敛速度避免抽动
        if (Quaternion.Angle(_board.rb.rotation, targetRotation) < 0.5f)
            newRotation = targetRotation;
        _board.rb.MoveRotation(newRotation);
        
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
    
    /// <summary>
    /// 添加回调函数，用于检测输入事件触发
    /// </summary>
    protected virtual void AddInputActionsCallbacks()
    {
        _board.input.PlayerActions.SwitchMoveState.started += OnSwitchWalkState;
    }
    
    /// <summary>
    /// 删除回调函数，删除非当前状态的输入事件检测
    /// </summary>
    protected virtual void RemoveInputActionsCallbacks()
    {
        _board.input.PlayerActions.SwitchMoveState.started -= OnSwitchWalkState;
    }
    
    #endregion
}
