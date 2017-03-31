using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuHandler : MonoBehaviour {

    public MenuNavigation nav;

    public Button StartButton;
    public Button CreditsButton;

    public GameObject startStroke;
    public GameObject creditsStroke;

    public bool startSelected;


	// Use this for initialization
	void Start () {
        StartButton.onClick.AddListener(goToInfoScreen);
        CreditsButton.onClick.AddListener(goToCredits);

        startStroke.SetActive(startSelected);

		
	}

    void Update() {
        if (Input.GetKeyDown("up") || Input.GetKeyDown("down")) {
            startSelected = !startSelected;
            startStroke.SetActive(startSelected);
            creditsStroke.SetActive(!startSelected);
        } 
        if (Input.GetKeyDown("return")) {
            if (startStroke.activeSelf) {
                nav.goToInfo();
            }
            else if (creditsStroke.activeSelf) {
                nav.goToCredits();
            }
        }
    }

    private void goToCredits()
    {
        nav.goToCredits();
    }

    private void goToInfoScreen()
    {
        nav.goToInfo();
    }




}
