using System.Collections.Generic;
using UnityEngine;

public abstract class FSM
{
    public BlackBoard board;
    private IState _currentState;
    private Dictionary<PlayerStates,IState> _playerStates;

    public FSM(BlackBoard blackBoard)
    {
        board = blackBoard;
        _playerStates = new Dictionary<PlayerStates, IState>();
    }

    protected void AddState(PlayerStates type,IState state)
    {
        if (!_playerStates.TryAdd(type, state))
        {
            Debug.Log("State already exists!");
        }
    }

    public void RemoveState(PlayerStates type)
    {
        _playerStates.Remove(type);
    }

    public void SwitchState(PlayerStates type)
    {
        if (!_playerStates.ContainsKey(type))
        {
            Debug.Log(type + " State not exists!");
            return;
        }
        _currentState?.Exit();
        _currentState = _playerStates[type];
        _currentState.Enter();
    }
    
    public void Update()
    {
        _currentState?.Update();
    }

    public void HandleInput()
    {
        _currentState?.HandleInput();
    }

    public void FixedUpdate()
    {
        _currentState?.FixedUpdate();
    }

    public void OnAnimatorMove()
    {
        _currentState?.OnAnimatorMove();
    }
}
