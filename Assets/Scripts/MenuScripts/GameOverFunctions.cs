using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverFunctions : MonoBehaviour
{
    public GameObject GameOver;
    public TextMeshProUGUI TextScore;
    private GameObject gui;

    private void Start()
    {
        gui = GameObject.Find("GUIWindow");
    }


    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void NewGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GetScore()
    {
        var scorePoints = GameObject.Find("GameManager").GetComponent<PlayerScore>().score;
        TextScore.text = "Your score: " + scorePoints.ToString();

        var highScore = GetComponent<HighScoreManager>();
        highScore.CheckHighScore(scorePoints);

    }
    
    public void ActiveGameOverMenu()
    {
        GetScore();
        GameOver.SetActive(true);
        gui.SetActive(false);
    }
}

