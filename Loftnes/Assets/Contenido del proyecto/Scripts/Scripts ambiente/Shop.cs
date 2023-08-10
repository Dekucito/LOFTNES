using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Shop : MonoBehaviour
{
    [Header("text dates")]
    public TextMeshProUGUI dialogueTextDialogues;
    public string[] linesDIalogues;

    public bool writeEnd;
    public bool PanelDialoguesActive;
    public bool canClick;

    public float textSpeed = 0.1f;
    [SerializeField]
    int index;

    public AudioSource audioSource;
    public AudioClip typingSound;

    public GameObject PanelDialogues;
    public GameObject pociones;

    [Header("Player")]
    public PlayerMovement player;
    public PlayerAttackController player_attack;

    [Header("Shop")]
    public GameObject interactText;
    public GameObject panelShop;

    void Start()
    {
        dialogueTextDialogues.text = string.Empty;

        PanelDialoguesActive = false;
    }
    void Update()
    {
        if (index == 3)
        {
            interactText.SetActive(true);

            if (Input.GetKeyUp(KeyCode.E))
            {
                interactText.SetActive(false);

                panelShop.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                dialogueTextDialogues.text = string.Empty;

                PanelDialogues.SetActive(false);
                pociones.SetActive(true);
                interactText.SetActive(false);

                PanelDialoguesActive = false;
            }
        }

        if (canClick)
        {
            if (Input.GetMouseButtonDown(0))
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
       /* if (audioSource != null && typingSound != null)
        {
            PlaySound();
        }*/
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerChangues();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canClick = false;
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canClick = true;

            if (PanelDialoguesActive == false)
            {
                PanelDialoguesActive = true;

                pociones.SetActive(false);

                PanelDialogues.SetActive(true);
                StartDialogueForTrigger();
            }
        }
    }
    public void StartDialogueForTrigger()
    {
        index = 0;

        StartCoroutine(WriteLine());
    }
    public IEnumerator WriteLine()
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
    public void NextLine()
    {
        if (index < linesDIalogues.Length)
        {
            index++;
            dialogueTextDialogues.text = string.Empty;

            StartCoroutine(WriteLine());
        }
       /* else if (index == linesDIalogues.Length)
        {
            interactText.SetActive(true);

            if (Input.GetKeyUp(KeyCode.E))
            {
                interactText.SetActive(false);

                panelShop.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                dialogueTextDialogues.text = string.Empty;

                PanelDialogues.SetActive(false);
                pociones.SetActive(true);
                interactText.SetActive(false);

                PanelDialoguesActive = false;
            }
        }*/
    }
    private void ShopFunctionButtons()
    {
        
    }
    public void PlaySound()
    {
        if (!audioSource.isPlaying && writeEnd == false)
        {
            audioSource.PlayOneShot(typingSound);
        }
        else
        {
            //audioSource.Stop();
        }
    }
    public void PlayerChangues()
    {
        if (PanelDialoguesActive == true)
        {
           player.playerIsWalking = false;
           player.CanMove = false;

           player.playerAnimator.SetFloat("Speed", 0f);

           player_attack.isAttacking = true;
           player_attack.canAttack = false;
        }
        else
        {
           player.playerIsWalking = true;
           player.CanMove = true;

           player_attack.isAttacking = false;
           player_attack.canAttack = true;
        }
    }
}
