using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DatosJuegos
{
    [Header("Ultimos datos ingresados ")]

    public Vector3 theLastPlayerPosition;

    public float theLastPlayerCurrentLife;
    public float theLastPlayerMaxLife;

    public int theLastPlayerCurrentMoney;

    public int theLastCountPotionLife;
    public int theLastCountPotionDefense;
    public int theLastCountPotionDamage;

    public int theLastPlayerMaxUpgrades;
}
