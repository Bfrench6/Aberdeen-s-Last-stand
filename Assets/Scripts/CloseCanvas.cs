using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseCanvas : MonoBehaviour {


    public MenuNavigation nav;
    public GameObject canvas;
    public Button button;
    public Button lengthButton;
    public Button difficultyButton;
    EnemySpawn script;
    GameObject[] enemies;

    // Use this for initialization
    void Start () {
        

        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        button.onClick.AddListener(TaskOnClick);
        if (lengthButton != null)
        {
            lengthButton.onClick.AddListener(ChangeLength);
        }
        if (difficultyButton != null)
        {
            difficultyButton.onClick.AddListener(ChangeDiff);
        }
        

    }
    
    // Update is called once per frame
    void Update () {
        
        

    }

    void TaskOnClick(){
        
        for (int i=0; i<enemies.Length; i++) {
            EnemySpawn script = enemies[i].GetComponent<EnemySpawn>();
            if (script != null)
            {
                script.start = true;
            }
            
        }
        if (nav != null)
        {
            TimerControl.StartTimer(Manager.Instance.length);
            nav.goToGame();

        }

    }

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
