using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System;

public class GameManager : MonoBehaviour
{
    [Header("panel de mejoras")]
    public int upgradesVida;
    public int upgradesDaño;
    public int upgradesDefensa;
    public int upgradesPrice;

    public int numberMaxUpgrades;


    public TMP_Text[] numberText;
    public TMP_Text upgradesRemainingText; // Texto para mostrar las mejoras restantes
    public TMP_Text textoUpgrade;

    [Header("Scripts")]
    public ButtonUpgradeFunctions upgradeStats;
    public StatsPlayer statsPlayers;
    public PlayerActions player_actions;
    public ControladorDatosJuego controladorJuego;

    internal static GameManager _instance;
    
    [Header("CanvasObjects")]
    public GameObject canvasPlayer;
    public GameObject canvasMenus;
    public GameObject panelConfirmationNewGame;
    public GameObject panelConfirmationDeleteGame;

    [Header("Pause")]
    [SerializeField]
    internal bool isPaused = false;
    public GameObject pauseMenu;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();

                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject("MySingleton");
                    _instance = singletonObject.AddComponent<GameManager>();
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        upgradesRemainingText.text = "Upgrades Remaining: " + numberMaxUpgrades.ToString();

        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            if (this != _instance)
            {
                Destroy(this.gameObject);
            }
        }
    }
    private void Start()
    {
        canvasPlayer.SetActive(false);
        player_actions.PlayerCantActions();
    }
    private void Update()
    {
        upgradesRemainingText.text = "Upgrades Remaining: " + numberMaxUpgrades.ToString();

        upgradeStats = FindObjectOfType<ButtonUpgradeFunctions>();

        if (Input.GetKeyDown(KeyCode.Escape) && !player_actions.stayInTriggers)
        {
            if (!isPaused)
            {
                PauseGame();
                Debug.Log("Pausa");

                player_actions.stayInTriggers = false;
            }
            else if (isPaused)
            {
                ResumeGame();
                Debug.Log("despausa");
            }
        }
        if (statsPlayers.currentHealth <= 0)
        {
            StartCoroutine(DeadFunctionRutine());
        }
    }
    public void OkButton()
    {
        int totalUpgradesToApply = upgradesVida + upgradesDaño + upgradesDefensa;
        int totalPriceUpgrades = (upgradesPrice * upgradesVida) + (upgradesPrice * upgradesDefensa) + (upgradesPrice * upgradesDaño);


        if (totalUpgradesToApply > 0 && totalUpgradesToApply <= numberMaxUpgrades && totalPriceUpgrades <= statsPlayers.currentMoney)
        {
            upgradeStats.UpgradePlayerStat();

            numberMaxUpgrades -= totalUpgradesToApply;

            upgradesRemainingText.text = "Upgrades Remaining: " + numberMaxUpgrades.ToString();

            for (int i = 0; i < numberText.Length; i++)
            {
                upgradesDaño = 0;
                upgradesDefensa = 0;
                upgradesVida = 0;

                numberText[i].text = "0";
            }

            textoUpgrade.text = "MEJORAS HECHAS";
            StartCoroutine(TextUpgradeRuttine());

        }
        else if (totalPriceUpgrades > statsPlayers.currentMoney)
        {
            textoUpgrade.text = "NO TIENES SUFICIENTE DINERO";
            StartCoroutine(TextUpgradeRuttine());

            for (int i = 0; i < numberText.Length; i++)
            {
                upgradesDaño = 0;
                upgradesDefensa = 0;
                upgradesVida = 0;

                numberText[i].text = "0";
            }
        }
        else
        {
            if (numberMaxUpgrades == 0)
            {
                textoUpgrade.text = "NO PUEDES HACER MAS MEJORAS";
                StartCoroutine(TextUpgradeRuttine());
            }
            else if (totalUpgradesToApply > numberMaxUpgrades && numberMaxUpgrades > 0)
            {
                textoUpgrade.text = "NO PUEDES HACER ESA CANTIDAD DE MEJORAS";
                StartCoroutine(TextUpgradeRuttine());
            }

        }
    }
    public IEnumerator TextUpgradeRuttine()
    {
        yield return new WaitForSeconds(1);
        textoUpgrade.text = "";
    }
    public void PauseGame()
    {
        player_actions.PlayerCantActions();

        Time.timeScale = 0f; // Pausar el tiempo del juego
        isPaused = true;
        pauseMenu.SetActive(true);
    }
    public void ResumeGame()
    {
        player_actions.PlayerCanActions();

        Time.timeScale = 1f; // Reanudar el tiempo del juego
        isPaused = false;
        pauseMenu.SetActive(false);
    }
    public void ExitTheGame()
    {
        player_actions.PlayerCantActions();

        Time.timeScale = 1f; // Reanudar el tiempo del juego
        isPaused = false;
        pauseMenu.SetActive(false);
        canvasMenus.SetActive(true);
    }
    public void CargarDatos()
    {
        if (File.Exists(controladorJuego.archivoGuardado))
        {
            Time.timeScale = 1f; // Reanudar el tiempo del juego
            isPaused = false;
            pauseMenu.SetActive(false);

            StartCoroutine(controladorJuego.LoadGameRutine());
        }
    }
    internal IEnumerator DeadFunctionRutine()
    {
        player_actions.movementPlayer.playerAnimator.SetBool("Dead", true);
        player_actions.PlayerCantActions();

        yield return new WaitForSeconds(2f);

        player_actions.movementPlayer.playerAnimator.SetBool("Dead", false);

        controladorJuego.LoadGameRutine();
    }
}

