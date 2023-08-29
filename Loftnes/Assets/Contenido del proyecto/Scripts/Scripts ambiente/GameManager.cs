using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("panel de mejoras")]
    public int upgradesVida;
    public int upgradesDaño;
    public int upgradesDefensa;
    public int upgradesPrice;

    public int numberMaxUpgrades = 20;


    public TMP_Text[] numberText;
    public TMP_Text upgradesRemainingText; // Texto para mostrar las mejoras restantes
    public TMP_Text textoUpgrade;

    [Header("Scripts")]
    public ButtonUpgradeFunctions upgradeStats;
    public StatsPlayer moneyPlayer;
    public PlayerActions player_actions;

    [Header("Objects or canvas")]
    private static GameManager _instance;

    public GameObject canvasPlayer;
    public GameObject canvasMenus;
    public GameObject panelConfirmationNewGame;
    public GameObject panelConfirmationDeleteGame;

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
    }
    public void OkButton()
    {
        int totalUpgradesToApply = upgradesVida + upgradesDaño + upgradesDefensa;
        int totalPriceUpgrades = (upgradesPrice * upgradesVida) + (upgradesPrice * upgradesDefensa) + (upgradesPrice * upgradesDaño);


        if (totalUpgradesToApply > 0 && totalUpgradesToApply <= numberMaxUpgrades && totalPriceUpgrades <= moneyPlayer.currentMoney)
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
        else if (totalPriceUpgrades > moneyPlayer.currentMoney)
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
}

