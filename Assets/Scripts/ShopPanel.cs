using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShopPanel : MonoBehaviour
{
    public int CurentIndex;
    public Sprite[] Items;
    public Image image;

    public static bool IsShopPanelOpen=false;
    public GameObject ShopPanelUI;
    // Update is called once per frame

    void Start()
    {
        image.sprite=Items[CurentIndex];
    }
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space))
        {
            if(IsShopPanelOpen)
            {
                Resume();
            }
            else
            {
                ShopOpen();
            }
        }
    }

    public void Resume()
    {
        ShopPanelUI.SetActive(false);
        Time.timeScale = 1f;
        IsShopPanelOpen = false;
    }

    void ShopOpen()
    {
        ShopPanelUI.SetActive(true);
        Time.timeScale = 0f;
        IsShopPanelOpen = true;
    }

    //Next i P

    public void Next()
    {
        CurentIndex++;
        if (CurentIndex>=Items.Length)
        {
            CurentIndex = 0;
        }
        image.sprite = Items[CurentIndex];
    }

    public void Previous()
    {
        CurentIndex--;
        if (CurentIndex ==-1)
        {
            CurentIndex = Items.Length-1;
        }
        image.sprite = Items[CurentIndex];
    }
}
