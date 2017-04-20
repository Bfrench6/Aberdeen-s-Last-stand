using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuNavigation : MonoBehaviour {

    public GameObject GameCam;
    public GameObject MiniMapCam;
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
        MiniMapCam.SetActive(false);
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
                TimerControl.ResumeTimer(SettingsMenu.GetComponent<PauseMenuHandler>().pauseTime);
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
        MiniMapCam.SetActive(false);
        PauseCam.SetActive(true);

        Manager.Instance.Score = 0;

        foreach(GameObject screen in screens)
        {
            screen.SetActive(false);
        }
        MainMenu.SetActive(true);
        curScreen = MainMenu;

        Manager.Instance.isPaused = true;

        BackgroundMusic.Stop();
        PauseMusic.Stop();
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
        MiniMapCam.SetActive(false);
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
        MiniMapCam.SetActive(false);

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
        MiniMapCam.SetActive(true);
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

    public void goToScoreScreen(bool winner)
    {
        MiniMapCam.SetActive(false);

        foreach (GameObject screen in screens)
        {
            screen.SetActive(false);
        }

        ScoreScreen.SetActive(true);
        curScreen = ScoreScreen;

        Manager.Instance.isPaused = true;

        if(winner)
        {
            Text textElement = ScoreScreen.GetComponentInChildren<Text>();
            textElement.text = "Congratulations! You held of the hoards long enough for reinforcments to arrive!\nYour score was: " + Manager.Instance.Score.ToString();
        }
        else
        {
            Text textElement = ScoreScreen.GetComponentInChildren<Text>();
            textElement.text = "Sorry, you have failed in your defense of the city\n\nYour score was: " + Manager.Instance.Score.ToString();
        }

    }

}
