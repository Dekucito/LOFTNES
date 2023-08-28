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
    public Vector2 initial_Position;

    public GameObject panelLoad;
    public PlayerActions player_Actions;

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
    }

    public void PrimerInicioDeJuego(bool theFirstGame)
    {
        if (theFirstGame)
        {
            StartCoroutine(FirstGameRutine());
        }
    }

    public void CargarDatos()
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

    public void GuardadoDatos()
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

    IEnumerator FirstGameRutine()
    {
        panelLoad.SetActive(true);

        yield return new WaitForSeconds(3f);
        
        player_Actions.PlayerCantActions();

        player.GetComponent<StatsPlayer>().startingMoney = 50;
        player.GetComponent<StatsPlayer>().currentMoney = 50;
        player.GetComponent<StatsPlayer>().maxHealth = 100;
        player.GetComponent<StatsPlayer>().currentHealth = player.GetComponent<StatsPlayer>().maxHealth;

        player.transform.position = initial_Position;

        GuardadoDatos();

        yield return new WaitForSeconds(2f);

        gameManager.canvasPlayer.SetActive(true);
        panelLoad.SetActive(false);
        player_Actions.PlayerCanActions();
    }
}
