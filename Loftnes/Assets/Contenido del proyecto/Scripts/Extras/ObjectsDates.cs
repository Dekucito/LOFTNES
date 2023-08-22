using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new object data", menuName = "shop object")]
public class ObjectsDates : ScriptableObject
{
    public string nameObjet;
    public int objectPrice;
    public Tipo type;

    public enum Tipo
    {
        life,damage,defense
    }
}
