using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header("scripts")]
    StatsPlayer player;
    public IAManager IA;

    [Header("componentes")]
    public Animator animator;

    public Transform player_Position; // Asigna el GameObject del jugador en el Inspector
    public float distanciaMinima = 2f; // Distancia m�nima para iniciar el ataque

    [SerializeField]
    private bool impactAttack;
    public bool canAttack;

    private bool isAttacking;
    public bool attackStarted; // Variable para verificar si ya se inició un ataque

    public float damageAttack;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<StatsPlayer>();

        impactAttack = false;
    }
    private void Update()
    {
        if (impactAttack && attackStarted)
        {
            FinalDamage();

            impactAttack = false;
        }

        float distanciaAlJugador = Vector2.Distance(IA.transform.position, player_Position.position);

        if (distanciaAlJugador < distanciaMinima)
        {
            canAttack = true;
       
            if (!isAttacking && !attackStarted) // Verificamos si el enemigo no está atacando actualmente y si el ataque no ha sido iniciado
            {
                AttackPlayer(canAttack, IA.direction);
            }
        }
        else
        {
            canAttack = false;
            StopCoroutine(RutineAttack());
            attackStarted = false; // Restablecemos attackStarted si el jugador está fuera del rango

            animator.SetInteger("attack", 4);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            impactAttack = true;
        }
    }
    public void AttackPlayer(bool playerInRange, int animation)
    {
        if (playerInRange && !isAttacking)
        {
            Debug.Log("attacking");
            isAttacking = true; // Establecemos isAttacking a true para evitar que se inicie una nueva corrutina de ataque

            StartCoroutine(RutineAttack());
        }
    }
    public IEnumerator RutineAttack()
    {
        attackStarted = true; // Indicamos que se ha iniciado un ataque

        animator.SetInteger("attack", IA.direction);

        yield return new WaitForSeconds(0.2f);

        // Esperar la duración de la animación de ataque antes de restablecer isAttacking a false
        yield return new WaitForSeconds(0.5f); // Reemplaza 'animacionDuracion' con la duración real de la animación de ataque

        // Restablecer isAttacking a false después de completar la animación de ataque
        isAttacking = false;
        attackStarted = false; // Restablecemos attackStarted para que un nuevo ataque pueda ser iniciado en el futuro
    }
    private void FinalDamage()
    {
        float damageReduction = 0;

        if (player.Maxdefending >= 18 && player.Maxdefending <= 25)
        {
            damageReduction = damageAttack * 0.3f;
        } 
        if (player.Maxdefending >= 26 && player.Maxdefending <= 50)
        {
            damageReduction = damageAttack * 0.5f;
        } 
        if (player.Maxdefending >= 51 && player.Maxdefending >= 70)
        {
            damageReduction = damageAttack * 0.8f; ;
        }
        Debug.Log("reuccion del daño es igual a =" + damageReduction);
        float damage = damageAttack - damageReduction;

        player.TakeDamage(damage);
    }
}
