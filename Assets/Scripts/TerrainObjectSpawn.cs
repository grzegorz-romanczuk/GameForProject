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
    public List<GameObject> map1Objects;
    public List<GameObject> map2Objects;
    public List<GameObject> map3Objects;
    public List<GameObject> terrains;
    private GameObject obj;
    public int maxEnemyCount;
    private GameObject player;
    int enemyCount = 0;
    public float spawnDelay;
    private int map;
    // Start is called before the first frame update
    private void Start()
    {
        map = Maps.GetMap();        
        foreach(var terrain in terrains)
        {
            terrain.SetActive(false);
        }
        terrains[map].SetActive(true);
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
        Quaternion randomRot = Quaternion.Euler(0, Random.Range(0.0f, 360.0f), 0);

        if (CheckForNavMesh(randomPos))
        {
            randomPos = new Vector3(randomPos.x, Ypos, randomPos.z);
            if (!CheckForUnit(randomPos, obj.GetComponent<CapsuleCollider>().radius * obj.transform.localScale.x + 0.1f))
            {
                Instantiate(obj, randomPos, randomRot);

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
        if(map == 0)
        {
            while (enemyCount < maxEnemyCount)
            {
                int rand = Random.Range(1, 6);
                float Ypos = 0.0f;
                switch (rand)
                {
                    case 1:
                        obj = map1Objects[0];
                        Ypos = 0.75f;
                        break;
                    case 2:
                        obj = map1Objects[1];
                        Ypos = 1.0f;
                        break;
                    case 3:
                        obj = map1Objects[2];
                        Ypos = 0.0f;
                        break;
                    case 4:
                        obj = map1Objects[3];
                        Ypos = 0.0f;
                        break;
                    case 5:
                        obj = map1Objects[4];
                        Ypos = 0.0f;
                        break;
                }
                SpawnEnemy(obj, Ypos);
                enemyCount++;
            }
        }
        else if (map == 1)
        {
            while (enemyCount < maxEnemyCount)
            {
                int rand = Random.Range(1, 6);
                float Ypos = 0.0f;
                switch (rand)
                {
                    case 1:
                        obj = map2Objects[0];
                        Ypos = obj.transform.position.y;
                        break;
                    case 2:
                        obj = map2Objects[1];
                        Ypos = obj.transform.position.y;
                        break;
                    case 3:
                        obj = map2Objects[2];
                        Ypos = obj.transform.position.y;
                        break;
                    case 4:
                        obj = map2Objects[3];
                        Ypos = obj.transform.position.y;
                        break;
                    case 5:
                        obj = map2Objects[4];
                        Ypos = obj.transform.position.y;
                        break;
                }
                SpawnEnemy(obj, Ypos);
                enemyCount++;
            }
        }
        else if (map == 2)
        {
            while (enemyCount < maxEnemyCount)
            {
                int rand = Random.Range(1, 6);
                float Ypos = 0.0f;
                switch (rand)
                {
                    case 1:
                        obj = map3Objects[0];
                        Ypos = obj.transform.position.y;
                        break;
                    case 2:
                        obj = map3Objects[1];
                        Ypos = obj.transform.position.y;
                        break;
                    case 3:
                        obj = map3Objects[2];
                        Ypos = obj.transform.position.y;
                        break;
                    case 4:
                        obj = map3Objects[3];
                        Ypos = obj.transform.position.y;
                        break;
                    case 5:
                        obj = map3Objects[4];
                        Ypos = obj.transform.position.y;
                        break;
                }
                SpawnEnemy(obj, Ypos);
                enemyCount++;
            }
        }
    }
}
