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

    public float textSpeed = 0.1f;
    [SerializeField]
    int index;

    public AudioSource audioSource;
    public AudioClip typingSound; 


    void Start()
    {
        dialogueText.text = string.Empty;

        StartDialogueForTrigger();
    }
    void Update()
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

       /* if (audioSource != null && typingSound != null)
        {
            PlaySound();
        }*/
    }
    public void StartDialogueForTrigger()
    {
        index = 0;

        StartCoroutine(WriteLine());
    }
    public IEnumerator WriteLine()
    {
        writeEnd = false;

        foreach (char letter in lines[index].ToCharArray())
        {
            dialogueText.text += letter;

            yield return new WaitForSeconds(textSpeed);
        }

        writeEnd = true;
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
            gameObject.SetActive(false);
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
}
