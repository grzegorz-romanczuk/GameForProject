using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawn : MonoBehaviour
{
    public float maxDistance = 45f;
    public LayerMask ignoreLayers;
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

    private bool CheckForNavMesh(Vector3 targetDestination)
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(targetDestination, out hit, 1f, NavMesh.AllAreas))
        {           
            return true;
        }
        return false;
    }

    private bool CheckForUnit(Vector3 targetDestination, float radius)
    {        
        return Physics.CheckSphere(targetDestination, radius, ~ignoreLayers);        
    }
    private bool CheckIfInView(Vector3 targetDestination)
    {
        var viewportCord = Camera.main.WorldToViewportPoint(targetDestination);
        if((viewportCord.x > 1 || viewportCord.x < 0) && (viewportCord.y > 1 || viewportCord.y < 0))
        {
            
            return false;
        }
        Debug.Log(viewportCord);
        return true;
    }

    public IEnumerator SpawnEnemy(GameObject enemy)
    {
        Vector3 randomPos = Random.insideUnitSphere * maxDistance;

        if (CheckForNavMesh(randomPos))
        {
            randomPos = new Vector3(randomPos.x, 0, randomPos.z);
            if (!CheckForUnit(randomPos, enemy.GetComponent<CapsuleCollider>().radius * enemy.transform.localScale.x + 0.1f) && !CheckIfInView(randomPos))
            {
                Instantiate(enemy, randomPos, Quaternion.identity);

            }
            else
            {
                yield return new WaitForSeconds(0.1f);
                StartCoroutine(SpawnEnemy(Enemy));
            }
        }
        else
        {            
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(SpawnEnemy(Enemy));
        }
    }

    IEnumerator enemyDrop()
    {
        while(enemyCount < maxEnemyCount)
        {
            //xpos = Random.Range(spawn.transform.position.x - 5, spawn.transform.position.x + 5);
            //zpos = Random.Range(spawn.transform.position.z - 5, spawn.transform.position.z + 5);
            //Instantiate(Enemy, new Vector3(xpos, 0, zpos), Quaternion.identity);
            StartCoroutine(SpawnEnemy(Enemy));
            yield return new WaitForSeconds(spawnDelay);
            enemyCount++;
        }
    }
}
