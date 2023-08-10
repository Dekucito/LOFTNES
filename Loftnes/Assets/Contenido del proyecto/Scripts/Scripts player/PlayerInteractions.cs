using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInteractions : MonoBehaviour
{
    [Header("Time Effects")]
    public float coolingTimeLife;
    public float coolingTimeEffects;
    StatsPlayer player;

    [Header("Tipes Effects")]
    public int healingValue;
    public int valueStrength;
    public int defensValue;

    [Header("Potion Count")]
    public TMP_Text[] potionText;

    public int posionLifeCount;
    public int posionStrenghtCount;
    public int posionDefenseCounts;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<StatsPlayer>();
        coolingTimeLife = 0;
    }
    void Update()
    {
        ButtonsComands();
    }

    private void ButtonsComands()
    {
        if (Input.GetKey(KeyCode.Alpha1) && coolingTimeLife == 0 && posionLifeCount != 0)
        {
            coolingTimeLife = 1;
            Debug.Log("pres 1" );
            StartCoroutine(PotionLifeCorrutine());
        }
        else
        {
            Debug.Log("Sin posiones de vida");
        }
        if (Input.GetKey(KeyCode.Alpha2) && coolingTimeEffects == 0 && posionStrenghtCount != 0)
        {
            coolingTimeEffects = 1;
            Debug.Log("pres 2");
            StartCoroutine(PotionDamageCorrutine());
        }
        else
        {
            Debug.Log("Sin posiones de da�o");

        }
        if (Input.GetKey(KeyCode.Alpha3) && coolingTimeEffects == 0 && posionDefenseCounts != 0)
        {
            coolingTimeEffects = 1;
            Debug.Log("pres 3");
            StartCoroutine(PotioDefendingCorrutine());
        }
        else
        {
            Debug.Log("Sin posiones de defense");

        }
    }
    IEnumerator PotionLifeCorrutine()
    {
        if (player.currentHealth > 0)
        {
            float potentialHealth = player.currentHealth + healingValue;
            float healingAmount;

            if (potentialHealth > player.maxHealth)
            {
                healingAmount = player.maxHealth - player.currentHealth;
            }
            else
            {
                healingAmount = healingValue;
            }
            if (player.currentHealth == player.maxHealth)
            {
                Debug.Log("Tu vida esta al maximo");
            }

            //activar animacion de curacion
            yield return new WaitForSeconds(0.4f); // depende de la duracion de la animacion de curacion

            player.currentHealth += healingAmount;

            yield return new WaitForSeconds(10);

            coolingTimeLife = 0;
        }
        else
        {
            Debug.Log("No puedes usar una poci�n de salud, ya que tu vida actual es 0.");
        }
    }
    IEnumerator PotionDamageCorrutine()
    {
        //activar animacion de pocion de fuerza
        yield return new WaitForSeconds(0.4f); // depende de la duracion de la animacion de curacion

        player.Maxdamage += valueStrength;

        yield return new WaitForSeconds(2); // depende de la duracion que le quieran dar a el efecto de fuerza

        player.Maxdamage -= valueStrength;

        coolingTimeEffects = 0;
    } 
    IEnumerator PotioDefendingCorrutine()
    {
        //activar animacion de pocion de defensa
        yield return new WaitForSeconds(0.4f); // depende de la duracion de la animacion de curacion

        player.Maxdefending += defensValue;

        yield return new WaitForSeconds(2); // depende de la duracion que le quieran dar a el efecto de fuerza

        player.Maxdefending -= defensValue;

        coolingTimeEffects = 0;
    }
    private void TextCountPotion()
    {
        potionText[0].text = posionLifeCount.ToString();
        potionText[1].text = posionStrenghtCount.ToString();
        potionText[2].text = posionDefenseCounts.ToString();
    }
}
