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

    public bool gameExist;

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

        CargarDatosForPlay();
        gameExist = datosJuego.GameExist;
    }

    public void PrimerInicioDeJuego(bool theFirstGame)
    {
        if (theFirstGame)
        {
            StartCoroutine(NewGameRutine());
        }
    }

    public void CargarDatos()
    {
        if (File.Exists(archivoGuardado))
        {
            string contenido = File.ReadAllText(archivoGuardado);
            datosJuego = JsonUtility.FromJson<DatosJuegos>(contenido);

            Debug.Log("posicion del jugador: " + datosJuego.theLastPlayerPosition);

            player.GetComponent<StatsPlayer>().maxHealth = datosJuego.theLastPlayerMaxLife;
            player.transform.position = datosJuego.theLastPlayerPosition;
            player.GetComponent<StatsPlayer>().currentMoney = datosJuego.theLastPlayerCurrentMoney;
            player.GetComponent<StatsPlayer>().currentHealth = datosJuego.theLastPlayerCurrentLife;
        }
        else
        {
            Debug.Log("el archivo no existe");
        }
    }

    public void CargarDatosForPlay()
    {
        if (File.Exists(archivoGuardado))
        {
            string contenido = File.ReadAllText(archivoGuardado);
            datosJuego = JsonUtility.FromJson<DatosJuegos>(contenido);
        }
    }

    public void GuardadoDatos()
    {
        DatosJuegos nuevosDatos = new DatosJuegos()
        {
            theLastPlayerMaxLife = player.GetComponent<StatsPlayer>().maxHealth,
            theLastPlayerPosition = player.transform.position,
            theLastPlayerCurrentLife = player.GetComponent<StatsPlayer>().currentHealth,
            theLastPlayerCurrentMoney = player.GetComponent<StatsPlayer>().currentMoney,
            GameExist = true
        };

        string cadenaJSON = JsonUtility.ToJson(nuevosDatos);

        File.WriteAllText(archivoGuardado, cadenaJSON);

        Debug.Log("archivo guardado");
    }

    public void EliminarDatos()
    {
        if (File.Exists(archivoGuardado))
        {
            File.Delete(archivoGuardado);
            Debug.Log("Datos del juego eliminados");
            gameExist = false;
        }
        else
        {
            Debug.Log("No se encontraron datos del juego para eliminar");
        }
    }

    IEnumerator NewGameRutine()
    {
        gameManager.canvasMenus.SetActive(false);
        gameManager.panelConfirmation.SetActive(false);

        gameManager.canvasPlayer.SetActive(true);
        panelLoad.SetActive(true);

        yield return new WaitForSeconds(2f);

        player.GetComponent<StatsPlayer>().startingMoney = 50;
        player.GetComponent<StatsPlayer>().currentMoney = 50;
        player.GetComponent<StatsPlayer>().maxHealth = 100;
        player.GetComponent<StatsPlayer>().currentHealth = player.GetComponent<StatsPlayer>().maxHealth;

        player.transform.position = initial_Position;

        GuardadoDatos();

        yield return new WaitForSeconds(1f);

        panelLoad.SetActive(false);
        player_Actions.PlayerCanActions();

        gameExist = datosJuego.GameExist;
    }

    public IEnumerator LoadGameRutine()
    {
        gameManager.canvasMenus.SetActive(false);
        gameManager.panelConfirmation.SetActive(false);

        gameManager.canvasPlayer.SetActive(true);
        panelLoad.SetActive(true);

        yield return new WaitForSeconds(2f);

        player_Actions.PlayerCantActions();
        CargarDatos();

        yield return new WaitForSeconds(1f);

        panelLoad.SetActive(false);
        player_Actions.PlayerCanActions();

        gameExist = datosJuego.GameExist;
    }
}
