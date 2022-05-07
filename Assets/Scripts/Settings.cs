using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public static Settings settings;

    public bool timerDisabled = false;
    
    Toggle timerToggle;
    


    void FixedUpdate()
    {
    }


    public void TimerToggle(bool tog)
    {
        if(tog) 
        {
            Debug.Log("1");
            PlayerPrefs.SetInt("timerDisabled", 1);
        }

        else 
        {
            Debug.Log("0");
            PlayerPrefs.SetInt("timerDisabled", 0);
        }
        
        timerDisabled = tog;
    }
}
