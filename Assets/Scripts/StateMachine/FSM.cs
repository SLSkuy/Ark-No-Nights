using UnityEngine;

public abstract class FSM
{
    protected IState currentState;

    public void ChangeState(IState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }
    
    public void Update()
    {
        currentState?.Update();
    }

    public void FixedUpdate()
    {
        currentState?.FixedUpdate();
    }

    public void OnAnimatorMove()
    {
        currentState?.OnAnimatorMove();
    }
}
