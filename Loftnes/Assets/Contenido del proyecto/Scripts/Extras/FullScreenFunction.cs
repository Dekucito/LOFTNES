using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullScreenFunction : MonoBehaviour
{
    private bool pantallaCompleta = true; // Puedes establecer esto como desees en el inspector.

    private void Start()
    {
        // Establece el modo de pantalla al iniciar.
        Screen.fullScreen = pantallaCompleta;
    }

    public void FullScreen(bool fullScreen)
    {
        pantallaCompleta = fullScreen;

        // Cambia el modo de pantalla según el valor actual de pantallaCompleta.
        Screen.fullScreen = pantallaCompleta;
    }
}
