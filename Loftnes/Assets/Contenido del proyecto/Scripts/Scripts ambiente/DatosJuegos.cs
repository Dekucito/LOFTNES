using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DatosJuegos
{
    [Header("datos del player a guardar ")]
    public Vector3 theLastPlayerPosition;
    public float theLastPlayerLife;
    public int theLastPlayerMoney;

    public bool GameExist;

}
