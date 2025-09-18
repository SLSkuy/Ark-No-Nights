using UnityEngine;

public class PlayerMovementFSM : FSM
{
    public PlayerMovementFSM(BlackBoard blackBoard):base(blackBoard)
    {
        var idleState = new PlayerIdleState(this);
        var walkState = new PlayerWalkState(this);
        var runState = new PlayerRunState(this);
        var sprintState = new PlayerSprintState(this);
        
        AddState(PlayerStates.Idle,idleState);
        AddState(PlayerStates.Walking, walkState);
        AddState(PlayerStates.Running, runState);
        AddState(PlayerStates.Sprinting, sprintState);
    }
}
