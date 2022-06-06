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
    public TextMeshProUGUI TextArmor;
    public TextMeshProUGUI TextAmmo;
    public Slider stamina;
    private GameObject Weapons;
    public Image WeaponImage;
    public GameObject ArmorUI;

    public List<Image> BoostImage;
    public List<Sprite> BoostSprites;
    public Image TurretImage;
    public List<Image> GrenadeImage;

    private PlayerScore playerScore;
    private PlayerMoney playerCash;
    private PlayerHealth playerHealth;
    private PlayerMover playerMover;
    private WeaponChanger WeapChanger;
    private GameObject gameManager;
    private List<GameObject> WeapList = new List<GameObject>();
    private PlayerTurretPlacer turretPlacer;




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
        turretPlacer = GameObject.Find("Player").GetComponent<PlayerTurretPlacer>();
        playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
        playerMover = GameObject.Find("Player").GetComponent<PlayerMover>();
        

        foreach (Image image in BoostImage)
        {
            image.enabled = false;
        }
        
        TurretImage.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        GUIScore();
        GUICash();
        GUIHeal();
        GUIStamina();
        GUIweapon();
        GUIArmor();
        
        GUIBoost();
        GUITurret();
        GUIGrenade();
    }

    void GUIScore()
    {
        TextScore.text = playerScore.score.ToString();
    }

    void GUIArmor()
    {
        if (playerHealth.currentArmor > 0) ArmorUI.SetActive(true);
        else ArmorUI.SetActive(false);

        TextArmor.text = "x " + playerHealth.currentArmor.ToString();
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

    bool isInfAmmo()
    {
        foreach (GameObject weap in WeapList)
        {
            if (weap.activeSelf && weap != GameObject.Find("M1911"))
            {
                if (weap.GetComponent<Gun>().ammoIsInfinite)
                {
                    return true;
                }
            }
        }
        return false;
    }


    void GUIBoost()
    {

        if (playerHealth.invulnerable)
        {
            BoostImage[0].enabled = true;
            BoostImage[0].sprite = BoostSprites[0];

        }
        else if (playerMover.isDoubleSpeed)
        {
            BoostImage[1].enabled = true;
            BoostImage[1].sprite = BoostSprites[3];

        }
        else if (playerMover.infiniteStamina)
        {
            BoostImage[2].enabled = true;
            BoostImage[2].sprite = BoostSprites[2];

        }
        else if (isInfAmmo())
        {
            BoostImage[3].enabled = true;
            BoostImage[3].sprite = BoostSprites[1];
        }
        else
        {
            foreach (Image image in BoostImage)
            {
                image.enabled = false;
            }
        }
    }

    void GUITurret()
    {
        if (turretPlacer.isTurretInEQ)
        {
            TurretImage.enabled = true;
        }
        else TurretImage.enabled = false;
    }

    void GUIGrenade()
    {
        for( int i = 0; i < 3; i++)
        {
            if(i < playerMover.getGrenades())
            {
                GrenadeImage[i].enabled = true;
            }
            else GrenadeImage[i].enabled = false;
        }
    }

}
