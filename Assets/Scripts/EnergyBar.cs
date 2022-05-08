using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Phases
{
    Phase1,
    Phase2,
    Phase3
}

public class EnergyBar : MonoBehaviour
{
    CanvasManager canvasManager;
    public Image energyImage;
    private Phases currentPhase = Phases.Phase1;

    public float MaxEnergy = 100;
    private float currentEnergy = 100;
    //todo add player or energy consumption thing here.

    private void Start()
    {
        currentPhase = Phases.Phase1;
        canvasManager = GameObject.FindObjectOfType<CanvasManager>();
        UpdateEnergyBar();
    }

    private void Update()
    {
        if (currentEnergy <= 0)
        {
            canvasManager.OpenGameOverMenu();
        }
    }

    private void UpdateEnergyBar()
    {
        energyImage.fillAmount = Mathf.Clamp(/*Todo addenergyconsumption value ie. energy / energyMax*/currentEnergy/MaxEnergy, 0, 1f);
        if (GetEnergyStatus() <= 100 && 70 < GetEnergyStatus())
        {
            currentPhase = Phases.Phase1;
        }
        else if (GetEnergyStatus() <= 70 && 35 < GetEnergyStatus())
        {
            currentPhase = Phases.Phase2;
        }
        else if (GetEnergyStatus() <= 35 && 0 < GetEnergyStatus())
        {
            currentPhase = Phases.Phase3;
        }
    }

    public void MinusEnergy(float amount)
    {
        currentEnergy -= amount;
        UpdateEnergyBar();
    }

    public void AddEnergy(float amount)
    {
        currentEnergy += amount;
        UpdateEnergyBar();
    }

    public float GetEnergyStatus()
    {
        return currentEnergy;
    }

    public Phases GetEnergyPhaseStatus()
    {
        return currentPhase;
    }
}
