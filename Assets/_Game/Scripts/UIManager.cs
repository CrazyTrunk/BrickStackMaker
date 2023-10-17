
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] public GameObject mainMenu;
    [SerializeField] public GameObject finishMenu;
    public void OpenMenuUi()
    {
        mainMenu.SetActive(true);
        finishMenu.SetActive(false);
    }
    public void HideAllUI()
    {
        mainMenu.SetActive(false);
        finishMenu.SetActive(false);
    }
    public void OpenFinishUi()
    {
        finishMenu.SetActive(true);
        mainMenu.SetActive(false);
    }
    public void PlayButton()
    {
        mainMenu.SetActive(false);
        LevelManager.Instance.OnStart();
    }
    public void RetryButton()
    {
        finishMenu.SetActive(false);
        GameManager.Instance.ChangeState(GameState.Playing);
        LevelManager.Instance.LoadLevel();
        LevelManager.Instance.OnInit();
    }
    public void NextButton()
    {
        LevelManager.Instance.NextLevel();
        GameManager.Instance.ChangeState(GameState.Playing);
        HideAllUI();
    }
    public void MainMenuButton()
    {
        OpenMenuUi();
        GameManager.Instance.ChangeState(GameState.MainMenu);
        LevelManager.Instance.LoadLevel(1);
        LevelManager.Instance.OnInit();
    }
}
