using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatsPlayer : MonoBehaviour
{
    [Header("stats")]
    public Image lifeBar;
    internal float maxHealth = 100;
    internal float currentHealth = 1;
    internal float Maxdamage = 15;
    internal float Maxdefending = 25;

    internal bool playerLive;

    [Header("money")]
    internal int startingMoney = 0; // Dinero inicial del jugador
    internal int currentMoney; // Dinero actual del jugador
    public TMP_Text moneyText; // Referencia al componente TextMeshPro para mostrar el dinero

    public GameObject moneyLogo;

    public void Start()
    {
        moneyLogo.SetActive(true);
        UpdateMoneyText();
    }
    private void Update()
    {
        LifeBar();
        UpdateMoneyText();
    }
    private void LifeBar()
    {
        lifeBar.fillAmount = currentHealth / maxHealth;
    }
    public void TakeDamage( float damage)
    {
        Debug.Log(damage);
        currentHealth = currentHealth -= damage;
    }
    public void AddMoney(int amount)
    {
        currentMoney += amount;
        UpdateMoneyText();
    }
    public void RemoveMoney(int amount)
    {
        if (currentMoney >= amount)
        {
            currentMoney -= amount;
            UpdateMoneyText();
        }
        else
        {
            Debug.Log("No tienes suficiente dinero.");
        }
    }
    public void UpdateMoneyText()
    {
        moneyText.text = currentMoney.ToString();
    }

    public void IncreaseHealth(int upgrades)
    {
        maxHealth += 10 * upgrades; // Aumenta la salud por 10 unidades por cada mejora
    }

    public void IncreaseDamage(int upgrades)
    {
        Maxdamage += 5 * upgrades; // Aumenta el daño por 5 unidades por cada mejora
    }

    public void IncreaseDefense(int upgrades)
    {
        Maxdefending += 2 * upgrades; // Aumenta la defensa por 2 unidades por cada mejora
    }
}
