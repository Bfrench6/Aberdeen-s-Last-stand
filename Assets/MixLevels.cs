using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MixLevels : MonoBehaviour
{

    public AudioMixer mixer;
    public void Start()
    {
        mixer.SetFloat("MasterVol", 0);
        mixer.SetFloat("FXVol", 0);
        mixer.SetFloat("MusicVol", 0);
    }

    public void SetMasterVol(float masterVol)
    {
        mixer.SetFloat("MasterVol", masterVol);
    }

    public void SetFXVol(float FXVol)
    {
        mixer.SetFloat("FXVol", FXVol);
    }

    public void SetMusicVol(float musicVol)
    {
        mixer.SetFloat("MusicVol", musicVol);
    }
}
