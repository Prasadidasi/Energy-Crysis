using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Machine : MonoBehaviour
{
    [Header("Machine Settings")]
    public int EnergyConsumedPerSecond = 2;//TODO 
    public float MaxUseTime = 20f;//in seconds

    [SerializeField] private float currentTime = 0;
    private float EnergyTime = 1f;
    private float currentEnergyTime = 1f;
    bool isUsingMachine = false;
    EnergyBar energyBar;

    [Header("VFX Settings")]
    public GameObject effectVFX;

    [Header("SFX Settings")]
    public string AudioName = "wrr";
    AudioManager audioManager;

    [Header("Sprite Settings")]
    [Tooltip("This boolean is used to either use sprite swapping or vfx smoke when the machine is on")]
    public bool RequiresSpriteSwap = false;
    public Sprite MachineOffSprite;
    public Sprite MachineOnSprite;

    

    // Start is called before the first frame update
    void Start()
    {
        energyBar = GameObject.FindObjectOfType<EnergyBar>();
        audioManager = GameObject.FindObjectOfType<AudioManager>();
        StopMachine();
        if(effectVFX == null)
        {
            Debug.LogWarning("effect vfx prefab for " + this.gameObject.name + " has been not added (is null)");
        }
        if (MachineOffSprite == null)
        {
            Debug.LogWarning("machine off sprite for " + this.gameObject.name + " has been not added (is null)");
        }
    }

    // Update is called once per frame
    void Update()
    {        
        if (isUsingMachine)
        {
            currentTime += Time.deltaTime;        
            if(currentTime >= MaxUseTime)
            {
                StopMachine();
                currentTime = 0;
            }
            UpdateEnergy();
        }
    }

    void OnMouseDown()
    {
        if (!isUsingMachine)
        {
            StartMachine();
        }
        else
        {
            StopMachine();
        }
    }

    //TODO: hover status?
    //private void OnMouseOver()
    //{
        
    //}

    public void StartMachine()
    {
        isUsingMachine = true;
        currentEnergyTime = EnergyTime;
        if (RequiresSpriteSwap)
        {
            this.GetComponent<SpriteRenderer>().sprite = MachineOnSprite;
        }
        else
        {
            effectVFX.SetActive(true);
        }
        UpdateEnergy();
        audioManager.Play(AudioName);
    }

    public void StopMachine()
    {
        isUsingMachine = false;
        if (RequiresSpriteSwap)
        {
            this.GetComponent<SpriteRenderer>().sprite = MachineOffSprite;
        }
        else
        {
            effectVFX.SetActive(false);
        }
        currentEnergyTime = EnergyTime;
        currentTime = 0;
        audioManager.Stop(AudioName);
    }

    public bool GetIsMachineOn() //getting status of machine
    {
        return isUsingMachine;
    }

    public void UpdateEnergy()
    {
        currentEnergyTime -= Time.deltaTime;
        if(currentEnergyTime <= 0)
        {
            energyBar.MinusEnergy(EnergyConsumedPerSecond);
            currentEnergyTime = EnergyTime;
        }   
    }
}