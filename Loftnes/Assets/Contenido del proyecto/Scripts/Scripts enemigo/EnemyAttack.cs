using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header("scripts")]
    StatsPlayer player;

    [Header("componentes")]
    public Animator animator;

    [SerializeField]
    private bool attacking;
    [SerializeField]
    private bool impactAttack;

    public float damageAttack;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<StatsPlayer>();

        attacking = false;
        impactAttack = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            impactAttack = true;
        }
    }
    public void AttackPlayer(bool playerinRange)
    {
        if (playerinRange && !attacking)
        {
            Debug.Log("attacking2");

            attacking = true;
            StartCoroutine(Atack());
            if (impactAttack)
            {
                FinalDamage();

                impactAttack = false;
            }
        }
    }
    private void FinalDamage()
    {
        float damageReduction = 0;

        if (player.Maxdefending >= 18 && player.Maxdefending <= 25)
        {
            damageReduction = damageReduction * 0.3f;
        } 
        if (player.Maxdefending >= 26 && player.Maxdefending <= 50)
        {
            damageReduction = damageReduction * 0.5f;
        } 
        if (player.Maxdefending >= 51 && player.Maxdefending >= 70)
        {
            damageReduction = damageReduction * 0.8f; ;
        }
        else
        {
            damageReduction = 0;
        }
        float damage = damageAttack - damageReduction;

        player.TakeDamage(damage);
    }

    IEnumerator Atack()
    {
        //ejecuta animacion de ataque
        animator.SetBool("attack", true);

        yield return new WaitForSeconds(0.5f);//dependiendo de la duracion de la animacion de ataque
        animator.SetBool("attack", false);
        attacking = false;
    }
}
