using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GenerateTerrain : MonoBehaviour
{
    void Start()
    {
        
        GameObject map1 = GameObject.Find("map1");
        GameObject map2 = GameObject.Find("map2");
        GameObject map3 = GameObject.Find("map3");
        Debug.Log(map1);
        Debug.Log(map2);
        Debug.Log(map3);

        if (map1 != null)
        {
            GameObject.Find("Terrain").SetActive(true);
            Debug.Log("Mapa 1");
            

        }
        else if (map2 != null)
        {
            GameObject.Find("Dirt").SetActive(true);
            Debug.Log("Mapa 2");
        }
        else if (map3 != null)
        {
            GameObject.Find("Sand").SetActive(true);
            Debug.Log("Mapa 3");
        }

        else
        {
            GameObject.Find("Terrain").SetActive(true);
        }

        Destroy(map1);
        Destroy(map2);
        Destroy(map3);
    }

}
