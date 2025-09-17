using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(HandleInput))]
public class PlayerControl : MonoBehaviour
{
    // 组件获取
    public BlackBoard board;
    
    // 有限状态机
    public PlayerMovementFSM movementFSM;

    private void Awake()
    {
        board.rb = GetComponent<Rigidbody>();
        board.animator = GetComponent<Animator>();
        board.input = GetComponent<HandleInput>();
        
        movementFSM = new PlayerMovementFSM(this);
        board.camera = Camera.main.transform;
    }

    private void Start()
    {
        movementFSM.ChangeState(movementFSM.IdleState);
    }

    private void Update()
    {
        // 测试使用
        board.orientation.transform.eulerAngles = new Vector3(0,board.camera.transform.eulerAngles.y,0);
        
        movementFSM.HandleInput();
        movementFSM.Update();
    }

    private void FixedUpdate()
    {
        movementFSM.FixedUpdate();
    }

    private void OnAnimatorMove()
    {
        movementFSM.OnAnimatorMove();
    }
}
