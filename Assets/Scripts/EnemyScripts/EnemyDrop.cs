using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrop : MonoBehaviour
{
    public GameObject coin;
    public GameObject heart;
    public GameObject akMag;
    public GameObject uziMag;
    public GameObject m4Mag;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CoinDrop()
    {
        Vector3 position = transform.position + new Vector3(0.0f, 3.0f, 0.0f);
        
        int rand = Random.Range(1, 9);
        if(rand >= 1 && rand <= 3)
        {
            Instantiate(coin, position, Quaternion.identity);
            coin.SetActive(true);       
            //hajs
        }
        if(rand >= 4 && rand <= 6)
        {
            Instantiate(heart, position, Quaternion.identity);
            heart.SetActive(true);
            //Life
        }
        if(rand >= 7 && rand <= 9)
        {
            switch (rand)
            {
                case 7:
                    Instantiate(akMag, position, Quaternion.identity);
                    akMag.SetActive(true);
                    break;
                case 8:
                    Instantiate(uziMag, position, Quaternion.identity);
                    uziMag.SetActive(true);
                    break;
                case 9:
                    Instantiate(m4Mag, position, Quaternion.identity);
                    m4Mag.SetActive(true);
                    break;
            }
            //Ammo
        }
    }
}
