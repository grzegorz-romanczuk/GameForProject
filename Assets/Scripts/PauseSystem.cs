using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseSystem : MonoBehaviour
{
    GameObject player;
    GameObject gui;
    public GameObject gamePause;
    public static bool gameIsPaused = false;

    private void Start()
    {
        player = GameObject.Find("Player");
        gui = GameObject.Find("GUIWindow");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                ResumeGame();
                gui.SetActive(true);
            }
            
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        gameIsPaused = false;
        AudioListener.pause = false;
        gamePause.SetActive(false);
        if (gamePause.transform.GetChild(1).gameObject.activeSelf)
        {
            gamePause.transform.GetChild(1).gameObject.SetActive(false);

        }
        
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        gameIsPaused = true;
        AudioListener.pause = true;
        gamePause.SetActive(true);
        gui.SetActive(false);
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
