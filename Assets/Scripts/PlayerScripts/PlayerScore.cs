using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    public int score;
   
    public void saveHighScore()
    {
        if (score > PlayerPrefs.GetInt("highscore"))
        {
            PlayerPrefs.SetInt("highscore", score);
        }
    }

    public void CalculateFinalScore()
    {
        var dayTime = GetComponent<DayTimeManager>();
        score -= Mathf.RoundToInt((dayTime.getTime() - dayTime.startTime) / dayTime.timeSpeedMultiplier);
        if (score < 0) score = 0;
        saveHighScore();
    }

    public void AddScore(int amount)
    {
        score += amount;
    }
}
