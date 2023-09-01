using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [Header("Scripts")]
    public PlayerMovement movementPlayer;
    public PlayerAttackController playerAttack;

    public bool stayInTriggers = false;
    public GameObject savePointStay;

    public void PlayerCantActions()
    {
        stayInTriggers = true;

        movementPlayer.playerIsWalking = false;
        movementPlayer.CanMove = false;
        playerAttack.isAttacking = true;
        playerAttack.canAttack = false;

        movementPlayer.playerAnimator.SetFloat("Speed", 0f);
    }

    public void PlayerCanActions()
    {
        stayInTriggers = false;

        movementPlayer.playerIsWalking = true;
        movementPlayer.CanMove = true;
        playerAttack.isAttacking = false;
        playerAttack.canAttack = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("SavePoint"))
        {
            savePointStay = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("SavePoint"))
        {
            savePointStay = null;
        }
    }
}
