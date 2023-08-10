using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IAManager : MonoBehaviour
{
    [Header("parametros")]
    public bool objectivoDectado;

    [Header("Follow")]
    public Transform player;
    private NavMeshAgent agent;

    [Header("Animator")]
    private Animator animator;
    public int direction;

    [Header("Scripts")]
    public Patrullar patrulla;
    public EnemyAttack attack;

    [Header("attack")]
    public GameObject arma;
    public Transform[] armaPositionForDirection;
    [Header("bool")]
    public bool armaActive;

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

       //animator.SetInteger("Direction", 1);

       AnimationsMove();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            armaActive = true;
            //animacion de sacar arma
            arma.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

        }
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
    void AnimationsMove()
    {
        Vector3 movementDirection = agent.velocity.normalized;

        if (movementDirection != Vector3.zero)
        {
            // Calcular los valores absolutos de las componentes X e Y del vector de dirección
            float horizontal = Mathf.Abs(movementDirection.x);
            float vertical = Mathf.Abs(movementDirection.y);

            // Determinar la dirección basada en los valores absolutos
            if (horizontal > vertical)
            {
                direction = (movementDirection.x > 0) ? 1 : 3; // Derecha o izquierda
                ArmaActive(direction);
            }
            else
            {
                direction = (movementDirection.y > 0) ? 0 : 2; // Arriba o abajo
                ArmaActive(direction);
            }

            animator.SetInteger("Direction", direction);
        }

    }
    private void ArmaActive(int armaGameobjectPosition)
    {
        if (armaActive)
        {
            arma.transform.parent = armaPositionForDirection[armaGameobjectPosition];
        }
    }
}
