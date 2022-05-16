using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDifficulty : MonoBehaviour
{
    private void Awake()
    {
        if (PlayerPrefs.GetInt("difficulty") == 0 )
        {
            Debug.Log(PlayerPrefs.GetInt("difficulty"));
            PlayerPrefs.SetInt("difficulty", 1);
        }
    }
    public static int getDifficulty()
    {
        return PlayerPrefs.GetInt("difficulty");
    }

    public static void setDifficulty(int diff)
    {
        PlayerPrefs.SetInt("difficulty", diff);
    }
    public static void setDifficultyToEasy()
    {
        PlayerPrefs.SetInt("difficulty", 1);
    }
    public static void setDifficultyToNormal()
    {
        PlayerPrefs.SetInt("difficulty", 2);
    }

    public static void setDifficultyToHard()
    {
        PlayerPrefs.SetInt("difficulty", 4);
    }

}
