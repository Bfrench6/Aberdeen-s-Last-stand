using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerControl : MonoBehaviour {

    public Text timertextElement;
    public Text finishedtextElement;
    public MenuNavigation nav;
    public EnemyManager enemyMan;
    public EnemyManager enemyMan2;

    private static float goalTime;
    public static float totalTime = 60; //the amount of time you want to start the countdown;
    private static float pauseTime = 0;

    private string timerText;
    private float guiTime;
    
    


    public static void StartTimer(int length)
    {
        totalTime += length;
        goalTime = Time.time + totalTime;

    }

    void Update()
    {   
        guiTime = (goalTime - Time.time) + pauseTime; 
        if (guiTime > 0)
        {
            float minutes = Mathf.Floor(guiTime / 60);
            float seconds = guiTime % 60;

            timerText = string.Format("{0:00}:{1:00}", minutes, seconds);
            timertextElement.text = timerText;
        }
        else
        {

            if(enemyMan.allDead())
            {
                Manager.Instance.gameOver = true;
                nav.goToScoreScreen(true);
            }
            else
            {
                enemyMan.stopSpawn();
                enemyMan2.stopSpawn();
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                finishedtextElement.text = "Kill the remaining enemies to win\nEnemies left: " + enemies.Length;
            }
            guiTime = 0;
        }

    }

    public static void ResumeTimer(float pausedTime)
    {
        pauseTime = pausedTime;
    }

    public float getCurTime()
    {
        return guiTime;
    }

    public float getTotalTime()
    {
        return totalTime;
    }
}
