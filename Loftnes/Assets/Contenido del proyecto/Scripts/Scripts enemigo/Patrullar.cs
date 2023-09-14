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
    public NavMeshAgent agent;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }
    public void Move() // funcion para que el enemigo se mueva entre unos puntos predeterminados creando una ronda de patrullaje
    {
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
