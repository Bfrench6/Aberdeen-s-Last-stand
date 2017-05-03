//Ben French, Chuan Yui, Pranav Bhardwaj

using UnityEngine;
using UnityEngine.Audio;

public class MasterAudioMixer : MonoBehaviour {

    public AudioMixer mixer;

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
