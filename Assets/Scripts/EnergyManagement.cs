using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyManagement : MonoBehaviour
{
    public static EnergyManagement Instance;

    private EnergyBar energyBar;
    

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }


    void Update()
    {
        
    }
}
