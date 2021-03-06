using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    public Text clockTime;
    public float MaxTime = 3f;
    [SerializeField] private float currentTime = 300f;
    private bool isClockTimeFinished = false;

    [Header("SFX")]
    public string sfxName;
    public string sfxWinName;

    public void Start()
    {
        currentTime = MaxTime;
        isClockTimeFinished = false;
    }

    public void Update()
    {
        currentTime -= Time.deltaTime;
        if(currentTime <= 0 && !isClockTimeFinished)
        {
            isClockTimeFinished = true;
            currentTime = 0;
            clockTime.text = currentTime.ToString();
        }
        
        if(!isClockTimeFinished)
        {
            clockTime.text = currentTime.ToString();
        }

    }

    //Call this is game manager, do a check and if true then end game!
    public bool GetClockStatus()
    {
        return isClockTimeFinished;
    }

    public float GetCurrentTime()
    {
        return currentTime;
    }
}
