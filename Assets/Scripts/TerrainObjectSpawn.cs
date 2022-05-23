using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TerrainObjectSpawn : MonoBehaviour
{
    public float maxDistance = 45f;
    public LayerMask ignoreLayers;
    public GameObject obj1;
    public GameObject obj2;
    public GameObject obj3;
    public GameObject obj4;
    public GameObject obj5;
    private GameObject obj;
    public int maxEnemyCount;
    private GameObject player;
    int enemyCount = 0;
    public float spawnDelay;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        enemyDrop();
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
        if ((viewportCord.x > 1 || viewportCord.x < 0) && (viewportCord.y > 1 || viewportCord.y < 0))
        {

            return false;
        }
        return true;
    }

    public void SpawnEnemy(GameObject obj, float Ypos)
    {
        Vector3 randomPos = Random.insideUnitSphere * maxDistance + new Vector3(0.0f, 0.0f, 0.0f);

        if (CheckForNavMesh(randomPos))
        {
            randomPos = new Vector3(randomPos.x, Ypos, randomPos.z);
            if (!CheckForUnit(randomPos, obj.GetComponent<CapsuleCollider>().radius * obj.transform.localScale.x + 0.1f))
            {
                Instantiate(obj, randomPos, Quaternion.identity);

            }
            else
            {
                SpawnEnemy(obj, Ypos);
            }
        }
        else
        {
            SpawnEnemy(obj, Ypos);
        }
    }

    void enemyDrop()
    {
        while (enemyCount < maxEnemyCount)
        {
            int rand = Random.Range(1, 6);
            float Ypos = 0.0f;
            switch (rand)
            {
                case 1:
                    obj = obj1;
                    Ypos = 0.75f;
                    break;
                case 2:
                    obj = obj2;
                    Ypos = 1.0f;
                    break;
                case 3:
                    obj = obj3;
                    Ypos = 0.0f;
                    break;
                case 4:
                    obj = obj4;
                    Ypos = 0.0f;
                    break;
                case 5:
                    obj = obj5;
                    Ypos = 0.0f;
                    break;
            }
            Invoke("disableInfHP", 10);
            SpawnEnemy(obj, Ypos);
            enemyCount++;
        }
    }
}
