using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GuiManager : MonoBehaviour
{
    public TextMeshProUGUI TextScore;
    public TextMeshProUGUI TextCash;
    public TextMeshProUGUI TextHeal;
    public TextMeshProUGUI TextAmmo;
    public Slider stamina;
    private GameObject Weapons;
    public Image WeaponImage;


    private PlayerScore playerScore;
    private PlayerMoney playerCash;
    private PlayerHealth playerHealth;
    private PlayerMover playerMover;
    private WeaponChanger WeapChanger;
    private GameObject gameManager;
    private List<GameObject> WeapList = new List<GameObject>();
    void Start()
    {
        stamina.maxValue = GameObject.Find("Player").GetComponent<PlayerMover>().maxStamina;
        Weapons = GameObject.Find("Weapons");
        WeapChanger = Weapons.GetComponent<WeaponChanger>();
        gameManager = GameObject.Find("GameManager");
        foreach (Transform child in Weapons.transform)
        {
            WeapList.Add(child.gameObject);
        }
        playerScore = gameManager.GetComponent<PlayerScore>();
        playerCash = gameManager.GetComponent<PlayerMoney>();
        playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
        playerMover = GameObject.Find("Player").GetComponent<PlayerMover>();
    }

    // Update is called once per frame
    void Update()
    {        
        GUIScore();
        GUICash();
        GUIHeal();
        GUIStamina();
        GUIweapon();
    }

    void GUIScore()
    {
        TextScore.text = playerScore.score.ToString();
    }

    void GUICash()
    {
        TextCash.text = playerCash.Money.ToString() + "$";
    }

    void GUIHeal()
    {
        if (playerHealth != null)
        {
            TextHeal.text = "x " + playerHealth.currentHealth.ToString();
            if (playerHealth.currentHealth == 1)
            {
                TextHeal.color = Color.red;
            }
        }
        else
        {
            TextHeal.text = "0";
        }

    }

    void GUIStamina()
    {
        if (playerMover != null)
        {
            stamina.value = playerMover.stamina;
        }
    }
    void GUIAmmo(GameObject weap)
    {
        var weapInfo = weap.GetComponent<Gun>();
        if (weapInfo.ammoIsInfinite)
        {
            TextAmmo.text = weapInfo.GetCurrentAmmo().ToString();
        }
        else
        {
            TextAmmo.text = weapInfo.GetCurrentAmmo().ToString() + "/" + weapInfo.Ammo.ToString();
        }   
    }
    void GUIweapon()
    {
        foreach (GameObject weap in WeapList)
        {
            if (weap.activeSelf)
            {
                WeaponImage.sprite = weap.GetComponent<Gun>().weaponIcon;
                GUIAmmo(weap);
            }
        }
    }
}
