using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyPickUp : MonoBehaviour
{
    EnergyBar energyBar;
    AudioManager audioManager;
    //TODO add navigation finder from scene?

    [Header("Pick Up Settings")]
    public float EnergyAmount = 10;

    [Header("VFX")]
    public GameObject VfxPrefab;

    [Header("SFX")]
    public string sfxName;
    public string sfxSpawnName;

    void Start()
    {
        energyBar = GameObject.FindObjectOfType<EnergyBar>();
        audioManager = GameObject.FindObjectOfType<AudioManager>();
        VfxPrefab.SetActive(true);
        audioManager.Play(sfxSpawnName);
    }

    // Update is called once per frame
    void OnMouseDown()
    {
        energyBar.AddEnergy(EnergyAmount);
        audioManager.Play(sfxName);
        VfxPrefab.SetActive(false);
        Destroy(this.gameObject);
    }
}
