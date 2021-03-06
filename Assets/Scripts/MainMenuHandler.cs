﻿//Ben French, Chuan Yui, Pranav Bhardwaj

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
        startStroke.SetActive(false);
        creditsStroke.SetActive(false);
	}

    void Update() {
        if (Input.GetKeyDown("up") || Input.GetKeyDown("down")) {
            //if neither active, pick one
            if (!startStroke.activeSelf && !creditsStroke.activeSelf)
            {
                if (Input.GetKeyDown("up"))
                {
                    startStroke.SetActive(true);
                }
                else
                {
                    creditsStroke.SetActive(true);
                }
            }
            //if one active, swap them
            else
            {
                startStroke.SetActive(!startStroke.activeSelf);
                creditsStroke.SetActive(!creditsStroke.activeSelf);
            }
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
