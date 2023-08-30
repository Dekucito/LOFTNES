using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoints : MonoBehaviour
{
    [Header("Scripts")]
    public ControladorDatosJuego controladorDatosJuego;
    public PlayerActions playerActions;

    [Header("properties")]
    public Animator animation_SavePoint;
    public GameObject textSave;


    public void Start()
    {
        animation_SavePoint = GetComponentInChildren <Animator> ();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            textSave.SetActive(true);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.E))
            {
                StartCoroutine(CorrutineSave());
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            textSave.SetActive(false);
        }
    }
    public IEnumerator CorrutineSave()
    {
        playerActions.PlayerCantActions();
        textSave.SetActive(false);

        yield return new WaitForSeconds(0.2f);

        animation_SavePoint.SetBool("PlayerSave", true);
        AnimatorStateInfo stateInfo = animation_SavePoint.GetCurrentAnimatorStateInfo(0);
        float animationDuration = stateInfo.length;

        yield return new WaitForSeconds(animationDuration);

        animation_SavePoint.SetBool("PlayerSave", false);
        controladorDatosJuego.GuardadoDatos();

        yield return new WaitForSeconds(0.5f);

        playerActions.PlayerCanActions();
    }
}
