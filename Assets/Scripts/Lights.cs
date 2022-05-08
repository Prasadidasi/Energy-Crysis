using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lights : MonoBehaviour
{
    AudioManager audioManager;

    [Header("Light Settings")]
    public GameObject LightsOffPrefab;
    public Sprite LightOn;
    public Sprite LightOff;
    public float EnergyConsumedPerSecond = 0.05f;

    [Header("SFX")]
    public string sfxName;

    private float EnergyTime = 1f;
    private float currentEnergyTime = 1f;
    bool isLightsOn = false;
    EnergyBar energyBar;

    public void Start()
    {
        energyBar = GameObject.FindObjectOfType<EnergyBar>();
        audioManager = GameObject.FindObjectOfType<AudioManager>();
        isLightsOn = false;
        this.GetComponent<SpriteRenderer>().sprite = LightOn;
        LightsOffPrefab.SetActive(true);
    }

    public void Update()
    {
        if (isLightsOn)
        {
            UpdateEnergy();
        }
    }
    void OnMouseDown()
    {
        audioManager.Play(sfxName);
        if (!isLightsOn)
        {
            TurnLightOn();
        }
        else
        {
            TurnLightOff();
        }
    }

    public void TurnLightOn()
    {
        isLightsOn = true;
        currentEnergyTime = EnergyTime;
        LightsOffPrefab.SetActive(false);
        this.GetComponent<SpriteRenderer>().sprite = LightOn;
        UpdateEnergy();
    }

    public void TurnLightOff()
    {
        isLightsOn = false;
        this.GetComponent<SpriteRenderer>().sprite = LightOff;
        currentEnergyTime = EnergyTime;
        LightsOffPrefab.SetActive(true);
    }
    public void UpdateEnergy()
    {
        currentEnergyTime -= Time.deltaTime;
        if (currentEnergyTime <= 0)
        {
            energyBar.MinusEnergy(EnergyConsumedPerSecond);
            currentEnergyTime = EnergyTime;
        }
    }
}
