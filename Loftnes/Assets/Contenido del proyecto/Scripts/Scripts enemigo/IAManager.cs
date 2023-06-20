using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAManager : MonoBehaviour
{
    [Header("parametros")]
    public Rigidbody2D enemyRB;
    public bool enemyInRange;

    private void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        Constaints();
    }
    private void Constaints()
    {
        if (enemyInRange)
        {
            enemyRB.isKinematic = true;
        }
        else
        {
            enemyRB.isKinematic = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enemyInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enemyInRange = false;
        }
    }
}
