using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject weaponBelt;
    public GameObject blankItem;
    private List<GameObject> Weapons = new List<GameObject>();
    private GameObject gameManager;

    [Header("UI Elements")]
    public GameObject WeaponPanelContent;
    public TMPro.TMP_Text cashText;


    private int money;
    void Start()
    {
        gameManager = GameObject.Find("GameManager");        
        int i = 0;
        foreach(Transform item in weaponBelt.transform)
        {
            if (i != 0)
            {
                Weapons.Add(item.gameObject);              
            }
            i++;
        }        
    }
    public void CloseShop()
    {
        gameManager.GetComponent<PauseSystem>().ResumeGame();
        transform.GetChild(0).gameObject.SetActive(false);
        gameManager.GetComponent<WaveSystem>().NextWave();
    }
    public void OpenShop()
    {
        gameManager.GetComponent<PauseSystem>().PauseGame();
        transform.GetChild(0).gameObject.SetActive(true);
        ReloadItems();
    }

    private void ReloadItems()
    {
        money = gameManager.GetComponent<PlayerMoney>().Money;
        cashText.text = money + "$";
        if(WeaponPanelContent.transform.childCount > 0)
        {
            foreach(Transform child in WeaponPanelContent.transform)
            {
                Destroy(child.gameObject);
            }
        }
        
        foreach (GameObject item in Weapons)
        {
            var gunScript = item.GetComponent<Gun>();
            
            if (gunScript.isUnlocked)
            {
                var newItem = Instantiate(blankItem, WeaponPanelContent.transform);
                newItem.name = item.name + "Item";
                newItem.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => BuyItem(item));
                newItem.transform.GetChild(0).GetChild(0).GetComponent<TMPro.TMP_Text>().text = item.name + " Ammunition\n" + (gunScript.Ammo + gunScript.GetCurrentAmmo()) + "/" + gunScript.maxAmmo;
                newItem.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = gunScript.ammoIcon;
                newItem.transform.GetChild(0).GetChild(2).GetComponent<TMPro.TMP_Text>().text = gunScript.ammoCost + "$";
                if (money < gunScript.ammoCost || gunScript.Ammo + gunScript.GetCurrentAmmo() >= gunScript.maxAmmo) newItem.transform.GetChild(0).GetComponent<Button>().interactable = false;


            }
            else
            {
                var newItem = Instantiate(blankItem, WeaponPanelContent.transform);
                newItem.name = item.name + "Item";                
                newItem.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => BuyItem(item));
                newItem.transform.GetChild(0).GetChild(0).GetComponent<TMPro.TMP_Text>().text = item.name;
                newItem.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = gunScript.weaponIcon;
                newItem.transform.GetChild(0).GetChild(2).GetComponent<TMPro.TMP_Text>().text = gunScript.weaponCost + "$";
                if (money < gunScript.weaponCost) newItem.transform.GetChild(0).GetComponent<Button>().interactable = false;
            }
            
        }        
    }

    private void BuyItem(GameObject weapon)
    {
        Debug.Log("kupiles " + weapon.name);
        var gun = weapon.GetComponent<Gun>();
        if (gun.isUnlocked)
        {
            gameManager.GetComponent<PlayerMoney>().AddMoney(-gun.ammoCost);
            gun.AmmoBought();
        }
        else
        {
            gameManager.GetComponent<PlayerMoney>().AddMoney(-gun.weaponCost);
            gun.isUnlocked = true;
            gun.Ammo = gun.magazineSize * 5;
        }
        ReloadItems();
    }    
}
