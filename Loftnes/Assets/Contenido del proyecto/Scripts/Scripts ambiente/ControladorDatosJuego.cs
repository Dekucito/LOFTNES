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
    public Vector3 initial_Position;

    public GameObject panelLoad;
    public PlayerActions player_Actions;

    public DatosJuegos datosJuego = new DatosJuegos();

    private void Awake()
    {
        archivoGuardado = Application.dataPath + "/datosJuego.Json";
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

            player.transform.position = datosJuego.theLastPlayerPosition;

            player.GetComponent<StatsPlayer>().maxHealth = datosJuego.theLastPlayerMaxLife;
            player.GetComponent<StatsPlayer>().currentMoney = datosJuego.theLastPlayerCurrentMoney;
            player.GetComponent<StatsPlayer>().currentHealth = datosJuego.theLastPlayerCurrentLife;

            player.GetComponent<PlayerInteractions>().PotionLifeCount = datosJuego.theLastCountPotionLife;
            player.GetComponent<PlayerInteractions>().PotionDamageCount = datosJuego.theLastCountPotionDamage;
            player.GetComponent<PlayerInteractions>().PotionDefenseCount = datosJuego.theLastCountPotionDefense;

            player.GetComponent<StatsPlayer>().playerLive = true;

            gameManager.numberMaxUpgrades = datosJuego.theLastPlayerMaxUpgrades;
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
            theLastPlayerPosition = player.transform.position,

            theLastPlayerMaxLife = player.GetComponent<StatsPlayer>().maxHealth,
            theLastPlayerCurrentLife = player.GetComponent<StatsPlayer>().currentHealth,
            theLastPlayerCurrentMoney = player.GetComponent<StatsPlayer>().currentMoney,

            theLastCountPotionLife = player.GetComponent<PlayerInteractions>().PotionLifeCount,
            theLastCountPotionDamage = player.GetComponent<PlayerInteractions>().PotionDamageCount,
            theLastCountPotionDefense = player.GetComponent<PlayerInteractions>().PotionDefenseCount,

            theLastPlayerMaxUpgrades = gameManager.numberMaxUpgrades,
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
        }
        else
        {
            Debug.Log("No se encontraron datos del juego para eliminar");
        }
    }
    IEnumerator NewGameRutine()
    {
        gameManager.canvasMenus.SetActive(false);
        gameManager.panelConfirmationNewGame.SetActive(false);

        gameManager.canvasPlayer.SetActive(true);
        panelLoad.SetActive(true);

        yield return new WaitForSeconds(2f);

        player.GetComponent<StatsPlayer>().startingMoney = 50;
        player.GetComponent<StatsPlayer>().currentMoney = 50;
        player.GetComponent<StatsPlayer>().maxHealth = 100;
        player.GetComponent<StatsPlayer>().currentHealth = player.GetComponent<StatsPlayer>().maxHealth;
        player.GetComponent<StatsPlayer>().playerLive = true;

        player.transform.position = initial_Position;

        player.GetComponent<PlayerInteractions>().PotionLifeCount = 1;
        player.GetComponent<PlayerInteractions>().PotionDamageCount = 1;
        player.GetComponent<PlayerInteractions>().PotionDefenseCount = 1;

        gameManager.numberMaxUpgrades = 25;

        EliminarDatos();

        yield return new WaitForSeconds(1f);

        panelLoad.SetActive(false);
        player_Actions.PlayerCanActions();

        gameManager.player_actions.stayInTriggers = false;
    }
    public IEnumerator LoadGameRutine()
    {
        gameManager.player_actions.stayInTriggers = true;

        gameManager.canvasMenus.SetActive(false);
        gameManager.panelConfirmationNewGame.SetActive(false);

        gameManager.canvasPlayer.SetActive(true);
        panelLoad.SetActive(true);

        yield return new WaitForSeconds(2f);

        player_Actions.PlayerCantActions();
        CargarDatos();

        yield return new WaitForSeconds(0.5f);

        gameManager.player_actions.savePointStay.GetComponent<SavePoints>().animation_SavePoint.SetBool("PlayerSave", true);

        yield return new WaitForSeconds(0.5f);

        panelLoad.SetActive(false);

        AnimatorStateInfo stateInfo = gameManager.player_actions.savePointStay.GetComponent<SavePoints>().animation_SavePoint.GetCurrentAnimatorStateInfo(0);
        float animationDuration = stateInfo.length;

        yield return new WaitForSeconds(animationDuration);

        gameManager.player_actions.savePointStay.GetComponent<SavePoints>().animation_SavePoint.SetBool("PlayerSave", false);
        player_Actions.PlayerCanActions();

        gameManager.player_actions.stayInTriggers = false;
    }
}