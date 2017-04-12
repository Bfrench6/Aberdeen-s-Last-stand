using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseCanvas : MonoBehaviour {


    public MenuNavigation nav;
    public GameObject canvas;
    public Button button;
    EnemySpawn script;
    GameObject[] enemies;

    // Use this for initialization
    void Start () {
        

        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        button.onClick.AddListener(TaskOnClick);

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
            nav.goToGame();

        }

    }


}
