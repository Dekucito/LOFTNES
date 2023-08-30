using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [Header("Scripts")]
    public PlayerMovement movementPlayer;
    public PlayerAttackController playerAttack;

    public bool stayTriggerShop;

    public void PlayerCantActions()
    {
        movementPlayer.playerIsWalking = false;
        movementPlayer.CanMove = false;
        playerAttack.isAttacking = true;
        playerAttack.canAttack = false;

        movementPlayer.playerAnimator.SetFloat("Speed", 0f);
    }

    public void PlayerCanActions()
    {
        movementPlayer.playerIsWalking = true;
        movementPlayer.CanMove = true;
        playerAttack.isAttacking = false;
        playerAttack.canAttack = true;
    }

    public void PausedFunction()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

        }
    }
}
