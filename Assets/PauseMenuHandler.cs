using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class PauseMenuHandler : MonoBehaviour {

    public Slider masterVolume;
    public Slider musicVolume;
    public Slider fxVolume;
    public Button difficultyButton;
	// Use this for initialization
	void Start () {
        masterVolume.onValueChanged.AddListener(delegate { ChangeMasterVolume(); });
        musicVolume.onValueChanged.AddListener(delegate { ChangeMusicVolume(); });
        fxVolume.onValueChanged.AddListener(delegate { ChangeFXVolume(); });

        difficultyButton.onClick.AddListener(ChangeDifficulty);



    }

    void ChangeMasterVolume()
    {
        //TODO: change global master volume variable
    }

    void ChangeMusicVolume()
    {
        //TODO: change global music volume variable
    }

    void ChangeFXVolume()
    {
        //TODO: change global fx volume variable
    }

    void ChangeDifficulty()
    {
        Text diffTextElement = difficultyButton.GetComponentInChildren<Text>();
        String diffText = diffTextElement.text;
        switch (diffText)
        {
            case "Easy":
                diffTextElement.text = "Medium";
                //TODO: set global difficulty variable
                break;
            case "Medium":
                diffTextElement.text = "Hard";
                //TODO: set global difficulty variable 
                break;
            case "Hard":
                diffTextElement.text = "Easy";
                //TODO: set global difficulty variable
                break;
        }
            

    }



}
