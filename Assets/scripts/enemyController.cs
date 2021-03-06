﻿using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class enemyController : MonoBehaviour
{

    //
    private GameManager playerAlive;
    //control enemy 
    //script for enemy AI 
    // script used https://www.youtube.com/watch?v=xppompv1DBg by Brackeys
    private enemyStats dead;

    private Animator enemyPlayer;

    public NavMeshAgent agent
;
    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public GameObject projectile;

    private float minTorque = -10f;
    private float maxTorque = 10f;
    
    public float heightAttack = 1;
    public float attackZpos = 1;


    //patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    // public float time between attacks
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    public float attackSpd = 7;

    // states
    public float sightRange, attackRange;
    public bool playerInSight, playerInAttackRange;
    private bool enemyDead;


    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();

    }








    // Start is called before the first frame update
    void Start()
    {
        playerAlive = GameObject.Find("Player").GetComponent<GameManager>();
        enemyPlayer = gameObject.GetComponentInChildren<Animator>();
        dead = gameObject.GetComponentInChildren<enemyStats>();

    }

    // Update is called once per frame
    void Update()
    {
        if (playerAlive.gameOver == true)
        {
            playerInAttackRange = false;
            playerInSight = false;
        }

        // check for player and if in range
        if (playerAlive.gameOver == false)
        {
            playerInSight = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        }

        if (!playerInSight && !playerInAttackRange && enemyDead == false)
            Patroling();
        if (playerInSight && !playerInAttackRange && enemyDead == false)
            ChasePlayer();
        if (playerInSight && playerInAttackRange && enemyDead == false)
            AttackPlayer();
    }


    private void Patroling()
    {

        if (!walkPointSet)
            SearchWalkPoint();
        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);

        }
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }


    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;

        }
    }
    private void ChasePlayer()
    {
        if (playerAlive.gameOver == false)
        {
            agent.SetDestination(player.position);
        }
    }

    private void AttackPlayer()
    {
        if (playerAlive.gameOver == false)
        {
            agent.SetDestination(transform.position);
            transform.LookAt(player);

            if (!alreadyAttacked)
            {
                alreadyAttacked = true;
                enemyPlayer.SetBool("attack", true);


                StartCoroutine(attackAnim());
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }
        }
    }


    private void ResetAttack()
    {
        alreadyAttacked = false;
        

    }

    IEnumerator attackAnim()
    {
        yield return new WaitForSeconds(timeBetweenAttacks);
        
        Rigidbody rb = Instantiate(projectile, transform.position + (transform.forward * attackZpos) + (transform.up * heightAttack), Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddTorque(Random.Range(minTorque, maxTorque),
            Random.Range(minTorque, maxTorque), Random.Range(minTorque, maxTorque),
            ForceMode.Impulse);

        rb.velocity = transform.forward * attackSpd;
        enemyPlayer.SetBool("attack", false);
        Destroy(rb.gameObject, 3f);

    }
}
