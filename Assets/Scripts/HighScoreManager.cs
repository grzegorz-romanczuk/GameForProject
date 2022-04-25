using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using System.Linq;

public class HighScoreManager : MonoBehaviour
{    

    List<int> highScore;
    public void CheckHighScore(int lastGameScore)
    {
        List<int> hScore = LoadHighScore();
        if(hScore == null)
        {
            CreateHighScore();
            hScore = LoadHighScore();
        }
        if(lastGameScore > hScore[0])
        {
            hScore.Add(lastGameScore);
            hScore.Sort();
            if(hScore.Count > 10) hScore.RemoveAt(0);
            SaveHighScore(hScore);
        }
    }

    public List<int> LoadHighScore()
    {
        try
        {
            List<int> hScore = new List<int>();
            string[] hScoreString;
            for (int i = 0; i < 10; i++) hScore.Add(0);
            hScoreString = File.ReadAllLines(Application.dataPath + "/highscore.txt");
            for (int i = 0; i < hScoreString.Length; i++)
            {
                hScore[i] = Convert.ToInt32(hScoreString[i]);
            }
            return hScore;
        }
        catch (Exception e)
        {
            Debug.Log(e);
            CreateHighScore();
            return LoadHighScore();
        }
    }

    public void CreateHighScore()
    {
        int[] zeros = new int[10];
        for (int i = 0; i < zeros.Length; i++)
        {
            zeros[i] = 0;
        }
        SaveHighScore(zeros.ToList());
    }
    public void SaveHighScore(List<int> hScore)
    {
        List<string> hScoreString = new List<string>();

        for (int i = 0; i < hScore.Count; i++)
        {
            hScoreString.Add(hScore[i].ToString());
        }
        Debug.Log("tu");
        File.WriteAllLines(Application.dataPath + "/highscore.txt", hScoreString);
    }
}
