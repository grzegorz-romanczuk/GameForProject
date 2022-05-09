using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFunctions : MonoBehaviour
{
    public TextMeshProUGUI TextScore;
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GetHighScore()
    {
        var highScore = GetComponent<HighScoreManager>().LoadHighScore();        
        highScore.Reverse();
        TextScore.text = "";
        for (int i = 0; i < highScore.Count; i++)
        {
            TextScore.text += i + 1 +".\t" + highScore[i] + "\n";
        }
    }
}
