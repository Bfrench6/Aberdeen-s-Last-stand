using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuNavigation : MonoBehaviour {

    public GameObject GameCam;
    public GameObject PauseCam;

    public GameObject MainMenu;
    public GameObject InfoScreen;
    public GameObject SettingsMenu;
    public GameObject Credits;
    public GameObject HUD;
    public GameObject ScoreScreen;

    public AudioSource TitleMusic;
    public AudioSource PauseMusic;
    public AudioSource BackgroundMusic;

    public GameObject curScreen;

    GameObject[] screens; //Make sure to update this in start if you add a canvas
    




	// Use this for initialization
	void Start () {
        screens = new GameObject[] { MainMenu, InfoScreen, SettingsMenu, Credits, HUD, ScoreScreen };

        goToMainMenu();
        GameCam.SetActive(false);
        PauseCam.SetActive(true);
        
		
	}
	
	// Update is called once per frame
	void Update () {


        if (curScreen == HUD)
        {
            if (Input.GetKeyDown(KeyCode.Escape) /*|| Input.GetButtonDown("Start")*/)
            {
                goToPauseMenu();
            }

        }
        else if (curScreen == MainMenu)
        {


        }
        else if (curScreen == SettingsMenu)
        {
            if (Input.GetKeyDown(KeyCode.Escape) /*|| Input.GetButtonDown("Start")*/)
            {
                goToGame();
            }
            

            
        }
        else if (curScreen == Credits)
        {
            if (Input.GetKeyDown(KeyCode.Escape) /*|| Input.GetButtonDown("Start")*/)
            {
                if(Manager.Instance.gameOver)
                {
                    Manager.Instance.gameOver = false;
                    SceneManager.LoadScene(0);
                }
                goToMainMenu();
            }

        }
        else if (curScreen == ScoreScreen)
        {
            if (Input.GetKeyDown(KeyCode.Escape) /*|| Input.GetButtonDown("Start")*/)
            {
                goToCredits();
            }
        }
		
	}
    public void goToMainMenu()
    {
        GameCam.SetActive(false);
        PauseCam.SetActive(true);

        Manager.Instance.Score = 0;

        foreach(GameObject screen in screens)
        {
            screen.SetActive(false);
        }
        MainMenu.SetActive(true);
        curScreen = MainMenu;

        Manager.Instance.isPaused = true;

        TitleMusic.Play();

    }

    public void goToInfo()
    {
        foreach (GameObject screen in screens)
        {
            screen.SetActive(false);
        }

        InfoScreen.SetActive(true);
        curScreen = InfoScreen;

        Manager.Instance.isPaused = true;

    }

    public void goToPauseMenu()
    {
        GameCam.SetActive(false);
        PauseCam.SetActive(true);

        foreach (GameObject screen in screens)
        {
            screen.SetActive(false);
        }

        SettingsMenu.SetActive(true);
        curScreen = SettingsMenu;

        Manager.Instance.isPaused = true;

        BackgroundMusic.Pause();
        PauseMusic.Play();

    }

    public void goToCredits()
    {

        foreach (GameObject screen in screens)
        {
            screen.SetActive(false);
        }

        Credits.SetActive(true);
        curScreen = Credits;

        Manager.Instance.isPaused = true;

    }

    public void goToGame()
    {
        GameCam.SetActive(true);
        PauseCam.SetActive(false);

        foreach (GameObject screen in screens)
        {
            screen.SetActive(false);
        }

        HUD.SetActive(true);
        curScreen = HUD;

        Manager.Instance.isPaused = false;

        TitleMusic.Stop();
        PauseMusic.Stop();
        BackgroundMusic.Play();

    }

    public void goToScoreScreen()
    {
        foreach (GameObject screen in screens)
        {
            screen.SetActive(false);
        }

        ScoreScreen.SetActive(true);
        curScreen = ScoreScreen;

        Manager.Instance.isPaused = true;

        UnityEngine.UI.Text winLoseText = ScoreScreen.GetComponentInChildren<UnityEngine.UI.Text>();
        winLoseText.text += Manager.Instance.Score.ToString();

    }

}
