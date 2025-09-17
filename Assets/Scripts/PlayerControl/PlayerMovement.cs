using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance { get; private set; }

    [SerializeField] private Transform orientation;
    [SerializeField] private float walkSpeed = 3f;
    [SerializeField] private float runSpeed = 7f;
    [SerializeField] private float rotationSpeed = 10f;
    
    // 组件获取
    private Animator _animator;
    private Rigidbody _rb;

    // 状态相关
    private enum PlayerState { Idle, Walking, Running }
    private PlayerState _curState = PlayerState.Idle;
    
    // 移动相关
    private Vector3 _direction;
    private bool _isMoving;
    private float _curSpeed, _targetSpeed;
    
    // 动画相关
    private int _speedHash;
    
    private void Awake()
    {
        Instance = this;
        
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _direction = Vector3.forward;
        _speedHash = Animator.StringToHash("Speed");
    }
    
    private void OnMove(InputValue value)
    {
        var dir = value.Get<Vector2>();
        _direction = orientation.transform.right * dir.x + orientation.transform.forward * dir.y;
        _direction.Normalize();
        
        _isMoving = dir != Vector2.zero;
        
        _curState = _isMoving ? PlayerState.Walking : PlayerState.Idle;
    }

    private void Update()
    {
        SpeedControl();
    }

    private void SpeedControl()
    {
        switch (_curState)
        {
            case PlayerState.Idle:
                _targetSpeed = 0f;
                break;
            case PlayerState.Walking:
                _targetSpeed = walkSpeed * _direction.magnitude;
                break;
            case PlayerState.Running:
                _targetSpeed = runSpeed * _direction.magnitude;
                break;
            default:
                break;
        }
        
        // 插值旋转模型朝向
        if (_isMoving)transform.forward = Vector3.Slerp(transform.forward, _direction, Time.deltaTime * rotationSpeed);
        
        // 当前速度递增到目标速度
        _curSpeed = Mathf.Abs(_curSpeed - _targetSpeed) > 0.01f ? 
            Mathf.Lerp(_curSpeed, _targetSpeed, 0.1f) : _targetSpeed;
        _animator.SetFloat(_speedHash, _curSpeed);
    }
    
    private void OnAnimatorMove()
    {
        Vector3 dirVelocity = _direction * _animator.velocity.magnitude;
        Vector3 fixedVelocity = new  Vector3(dirVelocity.x, _rb.linearVelocity.y, dirVelocity.z);
        _rb.linearVelocity = fixedVelocity;
    }
}
