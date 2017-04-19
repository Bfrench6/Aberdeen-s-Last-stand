using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : Singleton<Manager>
{
    protected Manager() { } // guarantee this will be always a singleton only - can't use the constructor!

    public int length = 1;
    public string difficulty = "Medium";
    public int difficultyMult = 2;

    public float masterVol = 100;
    public float musicVol = 100;
    public float FXVol = 100;

    public bool isPaused = false;
    public bool gameOver = false;

    public int Score;

}