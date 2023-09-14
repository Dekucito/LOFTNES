using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombateCaC : MonoBehaviour
{
    [SerializeField] private Transform controladorgolpe;

    [SerializeField] public float radioGolpe;
    [SerializeField] internal float dañoGolpe;
    [SerializeField] public float tiempoEntreataque;
    [SerializeField] internal float tiempoSiguienteAtaque;
    [SerializeField] internal bool IsAtacking;

    internal StatsPlayer statsPlayer;
    internal PlayerActions playerActions;
    internal SwordEfects sword;

    internal Animator animator;

    private void Awake()
    {
        statsPlayer = FindAnyObjectByType<StatsPlayer>();
        animator = GetComponent<Animator>();
        playerActions = FindAnyObjectByType<PlayerActions>();
        sword = GetComponentInChildren<SwordEfects>();
    }
    public void Start()
    {
    }
    private void Update()
    {
        if (tiempoSiguienteAtaque > 0)
        {
            tiempoSiguienteAtaque -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Fire1") && tiempoSiguienteAtaque <= 0 && !IsAtacking)
        {
            StartCoroutine(AttackRutine());
            tiempoSiguienteAtaque = tiempoEntreataque;
        }
    }
    public void Golpe()
    {
        Collider2D[] objetos = Physics2D.OverlapCircleAll(controladorgolpe.position, radioGolpe);

        foreach (Collider2D colisionador in objetos)
        {
            if (colisionador.CompareTag("Enemy"))
            {
                dañoGolpe = statsPlayer.Maxdamage;
                colisionador.transform.GetComponent<Limo>().TakeDamage(dañoGolpe, sword.Knockback);
            }
        }
    }
    public IEnumerator AttackRutine()
    {
        IsAtacking = true;

        animator.SetBool("Attack", true);
        playerActions.PlayerCantActions();

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        float animationDuration = stateInfo.length;

        yield return new WaitForSeconds(animationDuration);

        animator.SetBool("Attack", false);
        playerActions.PlayerCanActions();

        IsAtacking = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controladorgolpe.position, radioGolpe);
    }
}
