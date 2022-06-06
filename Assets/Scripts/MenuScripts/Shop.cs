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
    private GameObject player;
    private GameObject granade;

    [Header("UI Elements")]
    public GameObject WeaponPanelContent;
    public GameObject EqPanelContent;
    public TMPro.TMP_Text cashText;

    [Header("Kevlar Sprites")]
    public Sprite kevlarX1;
    public Sprite kevlarX2;
    public Sprite kevlarX3;

    [Header("Kevlar Prices")]
    public int kevlarX1Price = 50;
    public int kevlarX2Price = 150;
    public int kevlarX3Price = 250;

    [Header("Turret Settings")]
    public Sprite Turret;
    public int TurretPrice = 750;

    [Header("Grenade Settings")]
    public Sprite Grenade;
    public int GrenadePrice = 150;

    private int money;
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        player = GameObject.Find("Player");
        int i = 0;
        foreach(Transform item in weaponBelt.transform)
        {
            if (i != 0)
            {
                Weapons.Add(item.gameObject);              
            }
            i++;
        }
        var diff = GameDifficulty.getDifficulty();
        kevlarX1Price *= diff;
        kevlarX2Price *= diff;
        kevlarX3Price *= diff;
        ReloadItems();
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
        ReloadWeapons();
        ReloadEq();
    }

    private void ReloadWeapons()
    {
        if (WeaponPanelContent.transform.childCount > 0)
        {
            foreach (Transform child in WeaponPanelContent.transform)
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

    private void ReloadEq()
    {
        if (EqPanelContent.transform.childCount > 0)
        {
            foreach (Transform child in EqPanelContent.transform)
            {
                Destroy(child.gameObject);
            }
        }

        var newItem = Instantiate(blankItem, EqPanelContent.transform);
        newItem.name = kevlarX1.name + "Item";
        newItem.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => BuyKevlar(1));
        newItem.transform.GetChild(0).GetChild(0).GetComponent<TMPro.TMP_Text>().text = "Weak Kevlar";
        newItem.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = kevlarX1;
        newItem.transform.GetChild(0).GetChild(2).GetComponent<TMPro.TMP_Text>().text = kevlarX1Price + "$";
        if (player.GetComponent<PlayerHealth>().currentArmor >= 1 || money < kevlarX1Price) newItem.transform.GetChild(0).GetComponent<Button>().interactable = false;
        //weak kev
        newItem = Instantiate(blankItem, EqPanelContent.transform);
        newItem.name = kevlarX2.name + "Item";
        newItem.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => BuyKevlar(2));
        newItem.transform.GetChild(0).GetChild(0).GetComponent<TMPro.TMP_Text>().text = "Kevlar";
        newItem.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = kevlarX2;
        newItem.transform.GetChild(0).GetChild(2).GetComponent<TMPro.TMP_Text>().text = kevlarX2Price + "$";
        if (player.GetComponent<PlayerHealth>().currentArmor >= 2 || money < kevlarX2Price) newItem.transform.GetChild(0).GetComponent<Button>().interactable = false;
        //kev
        newItem = Instantiate(blankItem, EqPanelContent.transform);
        newItem.name = kevlarX3.name + "Item";
        newItem.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => BuyKevlar(3));
        newItem.transform.GetChild(0).GetChild(0).GetComponent<TMPro.TMP_Text>().text = "Hardened Kevlar";
        newItem.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = kevlarX3;
        newItem.transform.GetChild(0).GetChild(2).GetComponent<TMPro.TMP_Text>().text = kevlarX3Price + "$";
        if (player.GetComponent<PlayerHealth>().currentArmor >= 3 || money < kevlarX3Price) newItem.transform.GetChild(0).GetComponent<Button>().interactable = false;
        //hardened kev
        newItem = Instantiate(blankItem, EqPanelContent.transform);
        newItem.name = Turret.name + "Item";
        newItem.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => BuyTurret());
        newItem.transform.GetChild(0).GetChild(0).GetComponent<TMPro.TMP_Text>().text = "Auto Turret";
        newItem.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = Turret;
        newItem.transform.GetChild(0).GetChild(2).GetComponent<TMPro.TMP_Text>().text = TurretPrice + "$";
        if (player.GetComponent<PlayerTurretPlacer>().isTurretInEQ || money < TurretPrice) newItem.transform.GetChild(0).GetComponent<Button>().interactable = false;
        //Turret
        newItem = Instantiate(blankItem, EqPanelContent.transform);
        newItem.name = Grenade.name + "Item";
        newItem.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => BuyGrenade());
        newItem.transform.GetChild(0).GetChild(0).GetComponent<TMPro.TMP_Text>().text = "Grenade";
        newItem.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = Grenade;
        newItem.transform.GetChild(0).GetChild(2).GetComponent<TMPro.TMP_Text>().text = GrenadePrice + "$";
        if (player.GetComponent<PlayerMover>().getGrenades() >= player.GetComponent<PlayerMover>().maxGrenades || money < GrenadePrice) newItem.transform.GetChild(0).GetComponent<Button>().interactable = false;
        //Grenade

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

    private void BuyKevlar(int strenght)
    {
        player.GetComponent<PlayerHealth>().currentArmor = strenght;
        ReloadItems();
    }

    private void BuyTurret()
    {
        player.GetComponent<PlayerTurretPlacer>().isTurretInEQ = true;
        ReloadItems();
    }

    private void BuyGrenade()
    {
        player.GetComponent<PlayerMover>().addGrenade();
        ReloadItems();
    }
}
