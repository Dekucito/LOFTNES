using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsPlayer : MonoBehaviour
{
    [Header("stats")]
    public Image lifeBar;
    public float maxHealth = 100;
    public float currentHealth;
    public float Maxdamage = 15;
    public float Maxdefending = 25;

    private void Update()
    {
        LifeBar();
    }
    private void LifeBar()
    {
        lifeBar.fillAmount = currentHealth / maxHealth;
    }
    public void TakeDamage( float damage)
    {
        currentHealth = currentHealth -= damage;
    }
}
