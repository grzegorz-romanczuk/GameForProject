using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    private int currentWave = 1;
    private int maxMediumEnemies = 0, maxHardEnemies = 0, maxBossEnemies = 0;
    private int maxEnemiesActive = 0, currentEnemiesActive = 0;    
    private int waveValue = 0;

    public GameObject[] easyEnemyPrefabs, mediumEnemyPrefabs, hardEnemyPrefabs, bossEnemyPrefabs;
    public EnemySpawn enemySpawn;


    private void Start()
    {
                
    }

    private void CalculateWave()
    {        
        var waveX25 = (int)Mathf.Floor(currentWave / 25); //count every 25th wave
        var waveX10 = (int)Mathf.Floor(currentWave / 10); //count every 10th wave
        var waveX5 = (int)Mathf.Floor(currentWave / 5); //count every 5th wave
        var waveX2 = (int)Mathf.Floor(currentWave / 2); //count every 2th wave

        ResetData();

        waveValue = waveX25 * 50 + waveX10 * 25 + waveX5 * 10 + waveX2 * 5 + currentWave + 25;
        maxEnemiesActive = 10 + waveX5;
                
        if (waveX25 > 0) maxBossEnemies += waveX25;

        if (waveX10 > 0) maxBossEnemies++;

        if (waveX5 > 0)
        {
            for( int i = 0; i < waveX5 * 2; i++)
            {
                var rand = Random.Range(0, 100);
                if(rand <= 25 + waveX2)
                {
                    maxHardEnemies++;
                }
            }
        }

        if (waveX2 > 0)
        {
            for (int i = 0; i < waveX2 * 2; i++)
            {
                var rand = Random.Range(0, 100);
                if (rand <= 25 + waveX2)
                {
                    maxMediumEnemies++;
                }
            }
        }
    }

    private void ResetData()
    {
        maxBossEnemies = 0;        
        maxHardEnemies = 0;
        maxMediumEnemies = 0;
    }

    public void NextWave()
    {
        currentWave++;
    }

    public int GetWave()
    {
        return currentWave;
    }

}
