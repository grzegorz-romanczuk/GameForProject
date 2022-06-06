using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Maps : MonoBehaviour
{  

    public void SetForestMap()
    {
        PlayerPrefs.SetInt("map", 0);
    }
    public void SetDirtMap()
    {
        PlayerPrefs.SetInt("map", 1);

    }
    public void SetDesertMap()
    {
        PlayerPrefs.SetInt("map", 2);
    }
    public static int GetMap()
    {
        return PlayerPrefs.GetInt("map");

    }
    
}
