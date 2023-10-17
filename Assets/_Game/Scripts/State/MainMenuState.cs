
public class MainMenuState : IState<GameManager>
{
    private UIManager _uiManager;

    public MainMenuState(UIManager uiManager)
    {
        _uiManager = uiManager;
    }

    public void OnEnter(GameManager gameManager)
    {
        _uiManager.ShowMainMenuUI();
    }

    public void OnExecute(GameManager gameManager)
    {
    }

    public void OnExit(GameManager gameManager) 
    {
        _uiManager.HideMainMenuUI();

    }
}
