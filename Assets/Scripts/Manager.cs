//Ben French, Chuan Yui, Pranav Bhardwaj

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : Singleton<Manager>
{
    protected Manager() { } // guarantee this will be always a singleton only - can't use the constructor!

    public int length = 1;                  //game length (90 second intervals)
    public string difficulty = "Medium";    //game difficulty
    public int difficultyMult = 2;          //enemy health and damage multiplier based on difficulty

    public float masterVol = 1f;
    public float musicVol = 1f;
    public float FXVol = 1f;

    public bool isPaused = true;
    public bool gameOver = false;

    public enum powerType { damage, points, health, moveSpeed, stoneHealth }    //power up types

    public bool doubleDamage = false;

    public int Score;
    public bool doublePoints = false;
    
    public int HealthPowerUp = 20;      //amount to heal player when they recieve health powerup

    public bool movePU = false;

    public int StoneHealthPU = 100;      //amount to heal stone when player picks up stone health power up

    public string[] pUpNames = new string[] { "DamagePowerUp", "HealthPowerUp", "MoveSpeedPowerUp", "PointsPowerUp", "StoneHealthPowerUp" }; // power up names for prefab reference

}