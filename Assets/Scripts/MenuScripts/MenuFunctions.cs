using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFunctions : MonoBehaviour
{
    //audio
    public AudioSource audioSource;
    public AudioClip click;
    public AudioClip back;

    public TextMeshProUGUI TextScore;
    public void PlayGame()
    {
       
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        //audio

        audioSource.Play();
    }

    public void QuitGame()
    {
        Application.Quit();

        //audio
        audioSource.Play();

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

        //audio
       // audioSource.Play();
    }

    public void Options()
    {
        //audio
        audioSource.Play();
    }

    public void Back()
    {
        //audio
        audioSource.Play();
    }
}
