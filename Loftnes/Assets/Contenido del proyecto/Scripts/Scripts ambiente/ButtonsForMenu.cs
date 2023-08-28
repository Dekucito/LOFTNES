using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonsForMenu : MonoBehaviour
{
    public ControladorDatosJuego controladorJuego;

    public bool gameExist = false;
    
    public void ButtonNewGame(bool isNewGame)
    {
        controladorJuego.PrimerInicioDeJuego(isNewGame);

        gameExist = true;
    }

    public void ButtonLoadGame()
    {
        if (gameExist)
        {
            ChangueScene(sceneInt);
            controladorJuego.CargarDatos();
        }
    }
    public void ChangueScene(int indexOfScene)
    {
      
    }
    //public IEnumerator NewGameRutine()
    // {

    //}
}
