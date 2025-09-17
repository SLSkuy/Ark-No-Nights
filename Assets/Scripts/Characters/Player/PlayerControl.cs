using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // 组件获取
    private Rigidbody _rb;
    private Animator _animator;
    
    // 有限状态机
    private PlayerMovementFSM _movementFSM;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        
        _movementFSM = new PlayerMovementFSM(this);
    }

    private void Start()
    {
        _movementFSM.ChangeState(_movementFSM.IdleState);
    }

    private void Update()
    {
        _movementFSM.HandleInput();
        _movementFSM.Update();
    }

    private void FixedUpdate()
    {
        _movementFSM.FixedUpdate();
    }
}
