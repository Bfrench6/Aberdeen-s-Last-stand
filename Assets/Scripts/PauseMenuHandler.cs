//Ben French, Chuan Yui, Pranav Bhardwaj

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuHandler : MonoBehaviour {

    public Slider masterVolume;
    public Slider musicVolume;
    public Slider fxVolume;
    public Button difficultyButton;
    public float pauseTime;
    
	void Start () {
        masterVolume.onValueChanged.AddListener(delegate { ChangeMasterVolume(); });
        musicVolume.onValueChanged.AddListener(delegate { ChangeMusicVolume(); });
        fxVolume.onValueChanged.AddListener(delegate { ChangeFXVolume(); });

        difficultyButton.onClick.AddListener(ChangeDifficulty);
    }

    void Update()
    {
        pauseTime += Time.deltaTime;
    }

    void ChangeMasterVolume()
    {
        Manager.Instance.masterVol = masterVolume.value;
    }

    void ChangeMusicVolume()
    {
        Manager.Instance.musicVol = musicVolume.value;
    }

    void ChangeFXVolume()
    {
        Manager.Instance.FXVol = fxVolume.value;
    }

    void ChangeDifficulty()
    {
        Text diffTextElement = difficultyButton.GetComponentInChildren<Text>();
        string diffText = diffTextElement.text;
        switch (diffText)
        {
            case "Easy":
                diffTextElement.text = "Medium";
                Manager.Instance.difficulty = "Medium";
                Manager.Instance.difficultyMult = 2;
                break;
            case "Medium":
                diffTextElement.text = "Hard"; 
                Manager.Instance.difficulty = "Hard";
                Manager.Instance.difficultyMult = 3;
                break;
            case "Hard":
                diffTextElement.text = "Easy";
                Manager.Instance.difficulty = "Easy";
                Manager.Instance.difficultyMult = 1;
                break;
        }
    }

    public void quitToMainMenu()
    {
        Manager.Instance.gameOver = false;
        SceneManager.LoadScene(0);
    }
}
