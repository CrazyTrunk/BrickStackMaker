using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public List<Level> levels = new List<Level>();
    public Player player;
    Level currentLevel;
    private int level = 1;
    [SerializeField] public GameObject unBrickHolder;
    private void Start()
    {
    
        UIManager.Instance.OpenMenuUi();
        LoadLevel();
    }
    public void LoadLevel()
    {
        LoadLevel(level);
        OnInit();
    }
    public void LoadLevel(int index)
    {
        if(currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }
        currentLevel = Instantiate(levels[index -1]);
        player.transform.position = currentLevel.startPoint.position;
    }
    public void OnInit()
    {
        player.OnInit();
        player.transform.position = currentLevel.startPoint.position;
    }
    public void OnStart()
    {
        GameManager.Instance.ChangeState(GameState.Playing);
    }
    public void OnFinish()
    {
        UIManager.Instance.OpenFinishUi();
        GameManager.Instance.ChangeState(GameState.Finish);
    }
    public void NextLevel()
    {
        level++;
        LoadLevel();
    }
}
