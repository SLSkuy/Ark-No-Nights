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
    
    public void Enter()
    {
        Debug.Log(this.GetType().Name + " entered");
    }

    public void Exit()
    {

    }

    public void HandleInput()
    {

    }

    public void Update()
    {

    }

    public void FixedUpdate()
    {

    }

    public void OnAnimatorMove()
    {
        Move();
    }

    #region MainMethods

    void Move()
    {
        if (_board.moveDirection == Vector3.zero || !_board.canMove)
        {
            _board.currentSpeed = 0f;
            _board.rb.linearVelocity = Vector3.zero;
            return;
        }

        _board.currentSpeed = 3f;
        
        // 根据输入与角色朝向计算移动方向
        Vector3 fixedDirection = _board.moveDirection.x * _board.orientation.right + _board.moveDirection.z * _board.orientation.forward;
        fixedDirection.Normalize();
        
        // 旋转模型朝向
        _player.transform.forward = Vector3.Slerp(_player.transform.forward, fixedDirection, Time.deltaTime * _board.rotationSpeed);
        
        // 添加速度
        Vector3 targetVelocity = fixedDirection * _board.animator.velocity.magnitude;
        Vector3 fixedVelocity = new  Vector3(targetVelocity.x, _board.rb.linearVelocity.y, targetVelocity.z);
        _board.rb.linearVelocity = fixedVelocity;
    }

    #endregion
}
