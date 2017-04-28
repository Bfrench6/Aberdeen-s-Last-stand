using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public TimerControl timer;
    public PlayerHealth playerHealth;       // Reference to the player's heatlh.
    public GameObject enemy;                // The enemy prefab to be spawned.
    public float spawnTime = 3f;            // How long between each spawn.
    private float curSpawnTime;
    public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.



    void Start()
    {
        // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
        curSpawnTime = spawnTime;
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }


    void Spawn()
    {
        if (!Manager.Instance.isPaused && !Manager.Instance.gameOver)
        {
            
            if (timer.getCurTime() < timer.getTotalTime() / 4 && curSpawnTime != spawnTime * 0.5f)
            {
                curSpawnTime = spawnTime * 0.5f; 
                changeSpawnTime();
            }
            else if (timer.getCurTime() < timer.getTotalTime() / 2 && curSpawnTime != spawnTime * (2.0f / 3))
            {
                curSpawnTime = spawnTime * (2.0f / 3);
                changeSpawnTime();
            }
            // If the player has no health left...
            if (playerHealth.currentHealth <= 0f)
            {
                // ... exit the function.
                return;
            }

            // Find a random index between zero and one less than the number of spawn points.
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);

            // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
            Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        }
    }

    void changeSpawnTime()
    {
        CancelInvoke("Spawn");
        InvokeRepeating("Spawn", curSpawnTime, curSpawnTime);
    }

    public void stopSpawn()
    {
        CancelInvoke("Spawn");
    }

    public bool allDead()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if(enemies.Length > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
