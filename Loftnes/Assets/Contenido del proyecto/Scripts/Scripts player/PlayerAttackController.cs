using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    [Header("for animation")]
    public Animator PlayerAnimator;

    public bool isAttacking;
    public bool canAttack = true;
    public bool impactAttack;

    public PlayerMovement player;
    public GameObject armaPositionLuegoDeAnimacion;

    private void Start()
    {
        PlayerAnimator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isAttacking && canAttack)
        {
           StartCoroutine(AnimationsAttack(player.moveXValor, player.moveYValor));
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            impactAttack = true;
        }
    }
    public IEnumerator AnimationsAttack(float horizontal, float vertical)
    {
        // Establecer el bool 'isAttacking' a verdadero
        isAttacking = true;
        canAttack = false;

        // Ejecutar la animación de ataque
        PlayerAnimator.SetBool("Attacking", isAttacking);

       PlayerAnimator.SetFloat("Horizontal", horizontal);
       PlayerAnimator.SetFloat("Vertical", vertical);

        player.playerIsWalking = false;
        player.CanMove = false;

        AnimatorStateInfo stateInfo = PlayerAnimator.GetCurrentAnimatorStateInfo(0);
        float animationDuration = stateInfo.length;

        yield return new WaitForSeconds(animationDuration); // depende de la duracion de la animacion

        isAttacking = false;

        PlayerAnimator.SetBool("Attacking", isAttacking);

        player.playerIsWalking = true;
        player.CanMove = true;
        canAttack = true;

        // Ejecutar cualquier otra lógica o acciones relacionadas con el ataque aquí
    }
}
