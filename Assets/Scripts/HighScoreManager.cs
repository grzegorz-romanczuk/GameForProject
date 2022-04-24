using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;

public class HighScoreManager : MonoBehaviour
{

    void CheckHighScore(int lastGameScore)
    {
        List<int> hScore = LoadHighScore();
        if(lastGameScore > hScore[hScore.Count - 1])
        {
            hScore.Add(lastGameScore);
            hScore.Sort();
            hScore.RemoveAt(hScore.Count - 1);
            SaveHighScore(hScore);
        }
    }

    List<int> LoadHighScore()
    {
        List<int> hScore = new List<int>();
        string[] hScoreString;
        for (int i = 0; i < 10; i++) hScore.Add(0);
        hScoreString = File.ReadAllLines(Application.dataPath + "/highscore.txt");
        for (int i = 0; i < hScoreString.Length; i++)
        {
            hScore[i] = Convert.ToInt32(hScoreString);
        }
        return hScore;
    }

    void SaveHighScore(List<int> hScore)
    {
        string[] hScoreString = { };

        for (int i = 0; i < hScore.Count; i++)
        {
            hScoreString[i] = hScore.ToString();
        }
        File.WriteAllLines(Application.dataPath + "/highscore.txt", hScoreString);
    }
}
