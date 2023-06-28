using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IAManager : MonoBehaviour
{
    [Header("parametros")]
    public bool objectivoDectado;
    public bool enemyInRange;

    [Header("Follow")]
    public Transform player;
    private NavMeshAgent agent;

    [Header("Animator")]
    private Animator enemyAnimator;
    private float x;
    private float y;

    [Header("Scripts")]
    public Patrullar patrulla;
    public EnemyAttack atack;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        enemyAnimator = GetComponent<Animator>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }
    private void Update()
    {
        FollowPlayer();

       // Animaciones();
    }
    private void FollowPlayer()
    {
        float distancia = Vector3.Distance(player.position, this.transform.position);

        if (distancia < 3)
        {
            objectivoDectado = true;
        }

        MovimientoEnemy(objectivoDectado);
    }
    private void MovimientoEnemy(bool esDetectado)
    {
        if (esDetectado)
        {
            agent.SetDestination(player.position);
        }
        else
        {
            patrulla.Move();
        }
    }
    void Animaciones()
    {
     
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enemyInRange = true;

            atack.AttackPlayer(enemyInRange);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enemyInRange = true;

            atack.AttackPlayer(enemyInRange);
        }
    }
}
