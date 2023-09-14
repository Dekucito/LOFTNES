using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 3f;

    [Header("Move")]
    internal Rigidbody2D playerRb;
    internal Vector2 moveInput;
    public Animator playerAnimator;

    internal bool playerIsWalking;
    internal bool CanMove;

    internal float moveXValor;
    internal float moveYValor;

    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        CanMove = true;
    }

    void Update()
    {
        if (playerIsWalking)
        {
            InputsStats();
        }
    }
    private void FixedUpdate()
    {
        Movimiento();
    }
    private void Movimiento()
    {
        if (CanMove)
        {
            playerIsWalking = true;
            playerRb.MovePosition(playerRb.position + moveInput * speed * Time.fixedDeltaTime);
        }
    }
    private void InputsStats()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveXValor = moveX;
        moveYValor = moveY;

        moveInput = new Vector2(moveX, moveY).normalized;

        playerAnimator.SetFloat("Speed", moveInput.sqrMagnitude);
        playerAnimator.SetFloat("Horizontal", moveX);
        playerAnimator.SetFloat("Vertical", moveY);

        if (moveX == 0 && moveY == 0)
        {
            playerAnimator.SetBool("Attack", false);
        }
    }
}
