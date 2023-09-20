using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerForPlaces : MonoBehaviour
{
    [Header("Objectts")]
    public GameObject textInteract;
    public GameObject loadPanel;
    internal GameObject player;

    [Header("new dates")]
    public Vector3 newPosition;

    [Header("Scripts")]
    internal PlayerActions playerActions;

    private void Awake()
    {
        playerActions = FindAnyObjectByType<PlayerActions>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Start()
    {
       
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            textInteract.SetActive(true);

            if (Input.GetKey(KeyCode.E))
            {
                StartCoroutine(ChangeOfPlace());
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            textInteract.SetActive(false);
        }
    }
    public IEnumerator ChangeOfPlace()
    {
        playerActions.PlayerCantActions();
        loadPanel.SetActive(true) ;

        player.transform.position = newPosition;

        yield return new WaitForSeconds(2.5f);

        loadPanel.SetActive(false);
        playerActions.PlayerCanActions();
    }
}
