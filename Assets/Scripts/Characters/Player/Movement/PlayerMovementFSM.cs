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
        
        IdleState = new PlayerIdleState();
        WalkState = new PlayerWalkState();
        RunState = new PlayerRunState();
        SprintState = new PlayerSprintState();
    }
}
