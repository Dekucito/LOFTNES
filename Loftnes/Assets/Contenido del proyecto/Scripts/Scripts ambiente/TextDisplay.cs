using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextDisplay : MonoBehaviour
{
    public TextMeshProUGUI textObject;
    public float messageDuration = 5f;

    public List<string> messages = new List<string>();
    public int currentIndex = 0;
    private Coroutine currentDisplayCoroutine;

    private IEnumerator DisplayMessagesCoroutine()
    {
        while (currentIndex < messages.Count)
        {
            textObject.text = messages[currentIndex];
            yield return new WaitForSeconds(messageDuration);
            currentIndex++;
            textObject.text = "";
        }

        currentIndex = 0;
        ClearMessages();
    }

    public void AddMessages(List<string> newMessages)
    {
        messages.AddRange(newMessages);
        if (currentDisplayCoroutine == null)
        {
            currentDisplayCoroutine = StartCoroutine(DisplayMessagesCoroutine());
        }
    }

    public void ClearMessages()
    {
        messages.Clear();
        currentIndex = 0;
        if (currentDisplayCoroutine != null)
        {
            StopCoroutine(currentDisplayCoroutine);
            currentDisplayCoroutine = null;
        }
    }
}
