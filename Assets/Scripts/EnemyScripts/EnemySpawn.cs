using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawn : MonoBehaviour
{
    public GameObject Enemy;
    public GameObject spawn;
    public int maxEnemyCount;
    float xpos;
    float zpos;
    int enemyCount = 0;
    public float spawnDelay;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(enemyDrop());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator enemyDrop()
    {
        while(enemyCount < maxEnemyCount)
        {
            xpos = Random.Range(spawn.transform.position.x-5, spawn.transform.position.x + 5);
            zpos = Random.Range(spawn.transform.position.z - 5, spawn.transform.position.z + 5);
            Instantiate(Enemy, new Vector3(xpos, 0, zpos), Quaternion.identity);
            yield return new WaitForSeconds(spawnDelay);
            enemyCount++;
        }
    }
}
