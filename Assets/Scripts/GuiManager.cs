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
    public Slider stamina;

    private PlayerScore playerScore;
    private PlayerMoney playerCash;
    private PlayerHealth playerHealth;
    private PlayerMover playerMover;

    void Start()
    {
        stamina.maxValue = GameObject.Find("Player").GetComponent<PlayerMover>().maxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        playerScore = GetComponent<PlayerScore>();
        playerCash = GetComponent<PlayerMoney>();
        playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
        playerMover = GameObject.Find("Player").GetComponent<PlayerMover>();
        GUIScore();
        GUICash();
        GUIHeal();
        GUIStamina();
    }

    void GUIScore()
    {
        TextScore.text = playerScore.score.ToString();
    }

    void GUICash()
    {
        TextCash.text = playerCash.Money.ToString();
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
            TextHeal.text = "DEAD"; //?
        }

    }

    void GUIStamina()
    {
        if (playerMover != null)
        {
            stamina.value = playerMover.stamina;
        }
    }

}
