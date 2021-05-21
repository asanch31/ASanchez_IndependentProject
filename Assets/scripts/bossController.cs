﻿using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class bossController : MonoBehaviour
{
    //Controller to control boss actions

    //script for enemy AI 
    // script used https://www.youtube.com/watch?v=xppompv1DBg by Brackeys
    private enemyStats dead;

    private Animator enemyPlayer;

    public NavMeshAgent agent
;
    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;
    // what is enemy shooting/throwing
    public GameObject projectile;

    private float minTorque = -10f;
    private float maxTorque = 10f;
   
    public float heightAttack = 2;
    public float attackZpos = 4;


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
        enemyPlayer = gameObject.GetComponentInChildren<Animator>();
        dead = gameObject.GetComponentInChildren<enemyStats>();

    }

    // Update is called once per frame
    void Update()
    {


        // check for player and if in range
        playerInSight = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);


        if (!playerInSight && !playerInAttackRange && enemyDead == false)
            Patroling();
        if (playerInSight && !playerInAttackRange && enemyDead == false)
            ChasePlayer();
        if (playerInSight && playerInAttackRange && enemyDead == false)
            AttackPlayer();
    }

    //enemy moves when player in not in sight
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

    //finds walking points
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

    //follow player when in range
    private void ChasePlayer()
    {

        agent.SetDestination(player.position);
    }

    // attack player in range
    private void AttackPlayer()
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

    //cooldown of player attack
    private void ResetAttack()
    {
        alreadyAttacked = false;
        enemyPlayer.SetBool("attack", false);

    }
    //slowdown attack to align with animation
    IEnumerator attackAnim()
    {
        yield return new WaitForSeconds(timeBetweenAttacks);
        
        Rigidbody rb = Instantiate(projectile, transform.position + (transform.forward * attackZpos) + (transform.up * heightAttack), Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddTorque(Random.Range(minTorque, maxTorque),
            Random.Range(minTorque, maxTorque), Random.Range(minTorque, maxTorque),
            ForceMode.Impulse);

        rb.velocity = transform.forward * attackSpd;
        
        Destroy(rb.gameObject, 3f);

    }
}
