using UnityEngine;

public class PlayerMovementFSM : FSM
{
    // 待机状态
    public PlayerIdleState IdleState { get; }
    
    // 移动状态
    public PlayerWalkState WalkState { get; }
    public PlayerRunState RunState { get; }
    public PlayerSprintState SprintState { get; }

    private PlayerControl _player;

    public PlayerMovementFSM(PlayerControl playerControl)
    {
        _player = playerControl;
        
        IdleState = new PlayerIdleState(_player);
        WalkState = new PlayerWalkState(_player);
        RunState = new PlayerRunState(_player);
        SprintState = new PlayerSprintState(_player);
    }
}
