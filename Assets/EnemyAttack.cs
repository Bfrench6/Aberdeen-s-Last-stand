using UnityEngine;
using System.Collections;


public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;     // The time in seconds between each attack.
    public float attackDamage = 10;               // The amount of health taken away per attack.


    Animator anim;                              // Reference to the animator component.
    GameObject player;                          // Reference to the player GameObject.
    GameObject stone;                           //Reference to the stone GameObject
    PlayerHealth playerHealth;                  // Reference to the player's health.
    StoneHealth stoneHealth;                    // Reference to the stones health
    EnemyHealth enemyHealth;                    // Reference to this enemy's health.
    public bool playerInRange;                         // Whether player is within the trigger collider and can be attacked.
    bool stoneInRange;                          // Whether stone is within the trigger collider and can be attacked.
    float timer;                                // Timer for counting up to the next attack.


    void Awake()
    {
        // Setting up the references.
        player = GameObject.FindGameObjectWithTag("Player");
        stone = GameObject.FindGameObjectWithTag("Stone");
        attackDamage *= Manager.Instance.difficultyMult;
        playerHealth = player.GetComponent<PlayerHealth>();
        stoneHealth = stone.GetComponent<StoneHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent<Animator>();
    }


    void OnTriggerEnter(Collider other)
    {
        // If the entering collider is the player...
        if (other.gameObject == player)
        {
            // ... the player is in range.
            playerInRange = true;
        }
        if (other.gameObject == stone)
        {
            stoneInRange = true;
        }
    }


    void OnTriggerExit(Collider other)
    {
        // If the exiting collider is the player...
        if (other.gameObject == player)
        {
            // ... the player is no longer in range.
            playerInRange = false;
        }
        if (other.gameObject == stone)
        {
            stoneInRange = false;
        }
    }


    void Update()
    {
        // Add the time since Update was last called to the timer.
        timer += Time.deltaTime;

    
     // If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
        if(timer >= timeBetweenAttacks && enemyHealth.currentHealth > 0)
        {
            if(playerInRange)
            {
                Attack(player);
            }
            else if (stoneInRange)
            {
                Attack(stone);
            }
            else 
            {
                anim.SetBool("Attack", false);
            }
            // ... attack.
        }

        // If the player has zero or less health...
        if(playerHealth.currentHealth <= 0)
        {
            // ... tell the animator the player is dead.
            anim.SetTrigger ("PlayerDead");
        }

    }


    void Attack(GameObject thing)
    {
        // Reset the timer.
        timer = 0f;
        if (thing == player)
        {
            // If the player has health to lose...
            if (playerHealth.currentHealth > 0)
            {
                // ... damage the player.
                playerHealth.TakeDamage(attackDamage);
            }
        }
        else if (thing == stone)
        {
            // If the stone has health to lose...
            if (stoneHealth.currentHealth > 0)
            {
                // ... damage the player.
                stoneHealth.TakeDamage(attackDamage);
            }
        }
        anim.SetBool("Attack", true);
        
    }
}