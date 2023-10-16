using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public UIManager UIManager;      
    public LevelManager LevelManager;
    private GameState gameState;
    private IState<GameManager> currentState;
    private int currentLevel = 1;


    // Start is called before the first frame update
    void Start()
    {
        ChangeState(new MainMenuState());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeState(IState<GameManager> newState)
    {
        currentState?.OnExit(this);
        currentState = newState;
        currentState?.OnEnter(this);
    }
}
