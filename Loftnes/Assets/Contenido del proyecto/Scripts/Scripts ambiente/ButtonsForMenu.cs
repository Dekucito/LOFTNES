using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.SceneManagement;
public class ButtonsForMenu : MonoBehaviour
{
    [Header("info")]
    public ControladorDatosJuego controladorJuego;
    public GameManager gameManager;

    [Header("Bools")]
    internal bool confirmation;
    internal bool eliminationConfirm;

    private void Update()
    {
        if (confirmation)
        {
            gameManager.panelConfirmationNewGame.SetActive(true);

            confirmation = false;
        }
        if (eliminationConfirm)
        {
            gameManager.panelConfirmationDeleteGame.SetActive(true);

            eliminationConfirm = false;
        }
    }

    public void ButtonNewGame(bool isNewGame)
    {
        if (!File.Exists(controladorJuego.archivoGuardado))
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
        if (File.Exists(controladorJuego.archivoGuardado))
        {
            StartCoroutine(controladorJuego.LoadGameRutine());
        }
    }

    public void ButtonDeleteDatesOfGame()
    {
        if (File.Exists(controladorJuego.archivoGuardado))
        {
            eliminationConfirm = true;
        }
    }

    public void ButtonConfirm(bool IsNewGame)
    {
        controladorJuego.PrimerInicioDeJuego(IsNewGame);
    }

    public void ButtonBack()
    {
        gameManager.panelConfirmationNewGame.SetActive(false);
    }
    public void ButtonConfirmDelete()
    {
        gameManager.panelConfirmationDeleteGame.SetActive(false);
        controladorJuego.EliminarDatos();
    }

    public void ButtonBackDelete()
    {
        gameManager.panelConfirmationDeleteGame.SetActive(false);
    }
}
