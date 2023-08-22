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
    public float maxHealth = 100;
    public float currentHealth;
    public float Maxdamage = 15;
    public float Maxdefending = 25;

    [Header("money")]
    public int startingMoney = 0; // Dinero inicial del jugador
    public int currentMoney; // Dinero actual del jugador
    public TMP_Text moneyText; // Referencia al componente TextMeshPro para mostrar el dinero

    public GameObject moneyLogo;

    public void Start()
    {
        moneyLogo.SetActive(true);

        currentMoney = startingMoney;
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
}
