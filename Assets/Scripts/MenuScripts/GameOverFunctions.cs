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

    
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void NewGame()
    {
        SceneManager.LoadScene("Piotrek");
    }

    public void GetScore()
    {
        var scorePoints = GameObject.Find("GameManager").GetComponent<PlayerScore>().score;
        TextScore.text = "Your score: " + scorePoints.ToString();
    }
    public void ActiveGameOverMenu()
    {
        
        
        GetScore();
        GameOver.SetActive(true);
    }
}
