using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemySpawn))]
public class WaveSystem : MonoBehaviour
{
    private int currentWave = 0;
    private int maxMediumEnemies = 0, maxHardEnemies = 0, maxBossEnemies = 0;
    private int maxEnemiesActive = 0, currentEnemiesActive = 0;    
    private int waveValue = 0;
    public float spawnDelay = 1f;
    private bool isFinished = true;
    public Shop shop;
    public int waveCountdownTime = 10;

    public static bool waveIsRunning = false;
    public GameObject[] easyEnemyPrefabs, mediumEnemyPrefabs, hardEnemyPrefabs, bossEnemyPrefabs;
    private EnemySpawn enemySpawn;
    private MessageSystem messageSystem;
    private bool countdown = false;
    private float nextWaveTime = 0f;

    public TMPro.TMP_Text waveValueField;

    private int difficulty;
    private void Start()
    {
        CalculateWave();
        waveValueField.text = (currentWave + 1).ToString();
        enemySpawn = GetComponent<EnemySpawn>();
        messageSystem = GameObject.Find("MessageSystem").GetComponent<MessageSystem>();
        messageSystem.DisplayMessage("Press ENTER to start",0);
        difficulty = GameDifficulty.getDifficulty();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && isFinished && currentWave == 0)
        {

            NextWave();
            messageSystem.HideMessage();
        }

        if (!isFinished && currentEnemiesActive == 0 && waveValue <= 0)
        {
            isFinished = true;
            waveIsRunning = false;
            Invoke(nameof(EndWave), 1f);
        }
        if(countdown)
        {            
            messageSystem.WaveCountdown(Mathf.CeilToInt(nextWaveTime - Time.time),currentWave);
        }
    }

    public IEnumerator SpawnWave()
    {        
        isFinished = false;
        waveIsRunning = true;
        while (countdown)
        {
            if (countdown && Time.time > nextWaveTime)
            {
                countdown = false;
                messageSystem.HideCountdown();

            }
            else
            {
                yield return new WaitForSeconds(1);
            }           
        }
        
        while (waveValue > 0)
        {
            if (currentEnemiesActive < maxEnemiesActive)
            {
                if (maxBossEnemies > 0)
                {
                    SpawnBossEnemy();
                }
                else if (maxHardEnemies > 0)
                {
                    SpawnHardEnemy();
                }
                else if (maxMediumEnemies > 0)
                {
                    SpawnMediumEnemy();
                }
                else
                {
                    SpawnEasyEnemy();
                }
            }
            yield return new WaitForSeconds(spawnDelay / difficulty);
        }
        
    }

    private void SpawnEasyEnemy()
    {
        try
        {
            var rand = Mathf.RoundToInt(Random.Range(0, easyEnemyPrefabs.Length));
            var prefab = easyEnemyPrefabs[rand];
            var enemyValue = prefab.GetComponent<EnemyStats>().spawnValue;
            if (waveValue > 0)
            {
                StartCoroutine(enemySpawn.SpawnEnemy(prefab));
                currentEnemiesActive++;
                waveValue -= enemyValue;
            }
        }
        catch (UnityException e)
        {
            Debug.Log("Easy enemy spawn Error: " + e.Message);
        }
    }
    private void SpawnMediumEnemy()
    {
        maxMediumEnemies--;
        if(mediumEnemyPrefabs.Length > 0)
        {
            try
            {
                var rand = Mathf.RoundToInt(Random.Range(0, mediumEnemyPrefabs.Length));
                var prefab = mediumEnemyPrefabs[rand];
                var enemyValue = prefab.GetComponent<EnemyStats>().spawnValue;
                if (waveValue >= enemyValue)
                {
                    StartCoroutine(enemySpawn.SpawnEnemy(prefab));
                    currentEnemiesActive++;
                    waveValue -= enemyValue;
                }
            }
            catch(UnityException e) 
            { 
                Debug.Log("Medium enemy spawn Error: " + e.Message); 
            }
        }        
    }
    private void SpawnHardEnemy()
    {
        maxHardEnemies--;
        if (hardEnemyPrefabs.Length > 0)
        {
            try
            {
                var rand = Mathf.RoundToInt(Random.Range(0, hardEnemyPrefabs.Length));
                var prefab = hardEnemyPrefabs[rand];
                var enemyValue = prefab.GetComponent<EnemyStats>().spawnValue;
                if(waveValue >= enemyValue )
                {
                    StartCoroutine(enemySpawn.SpawnEnemy(prefab));
                    currentEnemiesActive++;
                    waveValue -= enemyValue;
                }
            }
            catch (UnityException e)
            {
                Debug.Log("Hard enemy spawn Error: " + e.Message);
            }
        }

    }
    private void SpawnBossEnemy()
    {
        maxBossEnemies--;
        if (bossEnemyPrefabs.Length > 0)
        {
            try
            {
                var rand = Mathf.RoundToInt(Random.Range(0, bossEnemyPrefabs.Length));
                var prefab = bossEnemyPrefabs[rand];
                var enemyValue = prefab.GetComponent<EnemyStats>().spawnValue;
                if (waveValue >= enemyValue)
                {
                    StartCoroutine(enemySpawn.SpawnEnemy(prefab));
                    currentEnemiesActive++;
                    waveValue -= enemyValue;
                }
            }
            catch (UnityException e)
            {
                Debug.Log("Boss enemy spawn Error: " + e.Message);
            }
        }
    }


    private void CalculateWave()
    {        
        var waveX25 = (int)Mathf.Floor(currentWave / 25); //count every 25th wave
        var waveX10 = (int)Mathf.Floor(currentWave / 10); //count every 10th wave
        var waveX5 = (int)Mathf.Floor(currentWave / 5); //count every 5th wave
        var waveX2 = (int)Mathf.Floor(currentWave / 2); //count every 2th wave

        ResetData();

        waveValue = waveX25 * 50 + waveX10 * 25 + waveX5 * 10 + waveX2 * 5 + currentWave + 25;
        maxEnemiesActive = (10 + waveX5) * difficulty;
                
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

    public void EndWave()
    {        
        shop.OpenShop();
    }
    public void NextWave()
    {        
        currentWave++;
        waveValueField.text = currentWave.ToString();
        CalculateWave();
        nextWaveTime = Time.time + waveCountdownTime;
        countdown = true;
        StartCoroutine(SpawnWave());
    }

    public int GetWave()
    {
        return currentWave;
    }

    public void EnemieDeath()
    {
        currentEnemiesActive--;
    }
        
}
