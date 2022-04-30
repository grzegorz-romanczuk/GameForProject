using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseSystem : MonoBehaviour
{
    GameObject player;
    public static bool gameIsPaused = false;

    private void Start()
    {
        player = GameObject.Find("Player");
    }
    public void ResumeGame()
    {        
        Time.timeScale = 1f;
        gameIsPaused = false;
        AudioListener.pause = false;
        
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        gameIsPaused = true;
        AudioListener.pause = true ;
    }
}
