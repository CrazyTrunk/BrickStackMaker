using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<Level> levels = new List<Level>();
    public Player player;
    Level currentLevel;
    [SerializeField] public GameObject unBrickHolder;
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

    }
    public void OnFinish()
    {

    }
}
