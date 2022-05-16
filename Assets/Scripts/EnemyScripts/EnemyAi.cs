
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(EnemyStats))]
public class EnemyAi : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public bool enemyIsRanged = false;
    public float timeBetweenAttacks, attackTime;
    bool alreadyAttacked, isAttacking;
    public GameObject projectile;
    public GameObject bulletSpawnPoint;
    public float bulletSpeed = 10f;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private int difficulty;
    EnemyStats enemyStats;
    Animator animator;
    private void Awake()
    {
        if(!player) player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        enemyStats = GetComponent<EnemyStats>();
        difficulty = GameDifficulty.getDifficulty();
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange && !isAttacking) Patroling();
        if (playerInSightRange && !playerInAttackRange && !isAttacking) ChasePlayer();
        if (playerInAttackRange && playerInSightRange && !isAttacking) StartAttack();
        if (isAttacking) transform.LookAt(player);
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        //agent.SetDestination(player.position);

        NavMeshPath path = new NavMeshPath();

        agent.CalculatePath(player.transform.position, path);

        if (player.transform.position != agent.destination)
        {
            agent.SetPath(path);
        }

    }

    private void StartAttack()
    {

        if (enemyIsRanged)
        {
            //Make sure enemy doesn't move
            agent.SetDestination(transform.position);

            isAttacking = true;
            Invoke(nameof(AttackPlayer), attackTime);
        }        
    }
    private void AttackPlayer()
    {                

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            ///Attack code here
            animator.SetTrigger("Attack");
            Rigidbody rb = Instantiate(projectile, transform.position + new Vector3(0f,1f,0f), Quaternion.identity).GetComponent<Rigidbody>();
            rb.GetComponent<EnemyBullet>().bulletDamage = enemyStats.damage;
            rb.rotation = transform.rotation;
            rb.AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);
            ///End of attack code
                                 
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
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
