using UnityEngine;

public class TriggerController : MonoBehaviour
{
    public MessageData messageData; // Asigna el ScriptableObject en el Inspector
    public TextDisplay textDisplay;
    public GameObject textInteract;

    internal bool playerMove;

    PlayerActions playerActions;

    public Type typeTrigger;

    private void Awake()
    {
        playerActions = FindAnyObjectByType<PlayerActions>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            textInteract.SetActive(true);
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.E))
            {
                switch (typeTrigger)
                {
                    case Type.playerCantMove:
                        playerActions.PlayerCantActions();

                        textDisplay.ClearMessages(); // Limpiar mensajes actuales
                        textDisplay.AddMessages(messageData.messageTexts);

                        textDisplay.playerMove = false;
                        textInteract.SetActive(false);
                        break;
                    case Type.playerCanMove:
                        textDisplay.ClearMessages(); // Limpiar mensajes actuales
                        textDisplay.AddMessages(messageData.messageTexts);

                        textInteract.SetActive(false);
                        break;
                    default:
                        break;
                }
            }
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            textInteract.SetActive(false);
        }
    }
    public enum Type
    {
        playerCantMove, playerCanMove
    }
}
