using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerControl : MonoBehaviour {

    public Text timertextElement;
    public MenuNavigation nav;

    private static float goalTime;
    public static float totalTime = 60; //the amount of time you want to start the countdown;
    private static float pauseTime = 0;

    private string timerText; 


    public static void StartTimer(int length)
    {
        goalTime = Time.time + (totalTime * length);

    }

    void Update()
    {   
        float guiTime = (goalTime - Time.time) + pauseTime; 

        float minutes = Mathf.Floor(guiTime / 60);
        float seconds = guiTime % 60;

        timerText = string.Format("{0:00}:{1:00}", minutes, seconds);
        timertextElement.text = timerText;

        if (guiTime < 0)
        {
            nav.goToScoreScreen(true);
        }

    }

    public static void ResumeTimer(float pausedTime)
    {
        pauseTime = pausedTime;
    }
}
