using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefeatedEnemyCount : MonoBehaviour
{
    public int DefeatedEasyEnemy = 0;
    int OldDefeatedEasyEnemy = 0;
    public int DefeatedMediumEnemy = 0;
    int OldDefeatedMediumEnemy = 0;
    public int DefeatedHardEnemy = 0;
    int OldDefeatedHardEnemy = 0;
    public int DefeatedBossEnemy = 0;
    int OldDefeatedBossEnemy = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerScore playerScore = GetComponent<PlayerScore>();
        PlayerMoney playerMoney = GetComponent<PlayerMoney>();
        if (OldDefeatedEasyEnemy < DefeatedEasyEnemy)
        {
            playerScore.score += (DefeatedEasyEnemy - OldDefeatedEasyEnemy) * 100;
            playerMoney.Money += (DefeatedEasyEnemy - OldDefeatedEasyEnemy) * 50;
            OldDefeatedEasyEnemy = DefeatedEasyEnemy;
            //Debug.Log("Punkty Gracza: " + playerScore.score);
            //Debug.Log("Pieni?dze Gracza: " + playerMoney.Money);

        }
        if (OldDefeatedMediumEnemy < DefeatedMediumEnemy)
        {
            playerScore.score += (DefeatedMediumEnemy - OldDefeatedMediumEnemy) * 200;
            OldDefeatedMediumEnemy = DefeatedMediumEnemy;
        }
        if (OldDefeatedHardEnemy < DefeatedHardEnemy)
        {
            playerScore.score += (DefeatedHardEnemy - OldDefeatedHardEnemy) * 500;
            OldDefeatedHardEnemy = DefeatedHardEnemy;
        }
        if (OldDefeatedBossEnemy < DefeatedBossEnemy)
        {
            playerScore.score += (DefeatedBossEnemy - OldDefeatedBossEnemy) * 1000;
            OldDefeatedBossEnemy = DefeatedBossEnemy;
        }
    }

}
