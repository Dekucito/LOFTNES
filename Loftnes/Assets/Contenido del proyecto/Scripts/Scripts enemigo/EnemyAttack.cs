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

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<StatsPlayer>();

        attacking = false;
        impactAttack = false;
    }
    public void AttackPlayer(bool playerinRange)
    {
        Debug.Log("attacking1");

        if (playerinRange && !attacking)
        {
            Debug.Log("attacking2");

            attacking = true;
            StartCoroutine(Atack());
            if (impactAttack)
            {
                player.life = player.life - 1;

                impactAttack = false;
                Debug.Log(player.life);
            }
        }
    }
    IEnumerator Atack()
    {
        //ejecuta animacion de ataque
        animator.SetBool("attack", true);

        yield return new WaitForSeconds(0.5f);//dependiendo de la duracion de la animacion de ataque
        animator.SetBool("attack", false);
        attacking = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            impactAttack = true;
        }
    }
}
