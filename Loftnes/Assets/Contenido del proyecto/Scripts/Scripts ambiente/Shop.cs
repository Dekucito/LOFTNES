using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Shop : MonoBehaviour
{
    [Header("Text dates")]
    public TextMeshProUGUI dialogueTextDialogues;
    public string[] linesDIalogues;
    public float textSpeed = 0.1f;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip typingSound;

    [Header("UI Elements")]
    public GameObject PanelDialogues;
    public GameObject pociones;
    public GameObject interactText;
    public GameObject panelShop;

    [Header("Player")]
    public PlayerActions playerActions;


    // Private variables to track state
    public bool writeEnd;
    public int index;
    public bool interactShop;
    public bool shopFunctions;
    public bool canClick;
    public bool PanelDialoguesActive;
    public bool pressEscForSalir;

    private void Start()
    {
        ResetDialogueState();
    }

    private void Update()
    {
        HandleShopFunctions();
        HandleDialogueInteraction();
        HandlePlayerChanges();
    }

    private void ResetDialogueState()
    {
        dialogueTextDialogues.text = string.Empty;
        writeEnd = false;
        index = 0;
        interactShop = false;
        shopFunctions = false;
        canClick = false;
        PanelDialoguesActive = false;
    }

    private void HandleShopFunctions()
    {
        if (shopFunctions)
        {
            HandleShopInteraction();
        }
    }

    private void HandleShopInteraction()
    {
        if (!interactShop)
        {
            dialogueTextDialogues.text = string.Empty;
            interactText.SetActive(true);
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            interactShop = true;
            canClick = false;
            panelShop.SetActive(true);
            interactText.SetActive(false);
            PanelDialogues.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitShop();
            pressEscForSalir = true;
        }
    }

    private void ExitShop()
    {
        PanelDialogues.SetActive(false);
        interactText.SetActive(false);
        panelShop.SetActive(false);
        pociones.SetActive(true);

        PanelDialoguesActive = false;
        interactShop = false;
        shopFunctions = false;
    }

    private void HandleDialogueInteraction()
    {
        if (canClick && Input.GetMouseButtonDown(0))
        {
            if (dialogueTextDialogues.text == linesDIalogues[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                dialogueTextDialogues.text = linesDIalogues[index];
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartDialogue();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canClick = false;
        }
    }

    private void StartDialogue()
    {
        canClick = true;

        if (!PanelDialoguesActive)
        {
            PanelDialoguesActive = true;
            pociones.SetActive(false);
            PanelDialogues.SetActive(true);
            StartDialogueForTrigger();
        }
    }

    private void StartDialogueForTrigger()
    {
        index = 0;
        StartCoroutine(WriteLine());
    }

    private IEnumerator WriteLine()
    {
        writeEnd = false;
        dialogueTextDialogues.text = string.Empty;

        foreach (char letter in linesDIalogues[index].ToCharArray())
        {
            dialogueTextDialogues.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }

        writeEnd = true;
    }

    private void NextLine()
    {
        if (index < linesDIalogues.Length - 1)
        {
            shopFunctions = false;
            index++;
            dialogueTextDialogues.text = string.Empty;
            StartCoroutine(WriteLine());
        }
        else
        {
            shopFunctions = true;
        }
    }

    private void PlaySound()
    {
        if (!audioSource.isPlaying && !writeEnd)
        {
            audioSource.PlayOneShot(typingSound);
        }
        else
        {
            //audioSource.Stop();
        }
    }

    private void HandlePlayerChanges()
    {
        if (PanelDialoguesActive)
        {
            playerActions.PlayerCantActions();  
        }
        else if (pressEscForSalir)
        {
            playerActions.PlayerCanActions();  

            pressEscForSalir = false;
        }
    }
}