using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    public Image energyImage;

    //todo add player or energy consumption thing here.

    public void UpdateEnergyBar()
    {
        energyImage.fillAmount = Mathf.Clamp(/*Todo addenergyconsumption value ie. energy / energyMax*/50/100, 0, 1f);
    }
}
