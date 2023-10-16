using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<Level> levels = new List<Level>();
    public Player player;
    Level currentLevel;
    private void Start()
    {
        LoadLevel(1);
        OnInit();
    }
    public void LoadLevel(int index)
    {
        if(currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }
        currentLevel = Instantiate(levels[index -1]);
    }
    public void OnInit()
    {
        player.transform.position = currentLevel.startPoint.position;
        player.OnInit();
    }
    public void OnStart()
    {

    }
    public void OnFinish()
    {

    }
}
