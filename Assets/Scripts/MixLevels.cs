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
        Manager.Instance.masterVol = 1 + masterVol/80;
        mixer.SetFloat("MasterVol", masterVol);
    }

    public void SetFXVol(float FXVol)
    {
        Manager.Instance.FXVol = 1 + FXVol/80;
        mixer.SetFloat("FXVol", FXVol);
    }

    public void SetMusicVol(float musicVol)
    {
        Manager.Instance.musicVol = 1 + musicVol/80;
        mixer.SetFloat("MusicVol", musicVol);
    }
}
