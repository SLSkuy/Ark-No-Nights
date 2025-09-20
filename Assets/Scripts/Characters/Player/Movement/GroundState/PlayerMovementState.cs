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
        CheckIsFalling();
    }

    public virtual void FixedUpdate()
    {
        BasicRotate();
    }

    public virtual void OnAnimatorMove()
    {
        BasicMove();
    }

    public virtual void OnAnimationEnterEvent()
    {

    }

    public virtual void OnAnimationExitEvent()
    {

    }

    public virtual void OnAnimationTransitionEvent()
    {
        
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        OnContactWithGround(other);
    }

    public virtual void OnTriggerExit(Collider other)
    {
        
    }

    #endregion

    #region Main Methods
    
    /// <summary>
    /// 检测是否开始坠落
    /// </summary>
    private void CheckIsFalling()
    {
        if (!_board.fallingTriggered && _board.rb.linearVelocity.y < -_board.fallingThreshold)
        {
            _fsm.SwitchState(PlayerStates.Falling);
            _board.animator.SetBool("Ground",false);
            _board.fallingTriggered = true;
        }
    }
    
    /// <summary>
    /// 与地面接触时调用
    /// </summary>
    /// <param name="other"></param>
    protected virtual void OnContactWithGround(Collider other)
    {
        // 触地
    }

    /// <summary>
    /// 更新输入方向
    /// </summary>
    private void GetInputDirection()
    {
        Vector2 movement = _board.input.PlayerActions.Move.ReadValue<Vector2>();
        _board.moveDirection = new Vector3(movement.x, 0, movement.y);
    }

    /// <summary>
    /// 根据输入方向和角色朝向返回移动朝向
    /// </summary>
    /// <returns></returns>
    public Vector3 GetFixedDirection()
    {
        Vector3 fixedDirection = _board.moveDirection.x * _board.orientation.right + _board.moveDirection.z * _board.orientation.forward;
        fixedDirection.Normalize();
        return fixedDirection;
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
    /// 人物朝向旋转到当前移动朝向
    /// </summary>
    private void BasicRotate()
    {
        if (_board.moveDirection == Vector3.zero)
        {
            return;
        }
        
        Vector3 fixedDirection = GetFixedDirection();
        
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
    }
    
    /// <summary>
    /// 人物基础移动功能
    /// 以Animator中的动画偏移作为移动位移
    /// </summary>
    private void BasicMove()
    {
        if (_board.moveDirection == Vector3.zero || _board.targetSpeed == 0)
        {
            _board.currentSpeed = 0f;
            return;
        }
        
        // _board.player.transform.position += _board.animator.deltaPosition;
        Vector3 fixedVelocity = new  Vector3(_board.player.forward.x,0,_board.player.forward.z).normalized
            * _board.animator.velocity.magnitude;
        fixedVelocity.y = _board.rb.linearVelocity.y;
        _board.rb.linearVelocity = fixedVelocity;
    }

    /// <summary>
    /// 重置人物当前速度
    /// </summary>
    protected void ResetVelocity()
    {
        _board.rb.linearVelocity = Vector3.zero;
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
