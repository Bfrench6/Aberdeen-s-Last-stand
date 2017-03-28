using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseCanvas : MonoBehaviour {

    public bool close;
    public GameObject canvas;
    public Button button;
    EnemySpawn script;
    GameObject[] enemies;

    // Use this for initialization
    void Start () {
        
        close = false;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

    }
    
    // Update is called once per frame
    void Update () {
        
        canvas.SetActive(!close);
        button.onClick.AddListener(TaskOnClick);

    }

    void TaskOnClick(){
        close = true;
        for (int i=0; i<enemies.Length; i++) {
            EnemySpawn script = enemies[i].GetComponent<EnemySpawn>();
            script.start = true;
        }
        
    }

}
