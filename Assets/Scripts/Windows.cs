using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [Header("House Talk Settings")]
    public GameObject TalkUI;
    public string[] promptsLow;
    public string[] promptsMedium; 
    public string[] promptsUrgent;
    public int promptTimeInSeconds = 4;
    public bool isFirstWindow = false;
    private float MaxTIME = 4;
    private float currentTime = 0;

    private Phases currentPhase = Phases.Phase1;
    private string currentTrack = "track1";
    private Clock clock;
    CanvasManager canvasManager;

    void Start()
    {
        canvasManager = GameObject.FindObjectOfType<CanvasManager>();
        clock = GameObject.FindObjectOfType<Clock>();
        energyBar = GameObject.FindObjectOfType<EnergyBar>();
        audioManager = GameObject.FindObjectOfType<AudioManager>();
        currentPhase = Phases.Phase1;
        this.GetComponent<SpriteRenderer>().sprite = Phase1;
        audioManager.Play(Track1Name);
        currentTrack = Track1Name;
        if (isFirstWindow)
        {
            if (TalkUI == null)
            {
                Debug.LogError("talk Prefab UI is null!");
            }
            if (promptsLow == null || promptsMedium == null || promptsUrgent == null)
            {
                Debug.LogError("prompt messages are null!");
            }
        }
        if(TalkUI != null)
            TalkUI.SetActive(false);
        MaxTIME = 2;
        currentTime = MaxTIME*10;//convertion to seconds
    }

    // Update is called once per frame
    void Update()
    {
        CheckPhase();
        if (isFirstWindow)
        {
            currentTime -= Time.deltaTime;
            if(currentTime <= 0)
            {
                StartCoroutine(HouseTalk());
                currentTime = UnityEngine.Random.Range(0, 3)*10f;
            }
        }
        if (clock.GetClockStatus())
        {
            audioManager.Stop(currentTrack);
            audioManager.Play("gamewin");
            canvasManager.OpenVictoryMenu();
        }
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

    string GetPrompt()
    { 
        string tmp = "";
        if(currentPhase == Phases.Phase1)
        {
            tmp = promptsLow[UnityEngine.Random.Range(0, promptsLow.Length)];
        }
        else if (currentPhase == Phases.Phase2)
        {
            tmp = promptsMedium[UnityEngine.Random.Range(0, promptsMedium.Length)]; ;
        }
        else
        {
            tmp = promptsUrgent[UnityEngine.Random.Range(0, promptsUrgent.Length)]; ;
        }
        return tmp;
    }

    IEnumerator blendIntoTrack(string OldTrack, string NewTrack)
    {
        audioManager.Play(NewTrack);
        yield return new WaitForSecondsRealtime(1);
        audioManager.Stop(OldTrack);
    }

    IEnumerator HouseTalk()
    {
        TalkUI.SetActive(true);
        TalkUI.GetComponentInChildren<Text>().text = GetPrompt();
        yield return new WaitForSeconds(promptTimeInSeconds);
        TalkUI.SetActive(false);
    }
}
