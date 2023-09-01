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

                    if (playerPociones.PotionLifeCount < playerPociones.maxPotionsCount)
                    {
                        playerMoney.RemoveMoney(objectForShop.objectPrice);
                        playerPociones.PotionLifeCount += 1;
                    }
                    break;

                case ObjectsDates.Tipo.damage:

                    if (playerPociones.PotionDamageCount < playerPociones.maxPotionsCount)
                    {
                        playerMoney.RemoveMoney(objectForShop.objectPrice);
                        playerPociones.PotionDamageCount += 1;
                    }
                    break;

                case ObjectsDates.Tipo.defense:

                    if (playerPociones.PotionDefenseCount < playerPociones.maxPotionsCount)
                    {
                        playerMoney.RemoveMoney(objectForShop.objectPrice);
                        playerPociones.PotionDefenseCount += 1;
                    }
                    break;
            }
        }
    }
}
