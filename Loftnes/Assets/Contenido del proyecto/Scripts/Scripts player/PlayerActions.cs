using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [Header("Scripts")]
    public PlayerMovement movementPlayer;
    public CombateCaC playerAttack;

    public bool stayInTriggers = false;
    public GameObject savePointStay;

    public void PlayerCantActions()
    {
        stayInTriggers = true;

        movementPlayer.playerIsWalking = false;
        movementPlayer.CanMove = false;
        playerAttack.IsAtacking = true;

        movementPlayer.playerAnimator.SetFloat("Speed", 0f);
    }

    public void PlayerCanActions()
    {
        stayInTriggers = false;

        movementPlayer.playerIsWalking = true;
        movementPlayer.CanMove = true;
        playerAttack.IsAtacking = false;
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
