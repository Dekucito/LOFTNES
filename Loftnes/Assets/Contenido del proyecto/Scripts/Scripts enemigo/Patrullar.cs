using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrullar : MonoBehaviour
{
    [Header("patrullar")]
    [SerializeField]
    public float velocidadmovimiento;
    [SerializeField]
    private Transform[] puntosmovimiento;
    [SerializeField]
    private float distanciaminima;
    private int Siguientepaso;

    [Header("Follow")]
    private NavMeshAgent agent;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }
    void Update()
    {

    }
    public void Move()
    {
        //transform.position = Vector2.MoveTowards(transform.position, puntosmovimiento[Siguientepaso].position, velocidadmovimiento * Time.deltaTime);
        agent.SetDestination(puntosmovimiento[Siguientepaso].position);

        if (Vector2.Distance(transform.position, puntosmovimiento[Siguientepaso].position) < distanciaminima)
        {
            Siguientepaso += 1;
            if (Siguientepaso >= puntosmovimiento.Length)
            {
                Siguientepaso = 0;
            }
        }
    }

}
