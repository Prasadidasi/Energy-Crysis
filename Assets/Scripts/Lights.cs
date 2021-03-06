using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
    CanvasManager canvasManager;

    public void Start()
    {
        canvasManager = GameObject.FindObjectOfType<CanvasManager>();
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
    public void OnMouseEnter()
    {
        Cursor.SetCursor(canvasManager.hoverCursor, Vector2.zero, CursorMode.Auto);
    }
    public void OnMouseExit()
    {
        Cursor.SetCursor(canvasManager.defaultCursor, Vector2.zero, CursorMode.Auto);
    }
    void OnMouseDown()
    {
        Cursor.SetCursor(canvasManager.clickCursor, Vector2.zero, CursorMode.Auto);
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
