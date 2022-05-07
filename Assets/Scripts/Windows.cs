using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Windows : MonoBehaviour
{
    EnergyBar energyBar;

    [Header("Sprite Settings")]
    public Sprite Phase1; //default
    public Sprite Phase2;
    public Sprite Phase3;

    bool hasEnteredNextPhase = false;

    void Start()
    {
        energyBar = GameObject.FindObjectOfType<EnergyBar>();
        this.GetComponent<SpriteRenderer>().sprite = Phase1;
    }

    // Update is called once per frame
    void Update()
    {
        CheckPhase();
    }

    void CheckPhase()
    {
        switch (energyBar.GetEnergyPhaseStatus())
        {
            case Phases.Phase1:
                this.GetComponent<SpriteRenderer>().sprite = Phase1;
                break;
            case Phases.Phase2:
                this.GetComponent<SpriteRenderer>().sprite = Phase2;
                break;
            case Phases.Phase3:
                this.GetComponent<SpriteRenderer>().sprite = Phase3;
                break;
        }
    }
}
