
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private Button playButton;
    [SerializeField] private Button retryButton;

    [SerializeField] private GameObject finishUI;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private LevelManager levelManager;
    private int currentLevel = 1;
    private void Start()
    {
        HideAllMenu();
        playButton.onClick.AddListener(OnPlayButtonClick); 
    }

    private void HideAllMenu()
    {
        mainMenu.SetActive(false);
        finishUI.SetActive(false);
    }
    private void OnDestroy()
    {
        playButton.onClick.RemoveListener(OnPlayButtonClick);
    }
    private void OnPlayButtonClick()
    {
        HideAllMenu();
        levelManager.LoadLevel(currentLevel);
        gameManager.CurrentGameState = GameState.Playing;
    }
    private void OnRetryButtonClick(int level)
    {
        HideAllMenu();
        levelManager.LoadLevel(level);
        Player.Instance.OnInit();
        gameManager.CurrentGameState = GameState.Retry;
    }
    public void OnFinishUIShow()
    {
        retryButton.onClick.AddListener(() =>
        {
            OnRetryButtonClick(currentLevel);
        });
    }

    public void ShowMainMenuUI()
    {
        mainMenu.SetActive(true);
    }

    public void HideMainMenuUI()
    {
        mainMenu.SetActive(false);
    }
    public void ShowFinishUI()
    {
        finishUI.SetActive(true);
    }
    public void HideFinishUI()
    {
        finishUI.SetActive(false);
    }
}
