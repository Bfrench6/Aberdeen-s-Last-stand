using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuNavigation : MonoBehaviour {

    public GameObject GameCam;
    public GameObject PauseCam;

    public GameObject MainMenu;
    public GameObject InfoScreen;
    public GameObject SettingsMenu;
    public GameObject Credits;

    public GameObject curScreen;
    




	// Use this for initialization
	void Start () {
        goToMainMenu();
        GameCam.SetActive(false);
        PauseCam.SetActive(true);
		
	}
	
	// Update is called once per frame
	void Update () {

        if (curScreen == null)
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
                goToMainMenu();
            }

        }
		
	}
    public void goToMainMenu()
    {
        GameCam.SetActive(false);
        PauseCam.SetActive(true);

        MainMenu.SetActive(true);
        InfoScreen.SetActive(false);
        SettingsMenu.SetActive(false);
        Credits.SetActive(false);
        curScreen = MainMenu;

        Manager.Instance.isPaused = true;

    }

    public void goToInfo()
    {
        MainMenu.SetActive(false);
        InfoScreen.SetActive(true);
        SettingsMenu.SetActive(false);
        Credits.SetActive(false);
        curScreen = InfoScreen;

        Manager.Instance.isPaused = true;

    }

    public void goToPauseMenu()
    {
        GameCam.SetActive(false);
        PauseCam.SetActive(true);

        MainMenu.SetActive(false);
        InfoScreen.SetActive(false);
        SettingsMenu.SetActive(true);
        Credits.SetActive(false);
        curScreen = SettingsMenu;

        Manager.Instance.isPaused = true;

    }

    public void goToCredits()
    {
        MainMenu.SetActive(false);
        InfoScreen.SetActive(false);
        SettingsMenu.SetActive(false);
        Credits.SetActive(true);
        curScreen = Credits;

        Manager.Instance.isPaused = true;

    }

    public void goToGame()
    {
        GameCam.SetActive(true);
        PauseCam.SetActive(false);

        MainMenu.SetActive(false);
        InfoScreen.SetActive(false);
        SettingsMenu.SetActive(false);
        Credits.SetActive(false);
        curScreen = null;

        Manager.Instance.isPaused = false;

    }
}
