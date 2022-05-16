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
    private PlayerScore playerScore;

    private void Start()
    {
        gui = GameObject.Find("GUIWindow");
        playerScore = GameObject.Find("GameManager").GetComponent<PlayerScore>();
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
        var scorePoints = playerScore.score;
        TextScore.text = "Your score: " + scorePoints.ToString();

        var highScore = GetComponent<HighScoreManager>();
        highScore.CheckHighScore(scorePoints);

    }
    
    public void ActiveGameOverMenu()
    {
        playerScore.CalculateFinalScore();
        GetScore();
        GameOver.SetActive(true);
        gui.SetActive(false);
    }
}

