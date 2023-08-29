using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonsForMenu : MonoBehaviour
{
    public ControladorDatosJuego controladorJuego;
    public GameManager gameManager;

    public bool confirmation;

    private void Update()
    {
        if (confirmation)
        {
            gameManager.panelConfirmation.SetActive(true);

            confirmation = false;
        }  
    }

    public void ButtonNewGame(bool isNewGame)
    {
        if (!controladorJuego.gameExist)
        {
            controladorJuego.PrimerInicioDeJuego(isNewGame);
        }
        else
        {
            confirmation = true;
        }
    }

    public void ButtonLoadGame()
    {
        if (controladorJuego.gameExist)
        {
            StartCoroutine(controladorJuego.LoadGameRutine());
        }
    }

    public void ButtonDeleteDatesOfGame()
    {
        controladorJuego.EliminarDatos();
        controladorJuego.gameExist = false;
    }

    public void ButtonConfirm(bool IsNewGame)
    {
        controladorJuego.PrimerInicioDeJuego(IsNewGame);
    }

    public void ButtonBack()
    {
        gameManager.panelConfirmation.SetActive(false);
    }
}
