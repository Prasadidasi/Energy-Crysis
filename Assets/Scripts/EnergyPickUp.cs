using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyPickUp : MonoBehaviour
{
    EnergyBar energyBar;
    //TODO add navigation finder from scene?

    [Header("Pick Up Settings")]
    public float EnergyAmount = 10;

    [Header("VFX")]
    public GameObject VfxPrefab;

    void Start()
    {
        energyBar = GameObject.FindObjectOfType<EnergyBar>();
        VfxPrefab.SetActive(true);
    }

    // Update is called once per frame
    void OnMouseDown()
    {
        energyBar.AddEnergy(EnergyAmount);
        VfxPrefab.SetActive(false);
        Destroy(this.gameObject);
    }
}
