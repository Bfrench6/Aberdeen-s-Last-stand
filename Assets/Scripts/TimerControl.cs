//Ben French, Chuan Yui, Pranav Bhardwaj

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerControl : MonoBehaviour {

    public Text timertextElement;
    public Text finishedtextElement;
    public Text powerUpText;
    public GameObject player;
    public MenuNavigation nav;
    public EnemyManager enemyMan;
    public EnemyManager enemyMan2;

    private static float goalTime;
    public static float gameTime;
    public static float totalTime = 90; //the amount of time you want to start the countdown;
    private static float pauseTime = 0;

    private string timerText;
    private float guiTime;

    public static void StartTimer(int length)
    {
        gameTime = totalTime * length;
        goalTime = Time.time + gameTime;

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
            {   //go to score creen once player wins
                Manager.Instance.gameOver = true;
                nav.goToScoreScreen(true);
            }
            else
            {
                //stop enemies from spawning and tell player how many they have left once timer ends
                enemyMan.stopSpawn();
                enemyMan2.stopSpawn();
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                finishedtextElement.text = "Kill the remaining enemies to win\nEnemies left: " + enemies.Length;
            }
            guiTime = 0;
        }
        //set power up text
        PowerUp[] playerPUs = player.GetComponents<PowerUp>();
        powerUpText.text = "Current power ups: ";
        foreach (PowerUp pu in playerPUs)
        {
            powerUpText.text += "<color=" + RGBToHex(pu.color) + ">" + pu.Name + "</color>\n";
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

    private string RGBToHex(Color color)
    {
        return string.Format("#{0}{1}{2}",
                     ((int)(color.r * 255)).ToString("X2"),
                     ((int)(color.g * 255)).ToString("X2"),
                     ((int)(color.b * 255)).ToString("X2"));
    }
}
