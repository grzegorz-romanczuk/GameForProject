using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseSystem : MonoBehaviour
{
    GameObject player;
    //GameObject gui;
    public GameObject gamePause;
    public static bool gameIsPaused = false;

    private void Start()
    {
        Time.timeScale = 1f;
        gameIsPaused = false;
        player = GameObject.Find("Player");
        //gui = GameObject.Find("GUIWindow");
    }   

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && gamePause)
        {
            if (gameIsPaused)
            {
                ResumeGame();
                gamePause.SetActive(false);
                gamePause.transform.GetChild(1).gameObject.SetActive(false);
                //gui.SetActive(true);
            }
            
            else
            {
                PauseGame();
                gamePause.SetActive(true);
                //gui.SetActive(false);
            }
        }
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
        AudioListener.pause = true;                
    }

    public void MenuClick()
    {
        if (gamePause.transform.GetChild(1).gameObject.activeSelf)
        {
            gamePause.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            gamePause.transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    public void YesClick()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void NoClick()
    {
        gamePause.transform.GetChild(1).gameObject.SetActive(false);
    }


}
