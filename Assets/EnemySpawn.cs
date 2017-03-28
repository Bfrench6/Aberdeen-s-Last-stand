using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {

    //CloseCanvas script;
    public bool start;
    bool started;
    //Vector3 position;
    Vector3 spawn;

	// Use this for initialization
	void Start () {
		
        start = false;
        started = false;
        //position = transform.position;
        spawn = new Vector3(0, 5f, 0);

	}
	
	// Update is called once per frame
	void Update () {

        if (start  && !started) {
            transform.position += spawn;
            started = true;
        }

		
	}
}
