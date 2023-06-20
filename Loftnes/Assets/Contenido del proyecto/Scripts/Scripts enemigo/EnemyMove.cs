using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [Header("Player")]
    private Transform target;

    [Header("enemy")]
    private Animator enemyAnimator;

    [Header("Dates")]
    public float stoppingDisance;
    public float speed;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        enemyAnimator = GetComponent<Animator>();
    }
    private void Update()
    {
        IAMoveToPlayer();   
    }
    public void IAMoveToPlayer()
    {
        if (Vector2.Distance(transform.position, target.position) > stoppingDisance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        /* if (Vector2.Distance(transform.position, target.position) < stoppingDisance)
         {
             transform.position = Vector2.MoveTowards(transform.position, -target.position, speed * Time.deltaTime);
         }*/

    }
}
