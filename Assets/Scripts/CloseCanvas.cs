using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseCanvas : MonoBehaviour {
    //game info screen

    public MenuNavigation nav;
    public GameObject canvas;
    public Button button;
    public Button lengthButton;
    public Button difficultyButton;

    // Use this for initialization
    void Start () {
        
        button.onClick.AddListener(TaskOnClick);                //start gamme button
        if (lengthButton != null)
        {
            lengthButton.onClick.AddListener(ChangeLength);     //game length button
        }
        if (difficultyButton != null)
        {
            difficultyButton.onClick.AddListener(ChangeDiff);   //difficulty button
        }
    }

    void TaskOnClick(){
        
        if (nav != null)
        {
            //start timer and game
            TimerControl.StartTimer(Manager.Instance.length);
            nav.goToGame();
        }

    }
    //change game length variables
    void ChangeLength()
    {
        Text lenTextElement = lengthButton.GetComponentInChildren<Text>();
        string lenText = lenTextElement.text;
        switch (lenText)
        {
            case "Short":
                lenTextElement.text = "Medium";
                Manager.Instance.length = 2;
                break;
            case "Medium":
                lenTextElement.text = "Long";
                Manager.Instance.length = 3;
                break;
            case "Long":
                lenTextElement.text = "Short";
                Manager.Instance.length = 1;
                break;
        }
    }
    //change difficulty settings
    void ChangeDiff()
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
}
