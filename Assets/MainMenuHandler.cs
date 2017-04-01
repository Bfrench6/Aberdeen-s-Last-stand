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


	// Use this for initialization
	void Start () {
        StartButton.onClick.AddListener(goToInfoScreen);
        CreditsButton.onClick.AddListener(goToCredits);

		
	}

    void Update() {
        if (Input.GetKeyDown("up") || Input.GetKeyDown("down")) {
            //startSelected = !startSelected;
            startStroke.SetActive(!startStroke.activeSelf);
            creditsStroke.SetActive(!creditsStroke.activeSelf);
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
