using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Maps : MonoBehaviour
{
    public GameObject map1;
    public GameObject map2;
    public GameObject map3;


    public void GetForestMap()
    {
        map1.name = "map1";
        DontDestroyOnLoad(map1);
    }
    public void GetDirtMap()
    {
        map2.name = "map2";
        DontDestroyOnLoad(map2);
    }
    
    public void GetDesertMap()
    {
        map3.name = "map3";
        DontDestroyOnLoad(map3);
    }






}
