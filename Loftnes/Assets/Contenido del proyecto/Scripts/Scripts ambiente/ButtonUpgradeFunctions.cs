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
        gameManager.upgradesDa�o = 0;
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
            case StatType.Da�o:
                gameManager.upgradesDa�o += 1;
                numberText.text = gameManager.upgradesDa�o.ToString();
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
            case StatType.Da�o:
                if (gameManager.upgradesDa�o > 0)
                {
                    gameManager.upgradesDa�o -= 1;
                    numberText.text = gameManager.upgradesDa�o.ToString();
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
            StatsPlayer_object.IncreaseHealth(gameManager.upgradesVida); // L�gica para mejorar la vida del jugador
            StatsPlayer_object.RemoveMoney(gameManager.upgradesPrice * gameManager.upgradesVida);
        }
        if (gameManager.upgradesDa�o > 0)
        {
            StatsPlayer_object.IncreaseDamage(gameManager.upgradesDa�o); // L�gica para mejorar el da�o del jugador
            StatsPlayer_object.RemoveMoney(gameManager.upgradesPrice * gameManager.upgradesDa�o);
        }
        if (gameManager.upgradesDefensa > 0)
        {
            StatsPlayer_object.IncreaseDefense(gameManager.upgradesDefensa); // L�gica para mejorar la defensa del jugador
            StatsPlayer_object.RemoveMoney(gameManager.upgradesPrice * gameManager.upgradesDefensa);
        }
    }

    public enum StatType
    {
        Vida,
        Da�o,
        Defensa
    }
}
