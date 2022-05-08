using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HungerState
{
    normal,
    hungry
}

public class Player : MonoBehaviour
{
    AudioManager audioManager;
    EnergyBar energyBar;
    CanvasManager canvasManager;
    Clock clock;
    Animator anim;
    GameObject[] hungerItems;

    [Header("SFX")]
    public string sfxHungerName;

    [Header("Energy pick up settings")]
    public GameObject pickupPrefab;

    [Header("Hunger Settings")]
    public float MaxHunger = 100;
    [SerializeField] float currentHunger = 0;
    public float HungerGainPerSecond = 5;
    public float HungerSatisficationWhileMachineOn = 5;
    [SerializeField] float currentTime = 0;
    [SerializeField] float MaxTime = 100;
   [SerializeField] HungerState currentHungerState = HungerState.normal;

    [Header("Energy Settings")]
    public float EnergyDrainPerSecond = 0.3f;

    bool hasDropped = false;
    bool hasDropped2 = false;
    bool hasDropped3 = false;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.FindObjectOfType<AudioManager>();
        energyBar = GameObject.FindObjectOfType<EnergyBar>();
        clock = GameObject.FindObjectOfType<Clock>();
        canvasManager = GameObject.FindObjectOfType<CanvasManager>();
        hungerItems = GameObject.FindGameObjectsWithTag("hungerItem");
        anim = GetComponent<Animator>();
        if(audioManager == null|| hungerItems == null)
        {
            Debug.LogWarning("Cannot find hunger items or audio manager");
        }
        hasDropped = false;
        hasDropped2 = false;
        hasDropped3 = false;
        currentHungerState = HungerState.normal;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= MaxTime)
        {
            energyBar.MinusEnergy(EnergyDrainPerSecond);
            currentHunger += HungerGainPerSecond;
            CheckHungerStatus();
            currentTime = 0;
        }   

        if (!clock.GetClockStatus())
        {
            float time = clock.GetCurrentTime() / clock.MaxTime;
            if (time <= 0.70 && !hasDropped)
            {
                Instantiate(pickupPrefab);
                pickupPrefab.GetComponent<EnergyPickUp>().EnergyAmount = GetEnergyAmount();
                hasDropped = true;
            }
            if (time <= 0.40 && !hasDropped2)
            {
                Instantiate(pickupPrefab);
                pickupPrefab.GetComponent<EnergyPickUp>().EnergyAmount = GetEnergyAmount();
                hasDropped2 = true;
            }
            if (time <= 0.10 && !hasDropped3)
            {
                Instantiate(pickupPrefab);
                pickupPrefab.GetComponent<EnergyPickUp>().EnergyAmount = GetEnergyAmount();
                hasDropped3 = true;
            }
        }


    }

    private void CheckHungerItemsOn()
    {
        foreach (GameObject item in hungerItems)
        {
            if (item.GetComponent<Machine>().isUsingMachine)
            {
                currentHunger -= HungerSatisficationWhileMachineOn;
                currentHunger = Mathf.Clamp(currentHunger, 0, MaxHunger);
            }
        }
    }

    private void CheckHungerStatus()
    {
        CheckHungerItemsOn();
        if (currentHunger >= MaxHunger && currentHungerState == HungerState.hungry)
        {
            canvasManager.OpenGameOverMenu();
            Time.timeScale = 0;
        }
        if ((currentHunger / MaxHunger) >= 0.80)
        {
            if(currentHungerState != HungerState.hungry)
            {
                currentHungerState = HungerState.hungry;
                SwitchAnimState();
            }
        }
        else
        {
            if (currentHungerState != HungerState.normal)
            {
                currentHungerState = HungerState.normal;
                SwitchAnimState();
            }
        }
    }

    float GetEnergyAmount()
    {
        float tmp = 0;
        if((currentHunger/MaxHunger) <= 0.10)
        {
            tmp = 40;
        }
        if ((currentHunger / MaxHunger) <= 0.20)
        {
            tmp = 30;
        }
        if ((currentHunger / MaxHunger) <= 0.30)
        {
            tmp = 20;
        }
        return tmp;
    }

    void SwitchAnimState()
    {
        switch (currentHungerState)
        {
            case HungerState.normal:
                anim.SetBool("isHungry", false);
                audioManager.Stop(sfxHungerName);
                break;
            case HungerState.hungry:
                anim.SetBool("isHungry", true);
                audioManager.Play(sfxHungerName);
                break;
        }
    }
}
