using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TurretAI : MonoBehaviour
{
    private Transform enemy;
    public LayerMask ignoreLayers;

    public LayerMask whatIsGround, whatIsEnemy;

    //Attacking
    public float timeBetweenAttacks, attackTime;
    bool alreadyAttacked, isAttacking;
    public GameObject projectile;
    public GameObject bulletSpawnPoint1;
    public GameObject bulletSpawnPoint2;
    public float bulletSpeed = 10f;
    public int ammo = 300;

    //States
    public float attackRange;
    public bool enemyInAttackRange;

    private void Awake()
    {

    }

    private void Update()
    {
        //Check for sight and attack range
        if(ammo == 0)
        {
            this.transform.parent.gameObject.SetActive(false);
        }
        enemy = GetClosestEnemy(FindGameObjectsInLayer());
        enemyInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsEnemy);
        if (enemyInAttackRange && !isAttacking && enemy != null)
        {
            StartAttack();
        }
        if (isAttacking) transform.LookAt(enemy);
    }

    private void StartAttack()
    {
        isAttacking = true;
        Invoke(nameof(AttackEnemy), attackTime);
    }
    private void AttackEnemy()
    {

        transform.LookAt(enemy);

        if (!alreadyAttacked)
        {
            ///Attack code here
            Rigidbody rb1 = Instantiate(projectile, bulletSpawnPoint1.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb1.rotation = transform.rotation;
            rb1.AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);
            rb1.AddForce(transform.up * 8f, ForceMode.Impulse);
            Rigidbody rb2 = Instantiate(projectile, bulletSpawnPoint2.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb2.rotation = transform.rotation;
            rb2.AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);
            rb2.AddForce(transform.up * 8f, ForceMode.Impulse);
            ammo -= 2;
            GetComponent<AudioSource>().Play();
            ///End of attack code

            Invoke(nameof(ResetAttack), timeBetweenAttacks);
            alreadyAttacked = true;
        }
        Invoke(nameof(FinishAttack), 0.5f);
    }

    private void HitPlayer()
    {

        transform.LookAt(enemy);

        if (!alreadyAttacked)
        {
            ///Attack code here
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
            alreadyAttacked = true;
        }
        Invoke(nameof(FinishAttack), 0.5f);
    }

    private void FinishAttack()
    {
        isAttacking = false;

    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    Transform GetClosestEnemy(GameObject[] enemies)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (GameObject t in enemies)
        {
            float dist = Vector3.Distance(t.transform.position, currentPos);
            if (dist < minDist)
            {
                if (Physics.Raycast(currentPos, t.transform.position, out RaycastHit hitInfo, dist, ~ignoreLayers))
                {
                    continue;
                }
                tMin = t.transform;
                minDist = dist;
                
            }
        }

        return tMin;
    }

    GameObject[] FindGameObjectsInLayer()
    {
        var goArray = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        var goList = new System.Collections.Generic.List<GameObject>();
        for (int i = 0; i < goArray.Length; i++)
        {
            if (goArray[i].layer == 14)
            {
                goList.Add(goArray[i]);
            }
        }
        return goList.ToArray();
    }
}
