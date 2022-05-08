using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Windows : MonoBehaviour
{
    EnergyBar energyBar;
    AudioManager audioManager;

    [Header("Sprite Settings")]
    public Sprite Phase1; //default
    public Sprite Phase2;
    public Sprite Phase3;

    [Header("Music Settings")]
    public string Track1Name = "track1";
    public string Track2Name = "track2";
    public string Track3Name = "track3";

    private Phases currentPhase = Phases.Phase1;
    private string currentTrack = "track1";

    void Start()
    {
        energyBar = GameObject.FindObjectOfType<EnergyBar>();
        audioManager = GameObject.FindObjectOfType<AudioManager>();
        currentPhase = Phases.Phase1;
        this.GetComponent<SpriteRenderer>().sprite = Phase1;
        audioManager.Play(Track1Name);
        currentTrack = Track1Name;
    }

    // Update is called once per frame
    void Update()
    {
        CheckPhase();
    }

    void CheckPhase()
    {
        if(energyBar.GetEnergyPhaseStatus() != currentPhase)
        {
            switch (energyBar.GetEnergyPhaseStatus())
            {
                case Phases.Phase1:
                    this.GetComponent<SpriteRenderer>().sprite = Phase1;
                    StartCoroutine(blendIntoTrack(currentTrack, Track1Name));
                    audioManager.Stop(Track3Name);
                    audioManager.Stop(Track2Name);
                    currentPhase = Phases.Phase1;
                    currentTrack = Track1Name;
                    break;
                case Phases.Phase2:
                    this.GetComponent<SpriteRenderer>().sprite = Phase2;
                    StartCoroutine(blendIntoTrack(currentTrack, Track2Name));
                    audioManager.Stop(Track3Name);
                    audioManager.Stop(Track1Name);
                    currentPhase = Phases.Phase2;
                    currentTrack = Track2Name;
                    break;
                case Phases.Phase3:
                    this.GetComponent<SpriteRenderer>().sprite = Phase3;
                    StartCoroutine(blendIntoTrack(currentTrack, Track3Name));
                    audioManager.Stop(Track1Name);
                    audioManager.Stop(Track2Name);
                    currentPhase = Phases.Phase3;
                    currentTrack = Track3Name;
                    break;
            }
        }
        
    }

    IEnumerator blendIntoTrack(string OldTrack, string NewTrack)
    {
        audioManager.Play(NewTrack);
        yield return new WaitForSecondsRealtime(1);
        audioManager.Stop(OldTrack);
    }
}
