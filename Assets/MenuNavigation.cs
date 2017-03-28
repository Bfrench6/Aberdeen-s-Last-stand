using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuNavigation : MonoBehaviour {

    public Canvas MainMenu;
    public Canvas SettingsMenu;
    public Canvas Credits;

    public Canvas curScreen;


	// Use this for initialization
	void Start () {
        curScreen = SettingsMenu;
		
	}
	
	// Update is called once per frame
	void Update () {

        if (curScreen == MainMenu)
        {


        } else if (curScreen == SettingsMenu)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Start"))
            {
                SceneManager.LoadScene(1);
            }

            
        } else if (curScreen == Credits)
        {

        }
		
	}
    void goToMainMenu()
    {

    }

    void goToPauseMenu()
    {

    }

    void goToCredits()
    {

    }
}
