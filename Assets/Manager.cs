using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : Singleton<Manager>
{
    protected Manager() { } // guarantee this will be always a singleton only - can't use the constructor!

    public string difficulty = "Medium";

    public float masterVol = 100;
    public float musicVol = 100;
    public float FXVol = 100;

}