using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<T> where T : class
{
    private IState<T> _currentState;
    public void ChangeState(IState<T> newState, T entity)
    {
        _currentState?.OnExit(entity);
        _currentState = newState;
        _currentState?.OnEnter(entity);
    }

    public void ExecuteState(T entity)
    {
        _currentState?.OnExecute(entity);
    }
}
