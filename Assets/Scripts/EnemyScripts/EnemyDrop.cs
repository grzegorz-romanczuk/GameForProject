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
    public GameObject DblSPD; //20
    public GameObject InfHP; //20
    public GameObject InfSTM; //15
    public GameObject InfAmmo; //20

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
        if(rand >= 6 && rand <= 10)
        {
            Instantiate(heart, position, Quaternion.identity);
            //HP
        }
        if (rand >= 11 && rand <= 15)
        {
            if (weaponBelt.transform.GetChild(2).GetComponent<Gun>().isUnlocked) Instantiate(akMag, position, Quaternion.identity);
            //Magazynek AK
            else CoinDrop();
        }
        if (rand >= 16 && rand <= 20)
        {
            if (weaponBelt.transform.GetChild(1).GetComponent<Gun>().isUnlocked) Instantiate(uziMag, position, Quaternion.identity);
            //Magazynek Uzi
            else CoinDrop();
        }
        if (rand >= 21 && rand <= 25)
        {
            if (weaponBelt.transform.GetChild(3).GetComponent<Gun>().isUnlocked) Instantiate(m4Mag, position, Quaternion.identity);
            //Magazynek M4
            else CoinDrop();
        }
        if (rand >= 26 && rand <= 45)
        {
            Instantiate(DblSPD, position, Quaternion.identity);
            //2x szybko??
        }
        if (rand >= 46 && rand <= 65)
        {
            Instantiate(InfHP, position, Quaternion.identity);
            //Nieko?cz?ce si? HP
        }
        if (rand >= 66 && rand <= 80)
        {
            Instantiate(InfSTM, position, Quaternion.identity);
            //Nieko?cz?ca si? stamina
        }
        if (rand >= 81 && rand <= 100)
        {
            Instantiate(InfAmmo, position, Quaternion.identity);
            //Nieko?cz?ce si? ammo
        }
    }
}
