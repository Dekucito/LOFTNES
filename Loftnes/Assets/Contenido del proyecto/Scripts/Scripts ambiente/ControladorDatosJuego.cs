using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class ControladorDatosJuego : MonoBehaviour
{
    public GameObject player;
    public string archivoGuardado;
    public GameManager gameManager;

    public DatosJuegos datosJuego = new DatosJuegos();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            CargarDatos();
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            GuardadoDatos();
        }
    }

    private void Awake()
    {
        archivoGuardado = Application.dataPath + "/datosJuego.Json";

        player = GameObject.FindGameObjectWithTag("Player");

        PrimerInicioDeJuego();
    }

    private void PrimerInicioDeJuego()
    {
        if (gameManager.isTheFirstGame)
        {
            player.transform.position = new Vector3(17,-2, 2);
            player.GetComponent<StatsPlayer>().startingMoney = 100;
            player.GetComponent<StatsPlayer>().maxHealth = 100;
            player.GetComponent<StatsPlayer>().currentHealth = player.GetComponent<StatsPlayer>().maxHealth;

            gameManager.isTheFirstGame = false;
        }
    }

    private void CargarDatos()
    {
        if (File.Exists(archivoGuardado))
        {
            string contenido = File.ReadAllText(archivoGuardado);
            datosJuego = JsonUtility.FromJson<DatosJuegos>(contenido);

            Debug.Log("posicion del jugador: " + datosJuego.posicion);

            player.transform.position = datosJuego.posicion;
            player.GetComponent<StatsPlayer>().currentMoney = datosJuego.dinero;
            player.GetComponent<StatsPlayer>().currentHealth = datosJuego.vida;
        }
        else
        {
            Debug.Log("el archivo no existe");
        }
    }

    private void GuardadoDatos()
    {
        DatosJuegos nuevosDatos = new DatosJuegos()
        {
            posicion = player.transform.position,
            vida = player.GetComponent<StatsPlayer>().currentHealth,
            dinero = player.GetComponent<StatsPlayer>().currentMoney
        };

        string cadenaJSON = JsonUtility.ToJson(nuevosDatos);

        File.WriteAllText(archivoGuardado, cadenaJSON);

        Debug.Log("archivo guardado");
    }
}
