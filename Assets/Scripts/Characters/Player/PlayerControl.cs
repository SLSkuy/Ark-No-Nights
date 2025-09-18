using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(HandleInput))]
public class PlayerControl : MonoBehaviour
{
    // 角色属性
    public BlackBoard board;
    private PlayerMovementFSM _movementFsm;

    private void Awake()
    {
        board.rb = GetComponent<Rigidbody>();
        board.animator = GetComponent<Animator>();
        board.input = GetComponent<HandleInput>();
        board.player = GetComponent<Transform>();
        
        _movementFsm = new PlayerMovementFSM(board);
        board.camera = Camera.main.transform;
    }

    private void Start()
    {
        _movementFsm.SwitchState(PlayerStates.Idle);
    }
    
    private void Update()
    {
        _movementFsm.HandleInput();
        _movementFsm.Update();
    }

    private void LateUpdate()
    {
        // 测试使用
        board.orientation.transform.eulerAngles = new Vector3(0,board.camera.transform.eulerAngles.y,0);
    }

    private void FixedUpdate()
    {
        _movementFsm.FixedUpdate();
    }

    private void OnAnimatorMove()
    {
        _movementFsm.OnAnimatorMove();
    }

    public void OnAnimationTransitionEvent()
    {
        _movementFsm.OnAnimationTransitionEvent();
    }
}
