using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    //public GameObject dest;
    public UnityEngine.AI.NavMeshAgent nav;
    public Animator anim;
    public Transform target;
	public Transform player;
    EnemyHealth enemyHealth;
    Vector2 smooth = Vector2.zero;
    Vector2 velocity = Vector2.zero;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
		target = GameObject.FindGameObjectWithTag ("Stone").transform;
        anim = GetComponent<Animator>();
        enemyHealth = GetComponent<EnemyHealth>();
    }

	// Use this for initialization
	void Start () {
        
        nav.updatePosition = false;
        
	}
	
	// Update is called once per frame
	void Update () {
        if(!Manager.Instance.isPaused && !enemyHealth.isDead && !Manager.Instance.gameOver)
        {
			if (enemyHealth.currentHealth < (enemyHealth.startingHealth* Manager.Instance.difficultyMult))
				nav.destination = player.position;
			else
            	nav.destination = target.position;
            nav.Resume();
            updateAnimatorCoordinates();
        } else if (nav.isActiveAndEnabled)
        {
            nav.Stop();
        }


		
	}

    void OnAnimatorMove () {
        // Update position to agent position
        transform.position = nav.nextPosition;
    
    }

    void updateAnimatorCoordinates() {
        //Calculate x and z direction of animation:
        Vector3 deltaWorldPos = nav.nextPosition - transform.position;
        //Debug.Log(deltaWorldPos);
        //map world coordinates to space
        float dx = Vector3.Dot(transform.right, deltaWorldPos);
        float dy = Vector3.Dot(transform.forward, deltaWorldPos);
        Vector2 deltaPos = new Vector2 (dx, dy);
        //filter movement change
        float smoothMove = Mathf.Min(1.0f, Time.deltaTime/0.15f);
        smooth = Vector2.Lerp(smooth, deltaPos, smoothMove);
        //update velocity
        if (Time.deltaTime > 1e-5f) {
            velocity = smooth/Time.deltaTime;
        }

        //Debug.Log(velocity.x);
        anim.SetFloat ("velx", velocity.x);
        anim.SetFloat ("velz", velocity.y);
    }
}
