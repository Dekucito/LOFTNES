using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonTP : MonoBehaviour
{
    [Header("DatesOfPortals")]
    public PortalManager portal;
    public TMP_Text buttonText;

    [Header("bools")]
    public bool TpIsActive;

    [Header("ParametersForTP")]
    public bool TpTargetACtive;
    Vector2 TpLocation;
    public PlayerActions player_2;

    [Header("Objects")]
    public GameObject loadingPanel;

    private void Start()
    {
        buttonText.text = portal.nameOfPortal;
    }
    private void Update()
    {
        TpIsActive = portal.thisTpIsActive;
    }
    public void OnButtonClick()
    {
        // Obtener los datos del PortalManager
        TpTargetACtive = portal.thisTpIsActive;
        TpLocation = portal.TpLocation;

        TpOfFunction();
    }
    public void TpOfFunction()
    {
        if (TpTargetACtive)
        {
            StartCoroutine(TransitionTeleport());
        }
    }
    private IEnumerator TransitionTeleport()
    {

        player_2.PlayerCantActions();

        // Realizar la transición gradual del jugador al portal objetivo
        float transitionTime = 1.0f;
        float elapsedTime = 0.0f;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector3 startingPosition = player.transform.position;
        Vector3 targetPosition = TpLocation;

        loadingPanel.SetActive(true);

        yield return new WaitForSeconds(5);

        while (elapsedTime <= transitionTime)
        {
            float t = elapsedTime / transitionTime;
            player.transform.position = Vector3.Lerp(startingPosition, targetPosition, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        portal.MapPanel.SetActive(false);
        portal.PanelActive = false;

        player_2.PlayerCanActions();

        loadingPanel.SetActive(false);
    }
}
