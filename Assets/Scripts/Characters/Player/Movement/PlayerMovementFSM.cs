using UnityEngine;

public class PlayerMovementFSM : FSM
{
    public PlayerMovementFSM(BlackBoard blackBoard):base(blackBoard)
    {
        var idleState = new PlayerIdleState(this);
        var walkState = new PlayerWalkState(this);
        var runState = new PlayerRunState(this);
        var sprintState = new PlayerSprintState(this);
        var dashState = new PlayerDashState(this);
        var lightStop = new PlayerLightStopState(this);
        var mediumStop = new PlayerMediumStopState(this);
        var hardStop = new PlayerHardStopState(this);
        var jumpState = new PlayerJumpState(this);
        var lightLand =  new PlayerLightLandState(this);
        var hardLand = new PlayerHardLandState(this);
        var falling = new PlayerFallingState(this);
        
        AddState(PlayerStates.Idle,idleState);
        AddState(PlayerStates.Walking, walkState);
        AddState(PlayerStates.Running, runState);
        AddState(PlayerStates.Sprinting, sprintState);
        AddState(PlayerStates.Dashing, dashState);
        AddState(PlayerStates.LightStop, lightStop);
        AddState(PlayerStates.MediumStop, mediumStop);
        AddState(PlayerStates.HardStop, hardStop);
        AddState(PlayerStates.Jumping, jumpState);
        AddState(PlayerStates.LightLand, lightLand);
        AddState(PlayerStates.HardLand,hardLand);
        AddState(PlayerStates.Falling, falling);
    }
}
