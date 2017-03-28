using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuHandler : MonoBehaviour {

    public MenuNavigation nav;

    public Button StartButton;
    public Button CreditsButton;


	// Use this for initialization
	void Start () {
        StartButton.onClick.AddListener(goToInfoScreen);
        CreditsButton.onClick.AddListener(goToCredits);
        
		
	}

    private void goToCredits()
    {
        nav.goToCredits();
    }

    private void goToInfoScreen()
    {
        nav.goToInfo();
    }

    // Update is called once per frame
    void Update () {
		
	}




}
