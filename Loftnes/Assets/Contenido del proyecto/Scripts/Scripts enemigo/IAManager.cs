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
    private Animator animator;

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
        animator = GetComponent<Animator>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }
    private void Update()
    {
        FollowPlayer();

        animator.SetInteger("Direction", 1);

        Animaciones();
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
        Vector3 movementDirection = agent.velocity.normalized;

        if (movementDirection != Vector3.zero)
        {
            // Calcular los valores absolutos de las componentes X e Y del vector de dirección
            float horizontal = Mathf.Abs(movementDirection.x);
            float vertical = Mathf.Abs(movementDirection.y);

            // Determinar la dirección basada en los valores absolutos
            int direction;
            if (horizontal > vertical)
            {
                direction = (movementDirection.x > 0) ? 1 : 3; // Derecha o izquierda
            }
            else
            {
                direction = (movementDirection.y > 0) ? 0 : 2; // Arriba o abajo
            }

            animator.SetInteger("Direction", direction);
        }

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
