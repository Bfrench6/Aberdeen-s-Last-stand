using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    private float speed = 10f;
    public bool fired = false;
    public int damagePerShot = 20;

    private bool selfDestructing;
    private int destructTimer;
    private int destructTime = 60;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
        if(selfDestructing)
        {
//            Debug.Log("Destroying arrow");
            destructTimer++;
        }
        if (fired && destructTimer < 3)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        if (destructTimer > destructTime)
        {
            selfDestruct();
        }
        
        
		
	}

    void FixedUpdate()
    {
        if(transform.parent) {
            transform.position = transform.parent.position;
            transform.Translate(new Vector3(0, 0.35f, 0.05f), transform.parent);
            transform.localEulerAngles = new Vector3(277f, 335f, 0f);
        }

    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("collision" + other.tag);
        if (other.tag == "Enemy")
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                Debug.Log("damage");
                enemyHealth.TakeDamage(damagePerShot);
            }
            selfDestruct();
        } else if (other.tag != "Player")
        {
            selfDestruct();
        }
    }

    public void selfDestruct()
    {
        Destroy(transform.gameObject);
    }

}
