using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAPatrol : MonoBehaviour
{
    [Header("Dates")]
    public float speed;
    public int siguientePaso = 0;
    public float distanciaMinima;

    public Transform[] moveSpots;

    private void Start()
    {
    }
    private void Update()
    {
        Patrol();
    }
    private void Patrol()
    {
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[siguientePaso].position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, moveSpots[siguientePaso].position) < distanciaMinima)
        {
             siguientePaso += 1;

            if (siguientePaso >= moveSpots.Length)
            {
                siguientePaso = 0;
            }
        }
    }
}
