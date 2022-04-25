using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShopPanel : MonoBehaviour
{
    public int CurentIndex;
    public Sprite[] Items;
    public Image image;
    public Button BuyButon;
    public Text cash;
    public Text cost;
    public PlayerMoney money;
    public static bool IsShopPanelOpen=false;
    public GameObject ShopPanelUI;
    string cashString = "Cash: ";
    public Sprite[] buttons;
    private GameObject gameManager;
    // Update is called once per frame

    public int[] WeponCost;
    public bool[] WeponsUnlocked;

    void Start()
    {
        image.sprite=Items[CurentIndex];
        cash.text = cashString+money.Money;
        WeponsUnlocked = new bool[4] {false,false,false,false};
        gameManager = GameObject.Find("GameManager");
    }
    void Update()
    {
        cost.text =""+WeponCost[CurentIndex];
        image.sprite = Items[CurentIndex];
        cash.text = cashString + money.Money;

        if(WeponsUnlocked[CurentIndex])
        {
            cost.text = "Owned";
            cost.color = Color.green;
        }
        else if (money.Money>=WeponCost[CurentIndex])
            cost.color = Color.green; 
        else
             cost.color = Color.red;

        if(WeponsUnlocked[CurentIndex])
        {
            BuyButon.image.sprite = buttons[1];
        }
        else
        {
            BuyButon.image.sprite = buttons[0];
        }

        //if (Input.GetKeyUp(KeyCode.Space))
        //{
        //    if(IsShopPanelOpen)
        //    {
        //        Resume();
        //    }
        //    else
        //    {
        //        ShopOpen();
        //    }
        //}
    }

    public void Resume()
    {
        ShopPanelUI.SetActive(false);
        Time.timeScale = 1f;
        IsShopPanelOpen = false;
        gameManager.GetComponent<WaveSystem>().NextWave();
    }

    public void ShopOpen()
    {
        ShopPanelUI.SetActive(true);
        Time.timeScale = 0f;
        IsShopPanelOpen = true;
        WeponsUnlocked[3] = false; //Bo to amo .....
    }

    //Next i P

    public void Next()
    {
        cost.fontSize = 30;
        CurentIndex++;
        if (CurentIndex>=Items.Length)
        {
            CurentIndex = 0;
        }
        
    }

    public void Previous()
    {
        cost.fontSize = 30;
        CurentIndex--;
        if (CurentIndex ==-1)
        {
            CurentIndex = Items.Length-1;
        }
       
    }

    public void Buy()
    {
        if (money.Money >= WeponCost[CurentIndex] && WeponsUnlocked[CurentIndex]==false)
        {
            money.Money -= WeponCost[CurentIndex];
            WeponsUnlocked[CurentIndex] = true;
        }
        else
            cost.fontSize = 45;
    }

}
