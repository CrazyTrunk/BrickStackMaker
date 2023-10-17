using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private StateMachine<GameManager> _stateMachine;
    private GameState _currentGameState;

    public GameState CurrentGameState
    {
        get => _currentGameState;
        set
        {
            _currentGameState = value;
            switch (_currentGameState)
            {
                case GameState.MainMenu:
                    _stateMachine.ChangeState(new MainMenuState(UIManager.Instance), this);
                    break;
                case GameState.Playing:
                    _stateMachine.ChangeState(new PlayingState(), this);
                    break;
                case GameState.Retry:
                    _stateMachine.ChangeState(new PlayingState(), this);
                    break;
            }
        }
    }
    private void Start()
    {
        _stateMachine = new StateMachine<GameManager>();
        CurrentGameState = GameState.MainMenu;
    }

    public void ChangeState(IState<GameManager> newState)
    {
        _stateMachine.ChangeState(newState, this);
    }

    private void Update()
    {
        _stateMachine.ExecuteState(this);
    }
}
