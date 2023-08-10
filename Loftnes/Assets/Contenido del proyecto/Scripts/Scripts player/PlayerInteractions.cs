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
    public TMP_Text textNotHavePotions;
    public GameObject textPotions;

    public int potionNumber;
    public int posionLifeCount;
    public int posionStrenghtCount;
    public int posionDefenseCounts;

    public bool presbuttonAndNotPosion;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<StatsPlayer>();
        coolingTimeLife = 0;
    }
    void Update()
    {
        ButtonsComands();

        TextCountPotion();
    }

    private void ButtonsComands()
    {
        if (Input.GetKey(KeyCode.Alpha1) && coolingTimeLife == 0 && posionLifeCount > 0)
        {
            posionLifeCount --;

            coolingTimeLife = 1;
            Debug.Log("pres 1" );
            StartCoroutine(PotionLifeCorrutine());
        }
        else if (Input.GetKey(KeyCode.Alpha1) && posionLifeCount == 0 && !presbuttonAndNotPosion)
        {
            presbuttonAndNotPosion = true;

            potionNumber = 1;
            StartCoroutine(TextPotions()); ;
        }
        if (Input.GetKey(KeyCode.Alpha2) && coolingTimeEffects == 0 && posionStrenghtCount > 0)
        {
            posionStrenghtCount--;

            coolingTimeEffects = 1;
            Debug.Log("pres 2");
            StartCoroutine(PotionDamageCorrutine());
        }
        else if (Input.GetKey(KeyCode.Alpha2) && posionStrenghtCount == 0 && !presbuttonAndNotPosion)
        {
            presbuttonAndNotPosion = true;

            potionNumber = 2;
            StartCoroutine(TextPotions());
        }
        if (Input.GetKey(KeyCode.Alpha3) && coolingTimeEffects == 0 && posionDefenseCounts > 0)
        {
            posionDefenseCounts--;

            coolingTimeEffects = 1;
            Debug.Log("pres 3");
            StartCoroutine(PotioDefendingCorrutine());
        }
        else if (Input.GetKey(KeyCode.Alpha3) && posionDefenseCounts == 0 && !presbuttonAndNotPosion)
        {
            presbuttonAndNotPosion = true;

            potionNumber = 3;
            StartCoroutine(TextPotions());
        }
    }
    IEnumerator TextPotions()
    {
        if (potionNumber == 1)
        {
            textPotions.SetActive(true);

            textNotHavePotions.text = ("te quedaste sin posiones de vida");

            yield return new WaitForSeconds(5f);

            textPotions.SetActive(false);
            presbuttonAndNotPosion = false;
        }
        if (potionNumber == 2)
        {
            textPotions.SetActive(true);

            textNotHavePotions.text = ("te quedaste sin posiones de daño");

            yield return new WaitForSeconds(5f);

            textPotions.SetActive(false);
            presbuttonAndNotPosion = false;
        }
        if (potionNumber == 3)
        {
            textPotions.SetActive(true);

            textNotHavePotions.text = ("te quedaste sin posiones de defensa");

            yield return new WaitForSeconds(5f);

            textPotions.SetActive(false);
            presbuttonAndNotPosion = false;
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
            Debug.Log("No puedes usar una poción de salud, ya que tu vida actual es 0.");
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
    public void TextCountPotion()
    {
        potionText[0].text = posionLifeCount.ToString();
        potionText[1].text = posionStrenghtCount.ToString();
        potionText[2].text = posionDefenseCounts.ToString();
    }
}
