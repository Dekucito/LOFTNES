using UnityEngine;

public class TriggerController : MonoBehaviour
{
    public MessageData messageData; // Asigna el ScriptableObject en el Inspector
    public TextDisplay textDisplay;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            textDisplay.ClearMessages(); // Limpiar mensajes actuales
            textDisplay.AddMessages(messageData.messageTexts);
        }
    }
}
