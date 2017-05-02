using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    private float speed = 16f;
    public bool fired = false;
    public int damagePerShot = 200;
    public AudioClip clip;
    
    private int destructTimer;
    private bool selfDestructing;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        
        
        if (fired && !selfDestructing)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
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
        if (!other.isTrigger)
        {
            if (other.tag == "Enemy" && fired)
            {
                EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(Manager.Instance.doubleDamage ? damagePerShot * 2 : damagePerShot, transform.position + transform.forward/2);
                }
                selfDestruct(0);
            }
            else if (other.tag != "Player" && fired)
            {
                AudioSource.PlayClipAtPoint(clip, transform.position, Manager.Instance.FXVol);
                selfDestruct(1f);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        //if enemy is on top of player while shooting
        if(other.tag == "Enemy" && fired)
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damagePerShot, transform.position + transform.forward / 2);
            }
            selfDestruct(0);
        }
    }

    public void selfDestruct(float time)
    {
        selfDestructing = true;
        Destroy(transform.gameObject, time);
    }

    public void Fire()
    {
        if (transform != null)
        {
            transform.SetParent(null);
        }
        
        fired = true;
    }
}
