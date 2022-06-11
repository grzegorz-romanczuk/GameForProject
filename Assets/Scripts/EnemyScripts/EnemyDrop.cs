using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrop : MonoBehaviour
{
    public GameObject coin; //5
    public GameObject heart; //5
    public GameObject akMag; //5
    public GameObject uziMag; //5
    public GameObject m4Mag; //5
    public GameObject m249Mag; //5
    public GameObject shotgunMag; //5
    public GameObject rpgMag; //5
    public GameObject DblSPD; //10
    public GameObject InfHP; //10
    public GameObject InfSTM; //10
    public GameObject InfAmmo; //10

    private GameObject weaponBelt;
    // Start is called before the first frame update
    void Start()
    {
        weaponBelt = GameObject.Find("Weapons");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CoinDrop()
    {
        Vector3 position = transform.position + new Vector3(0.0f, 3.0f, 0.0f);
        
        int rand = Random.Range(1, 100);
        if(rand >= 1 && rand <= 5)
        {
            Instantiate(coin, position, Quaternion.identity);
            //Pieni?dz
        }
        else if(rand >= 6 && rand <= 10)
        {
            Instantiate(heart, position, Quaternion.identity);
            //HP
        }
        else if(rand >= 11 && rand <= 15)
        {
            if (weaponBelt.transform.GetChild(2).GetComponent<Gun>().isUnlocked) Instantiate(akMag, position, Quaternion.identity);
            //Magazynek AK
            else CoinDrop();
        }
        else if(rand >= 16 && rand <= 20)
        {
            if (weaponBelt.transform.GetChild(1).GetComponent<Gun>().isUnlocked) Instantiate(uziMag, position, Quaternion.identity);
            //Magazynek Uzi
            else CoinDrop();
        }
        else if(rand >= 21 && rand <= 25)
        {
            if (weaponBelt.transform.GetChild(3).GetComponent<Gun>().isUnlocked) Instantiate(m4Mag, position, Quaternion.identity);
            //Magazynek M4
            else CoinDrop();
        }
        else if(rand >= 26 && rand <= 30)
        {
            if (weaponBelt.transform.GetChild(4).GetComponent<Gun>().isUnlocked) Instantiate(shotgunMag, position, Quaternion.identity);
            //Magazynek Shotgun
            else CoinDrop();
        }
        else if(rand >= 31 && rand <= 35)
        {
            if (weaponBelt.transform.GetChild(5).GetComponent<Gun>().isUnlocked) Instantiate(m249Mag, position, Quaternion.identity);
            //Magazynek M249
            else CoinDrop();
        }
        else if(rand >= 36 && rand <= 45)
        {
            if (weaponBelt.transform.GetChild(6).GetComponent<Gun>().isUnlocked) Instantiate(rpgMag, position, Quaternion.identity);
            //Magazynek RPG
            else CoinDrop();
        }
        else if(rand >= 61 && rand <= 70)
        {
            Instantiate(DblSPD, position, Quaternion.identity);
            //2x szybko??
        }
        else if(rand >= 71 && rand <= 80)
        {
            Instantiate(InfHP, position, Quaternion.identity);
            //Nieko?cz?ce si? HP
        }
        else if(rand >= 81 && rand <= 90)
        {
            Instantiate(InfSTM, position, Quaternion.identity);
            //Nieko?cz?ca si? stamina
        }
        else if(rand >= 91 && rand <= 100)
        {
            Instantiate(InfAmmo, position, Quaternion.identity);
            //Nieko?cz?ce si? ammo
        }
    }
}
