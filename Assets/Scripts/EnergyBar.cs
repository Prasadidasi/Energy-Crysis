using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    public Image energyImage;

    public float MaxEnergy = 100;
    private float currentEnergy = 100;
    //todo add player or energy consumption thing here.

    private void UpdateEnergyBar()
    {
        energyImage.fillAmount = Mathf.Clamp(/*Todo addenergyconsumption value ie. energy / energyMax*/currentEnergy/MaxEnergy, 0, 1f);
    }

    public void MinusEnergy(int amount)
    {
        currentEnergy -= amount;
        UpdateEnergyBar();
    }

    public void AddEnergy(int amount)
    {
        currentEnergy += amount;
        UpdateEnergyBar();
    }
}
