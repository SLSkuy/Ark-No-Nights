using UnityEngine;

public class PlayerWalkState : IState
{
    public void Enter()
    {
        Debug.Log(this.GetType().Name + " entered");
    }

    public void Exit()
    {

    }

    public void HandleInput()
    {

    }

    public void Update()
    {

    }

    public void FixedUpdate()
    {

    }
}
