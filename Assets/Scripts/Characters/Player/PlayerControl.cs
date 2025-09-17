using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    // 组件获取
    public BlackBoard board;
    
    // 有限状态机
    private PlayerMovementFSM _movementFSM;

    private void Awake()
    {
        board.rb = GetComponent<Rigidbody>();
        board.animator = GetComponent<Animator>();
        
        _movementFSM = new PlayerMovementFSM(this);
        board.camera = Camera.main.transform;
    }

    private void Start()
    {
        _movementFSM.ChangeState(_movementFSM.IdleState);
    }

    private void Update()
    {
        // 测试使用
        board.animator.SetFloat("Speed", board.currentSpeed);
        board.orientation.transform.eulerAngles = new Vector3(0,board.camera.transform.eulerAngles.y,0);
        
        _movementFSM.Update();
    }

    private void FixedUpdate()
    {
        _movementFSM.FixedUpdate();
    }

    private void OnAnimatorMove()
    {
        _movementFSM.OnAnimatorMove();
    }

    #region MainMethods

    void OnMove(InputValue value)
    {
        Vector2 movement = value.Get<Vector2>();
        board.moveDirection = new Vector3(movement.x, 0, movement.y);
    }

    #endregion
}
