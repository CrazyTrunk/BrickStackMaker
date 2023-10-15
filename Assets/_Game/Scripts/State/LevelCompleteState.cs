
public class LevelCompleteState : IState<GameManager>
{
    public void OnEnter(GameManager gameManager)
    {
        //gameManager.UIManager.ShowMainMenu();
    }

    public void OnExecute(GameManager gameManager)
    {
    }

    public void OnExit(GameManager gameManager)
    {
        //gameManager.UIManager.HideMainMenu();
    }
}
