using System;
using UnityEngine;

[Serializable]
public class BlackBoard
{
    // 组件获取
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public Animator animator;
    [HideInInspector] public Transform camera;
    
    // 移动属性
    [Header("Movement Attributes")]
    public float targetSpeed;
    public float currentSpeed;
    public float rotationSpeed = 10f;
    
    // 移动信息
    [Header("Debug Info")]
    public Transform orientation;
    public Vector3 moveDirection;
    public bool canMove = true;
}
