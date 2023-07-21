using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    public float coolingTimeLife;
    public float coolingTimeEffects;
    StatsPlayer player;

    public int healingValue;
    public int valueStrength;
    public int defensValue;

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
        if (Input.GetKey(KeyCode.Alpha1) && coolingTimeLife == 0)
        {
            coolingTimeLife = 1;
            Debug.Log("pres 1" );
            StartCoroutine(PotionLifeCorrutine());
        }
        if (Input.GetKey(KeyCode.Alpha2) && coolingTimeEffects == 0)
        {
            coolingTimeEffects = 1;
            Debug.Log("pres 2");
            StartCoroutine(PotionDamageCorrutine());
        }
        if (Input.GetKey(KeyCode.Alpha3) && coolingTimeEffects == 0)
        {
            coolingTimeEffects = 1;
            Debug.Log("pres 3");
            StartCoroutine(PotioDefendingCorrutine());
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

        yield return new WaitForSeconds(20); // depende de la duracion que le quieran dar a el efecto de fuerza

        player.Maxdamage -= valueStrength;

        coolingTimeEffects = 0;
    } 
    IEnumerator PotioDefendingCorrutine()
    {
        //activar animacion de pocion de defensa
        yield return new WaitForSeconds(0.4f); // depende de la duracion de la animacion de curacion

        player.Maxdefending += defensValue;

        yield return new WaitForSeconds(20); // depende de la duracion que le quieran dar a el efecto de fuerza

        player.Maxdamage -= defensValue;

        coolingTimeEffects = 0;
    }
}
