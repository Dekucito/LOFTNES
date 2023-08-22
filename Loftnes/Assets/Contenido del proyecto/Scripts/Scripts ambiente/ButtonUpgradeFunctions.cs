using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ButtonUpgradeFunctions : MonoBehaviour
{
    public int numberOfUpgrades;
    public TMP_Text numberText;

    private void Awake()
    {
        numberText.text = "";
    }
    private void Update()
    {
        numberText.text = numberOfUpgrades.ToString(); 
    }

    public void ButtonPlus()
    {
        numberOfUpgrades += 1;
    }

    public void ButtonLess()
    {
        numberOfUpgrades -= 1;
    }
}
