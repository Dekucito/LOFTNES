using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PortalManager : MonoBehaviour
{
    [Header("VariablesCambioDeColor")]
    public SpriteRenderer[] sprites;
    public float lerpSpeed;

    private Color curColor;
    private Color targetColor;
    public Color color;

    [Header("scripts")]
    public PlayerMovement player;

    [Header("bools")]
    public bool panelCantActive;
    public bool thisTpIsActive;
    public bool previousDisabled;
    public bool PanelActive;
    public bool EndCorrutine;

    [Header("dates of portals")]
    public string nameOfPortal;
    public Vector2 TpLocation;

    [Header("Tp")]
    public GameObject MapPanel;
    private Coroutine activeCoroutine; // Almacenar la referencia a la corrutina activa
    public GameObject interact;

    public AudioClip[] sonidosTP;
    public AudioSource source { get { return GetComponent<AudioSource>(); } }

    private void Awake()
    {
        TpLocation = this.transform.position;
    }
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        gameObject.AddComponent<AudioSource>();
    }
    private void Update()
    {
        curColor = Color.Lerp(curColor, targetColor, lerpSpeed * Time.deltaTime);

        foreach (var r in sprites)
        {
            r.color = curColor;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (PanelActive)
            {
                PanelActive = false;
            }
            Teleport();
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {

        Debug.Log("ESTA EN EL TRIGGER");
        if (other.CompareTag("Player"))
        {
            player.playerAnimator.SetFloat("Speed", 0f);

            if (EndCorrutine)
            {
                if (panelCantActive)
                {
                    interact.SetActive(true);

                    if (Input.GetKey(KeyCode.E))
                    {
                        MapPanel.SetActive(true);
                        PanelActive = true;
                    }
                }
                if (PanelActive)
                {
                    player.playerAnimator.SetFloat("Speed", 0f);

                    player.playerIsWalking = false;
                    player.CanMove = false;

                    if (Input.GetKey(KeyCode.Escape))
                    {
                        MapPanel.SetActive(false);

                        PanelActive = false;
                    }
                }
                else if (!PanelActive)
                {
                    player.playerIsWalking = true;
                    player.CanMove = true;
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("SALIO DE EL TRIGGER");
        if (other.CompareTag("Player"))
        {
            targetColor = new Color(1, 1, 1, 0);

            if (activeCoroutine != null)
            {
                StopCoroutine(activeCoroutine);
            }
            // Detener la reproducción de sonido
            source.Stop();
            //desactivar el texto de interactuar
            interact.SetActive(false);
        }
    }

    private void Teleport()
    {
        if (thisTpIsActive)
        {
            if (!previousDisabled)
            {
                activeCoroutine = StartCoroutine(RutinePortalEnter()); // Almacenar la referencia a la corrutina activa
            }
        }
        else if (!thisTpIsActive)
        {
            activeCoroutine = StartCoroutine(RutinePortalActive());
        }
           
    }
    private void SpritesAnimationEnter()
    {
        if (thisTpIsActive)
        {
            targetColor = color;
        }
    }
    IEnumerator RutinePortalEnter()
    {
        EndCorrutine = false;

        yield return new WaitForSeconds(0.5f);
        source.PlayOneShot(sonidosTP[0]); // el [0] es de entrada a el portal

        player.playerIsWalking = false;
        player.CanMove = false;

        yield return new WaitForSeconds(1);

        SpritesAnimationEnter();

        yield return new WaitForSeconds(0.1f);

        player.playerIsWalking = true;
        player.CanMove = true;

        panelCantActive = true;
        activeCoroutine = null; // Reiniciar la referencia a la corrutina activa

        EndCorrutine = true;
    }
    IEnumerator RutinePortalActive()
    {
        EndCorrutine = false;

        yield return new WaitForSeconds(0.5f);
        previousDisabled = true;

        player.playerIsWalking = false;
        player.CanMove = false;

        source.PlayOneShot(sonidosTP[1]); // el [1] es de activacion del portal
        yield return new WaitForSeconds(1);

        panelCantActive = true;
        thisTpIsActive = true;

        yield return new WaitForSeconds(0.2f);
           
        SpritesAnimationEnter();
        Teleport();
        previousDisabled = false;

        player.playerIsWalking = true;
        player.CanMove = true;

        activeCoroutine = null; // Reiniciar la referencia a la corrutina activa

        EndCorrutine = true;
    }
}
