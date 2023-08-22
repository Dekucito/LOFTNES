using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonShopFuctions : MonoBehaviour
{
    public ObjectsDates objectForShop;
    public StatsPlayer playerMoney;
    public PlayerInteractions playerPociones;

    public TMP_Text text;

    private void Start()
    {
        text.text = "precio " + objectForShop.objectPrice;    
    }

    public void ShopFuctions()
    {
        if (playerMoney.currentMoney >= objectForShop.objectPrice)
        {
            switch (objectForShop.type)
            {
                case ObjectsDates.Tipo.life:

                    if (playerPociones.posionLifeCount < playerPociones.maxPotionsCount)
                    {
                        playerMoney.RemoveMoney(objectForShop.objectPrice);
                        playerPociones.posionLifeCount += 1;
                    }
                    break;

                case ObjectsDates.Tipo.damage:

                    if (playerPociones.posionStrenghtCount < playerPociones.maxPotionsCount)
                    {
                        playerMoney.RemoveMoney(objectForShop.objectPrice);
                        playerPociones.posionStrenghtCount += 1;
                    }
                    break;

                case ObjectsDates.Tipo.defense:

                    if (playerPociones.posionDefenseCounts < playerPociones.maxPotionsCount)
                    {
                        playerMoney.RemoveMoney(objectForShop.objectPrice);
                        playerPociones.posionDefenseCounts += 1;
                    }
                    break;
            }
        }
    }
}
