using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogos : MonoBehaviour
{
    [Header("text dates")]
    public TextMeshProUGUI dialogueText;
    public string[] lines;

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
    void Start()
    {
        dialogueText.text = string.Empty;

        PanelDialoguesActive = false;
    }
    void Update()
    {
        if (canClick)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (dialogueText.text == lines[index])
                {
                    NextLine();
                }
                else
                {
                    StopAllCoroutines();

                    dialogueText.text = lines[index];
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

        dialogueText.text = string.Empty;

        foreach (char letter in lines[index].ToCharArray())
        {
            dialogueText.text += letter;

            yield return new WaitForSeconds(textSpeed);
        }

        writeEnd = true;
        canClick = false;

    }
    public void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            dialogueText.text = string.Empty;

            StartCoroutine(WriteLine());
        }
        else
        {
            dialogueText.text = string.Empty;

            PanelDialogues.SetActive(false);
            pociones.SetActive(true);

            PanelDialoguesActive = false;
        }
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
