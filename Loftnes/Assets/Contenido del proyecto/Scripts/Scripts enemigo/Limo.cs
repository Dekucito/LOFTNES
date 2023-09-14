using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Limo : MonoBehaviour
{
    [Header("hit")]
    Rigidbody2D rb;

    [Header("Attack")]
    [SerializeField] private Transform controladorgolpe;

    [SerializeField] public float radioGolpe;
    [SerializeField] internal float dañoGolpe;
    [SerializeField] public float tiempoEntreataque;
    [SerializeField] internal float tiempoSiguienteAtaque;
    [SerializeField] internal bool IsAtacking;

    [Header("parametros")]
    public float maxLife;
    internal float actualLife;
    public int moneyToDeath;

    [Header("Follow")]
    public Transform player;
    private NavMeshAgent agent;

    [Header("Animator")]
    internal Animator animator;
    internal int direction;

    [Header("Scripts")]
    public Patrullar patrulla;

    [Header("bool")]
    public bool canWalk;
    internal bool objectivoDectado;
    public bool deadth;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        actualLife = maxLife;
        rb = gameObject.GetComponent<Rigidbody2D>();

        canWalk = true;
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

       AnimationsMove();
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
        if (esDetectado && canWalk && !deadth && player.GetComponent<StatsPlayer>().playerLive)
        {
            agent.SetDestination(player.position);
        }
        else if(canWalk && !deadth)
        {
            patrulla.Move();
        }
    }
    void AnimationsMove()
    {
        animator.SetBool("Walk", true);

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
                if (direction == 3)
                {
                    this.GetComponent<SpriteRenderer>().flipX = true;
                }
                else if (direction != 3)
                {
                    this.GetComponent<SpriteRenderer>().flipX = false;
                }
            }
            /*else
            {
                direction = (movementDirection.y > 0) ? 0 : 2; // Arriba o abajo
            }

            animator.SetInteger("Direction", direction);*/
        }
    }
    public void TakeDamage(float damage, Vector2 KnockBack)
    {
        StartCoroutine(TakeDamageRutine(damage, KnockBack));

        if (actualLife <= 0)
        {
            StartCoroutine(DeathRutine());
        }
    }
    public IEnumerator TakeDamageRutine(float damage, Vector2 knockBack)
    {
        actualLife -= damage;

        rb.AddForce(knockBack);

        animator.SetBool("Damage", true);

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        float animationDuration = stateInfo.length;

        yield return new WaitForSeconds(animationDuration - 0.5f);

        animator.SetBool("Damage", false);
    }
    public IEnumerator DeathRutine()
    {
        animator.SetTrigger("Death");
        animator.SetBool("Walk", false);

        canWalk = false;
        deadth = true;

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        float animationDuration = stateInfo.length;

        yield return new WaitForSeconds(animationDuration - 0.1f);
        player.gameObject.GetComponent<StatsPlayer>().AddMoney(moneyToDeath);

        Destroy(gameObject);
    }
    public void AttackPlayer()
    {
        Collider2D[] objetos = Physics2D.OverlapCircleAll(controladorgolpe.position, radioGolpe);

        foreach (Collider2D colisionador in objetos)
        {
            if (colisionador.CompareTag("Player"))
            {
                colisionador.transform.GetComponent<StatsPlayer>().TakeDamage(dañoGolpe);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controladorgolpe.position, radioGolpe);
    }
}
