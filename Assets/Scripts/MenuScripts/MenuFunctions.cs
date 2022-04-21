using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFunctions : MonoBehaviour
{    
    public void PlayGame()
    {
        SceneManager.LoadScene("Piotrek");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
