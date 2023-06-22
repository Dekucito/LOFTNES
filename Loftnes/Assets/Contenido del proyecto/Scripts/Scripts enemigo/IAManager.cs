using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IAManager : MonoBehaviour
{
    [Header("parametros")]
    public bool enemyInRange;

    [Header("Follow")]
    public Transform player;
    private NavMeshAgent agent;
    private Animator enemyAnimator;
    public float x;
    public float y;

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
        Animaciones();
    }
    private void FollowPlayer()
    {
        agent.SetDestination(player.position);
    }
    void Animaciones()
    {
        x = ((float)transform.position.x);
        y = ((float)transform.position.y);

        x = Mathf.Clamp(x, -1f, 1f);
        y = Mathf.Clamp(y, -1f, 1f);

        enemyAnimator.SetFloat("Horizontal", x);
        enemyAnimator.SetFloat("Vertical", y);
        enemyAnimator.SetBool("walk", true);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
       
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
       
    }
}
