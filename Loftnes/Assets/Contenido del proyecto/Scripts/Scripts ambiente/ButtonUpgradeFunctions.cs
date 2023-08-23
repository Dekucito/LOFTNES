using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ButtonUpgradeFunctions : MonoBehaviour
{
    public TMP_Text numberText;

    public StatType statToUpgrade;

    public StatsPlayer StatsPlayer_object;
    public GameManager gameManager;


    private void Awake()
    {
        numberText.text = "0";
    }
    public void ButtonBack()
    {
        int textBack = 0;

        gameManager.upgradesVida = 0;
        gameManager.upgradesDaño = 0;
        gameManager.upgradesDefensa = 0;

        numberText.text = textBack.ToString();
    }

    public void ButtonPlus()
    {
        switch (statToUpgrade)
        {
            case StatType.Vida:
                gameManager.upgradesVida += 1;
                numberText.text = gameManager.upgradesVida.ToString();
                break;
            case StatType.Daño:
                gameManager.upgradesDaño += 1;
                numberText.text = gameManager.upgradesDaño.ToString();
                break;
            case StatType.Defensa:
                gameManager.upgradesDefensa += 1;
                numberText.text = gameManager.upgradesDefensa.ToString();
                break;
        }

    }

    public void ButtonLess()
    {
        switch (statToUpgrade)
        {
            case StatType.Vida:
                if (gameManager.upgradesVida > 0)
                {
                    gameManager.upgradesVida -= 1;
                    numberText.text = gameManager.upgradesVida.ToString();
                }
                break;
            case StatType.Daño:
                if (gameManager.upgradesDaño > 0)
                {
                    gameManager.upgradesDaño -= 1;
                    numberText.text = gameManager.upgradesDaño.ToString();
                }
                break;
            case StatType.Defensa:
                if (gameManager.upgradesDefensa > 0)
                {
                    gameManager.upgradesDefensa -= 1;
                    numberText.text = gameManager.upgradesDefensa.ToString();
                }
                break;
        }
    }

    public void UpgradePlayerStat()
    {
        if (gameManager.upgradesVida > 0)
        {
            StatsPlayer_object.IncreaseHealth(gameManager.upgradesVida); // Lógica para mejorar la vida del jugador
            StatsPlayer_object.RemoveMoney(gameManager.upgradesPrice * gameManager.upgradesVida);
        }
        if (gameManager.upgradesDaño > 0)
        {
            StatsPlayer_object.IncreaseDamage(gameManager.upgradesDaño); // Lógica para mejorar el daño del jugador
            StatsPlayer_object.RemoveMoney(gameManager.upgradesPrice * gameManager.upgradesDaño);
        }
        if (gameManager.upgradesDefensa > 0)
        {
            StatsPlayer_object.IncreaseDefense(gameManager.upgradesDefensa); // Lógica para mejorar la defensa del jugador
            StatsPlayer_object.RemoveMoney(gameManager.upgradesPrice * gameManager.upgradesDefensa);
        }
    }

    public enum StatType
    {
        Vida,
        Daño,
        Defensa
    }
}
