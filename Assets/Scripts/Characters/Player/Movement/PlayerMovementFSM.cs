using UnityEngine;

public class PlayerMovementFSM : FSM
{    
    private PlayerControl _player;
    
    public PlayerIdleState IdleState { get; }
    public PlayerWalkState WalkState { get; }
    public PlayerRunState RunState { get; }
    public PlayerSprintState SprintState { get; }
    
    public PlayerMovementFSM(PlayerControl playerControl)
    {
        _player = playerControl;
        
        IdleState = new PlayerIdleState(_player);
        WalkState = new PlayerWalkState(_player);
        RunState = new PlayerRunState(_player);
        SprintState = new PlayerSprintState(_player);
    }
}
