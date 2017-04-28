using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StoneHealth : MonoBehaviour {

    public float regenRate = 0.75f;
    public float startingHealth = 500;                            // The amount of health the stone starts the game with.
    public float currentHealth;                                   // The current health the stone has.
    public Slider healthSlider;                                 // Reference to the UI's health bar.
    public Image damageImage;                                   // Reference to an image to flash on the screen on being hurt.
    public float flashSpeed = 10f;                               // The speed the damageImage will fade at.
    public Color flashColour = new Color(0f, 0.78f, 1f, 0.1f);     // The colour the damageImage is set to, to flash.
    public Text warningText;
    public Color warnColor = new Color(1f, 1f, 1f, 1f);


    bool isDead;                                                // Whether the stone is destroyed.
    bool damaged;                                               // True when the stone gets damaged.

    int damageTimer;
    int damageTime = 60;

    public MenuNavigation nav;

    
    void Awake () {
        // Set the initial health of the stone.
        currentHealth = startingHealth;

        regenRate /= Manager.Instance.difficultyMult;

    }
	
	
	void Update () {
        // If the stone has just been damaged...
        if (damaged)
        {
            // ... set the colour of the damageImage to the flash colour.
            warningText.color = warnColor;
            damageImage.color = flashColour;
            damageTimer = 0;
        }
        // Otherwise...
        else
        {
            // ... transition the colour back to clear.
            warningText.color = Color.Lerp(warningText.color, Color.clear, 0.25f * flashSpeed * Time.deltaTime);
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
            damageTimer++;
        }

        if (damageTimer > damageTime && !isDead)
        {
            Regen();
        }

        // Reset the damaged flag.
        damaged = false;

    }

    public void TakeDamage(float amount)
    {
        // Set the damaged flag so the screen will flash.
        damaged = true;

        // Reduce the current health by the damage amount.
        currentHealth -= amount;

        // Set the health bar's value to the current health.
        healthSlider.value = currentHealth;

        // If the stone has lost all it's health and the death flag hasn't been set yet...
        if (currentHealth <= 0 && !isDead)
        {
            // ... it should die.
            Death();
        }
    }

    void Regen()
    {
        currentHealth += regenRate * Time.deltaTime;
        if (currentHealth > startingHealth)
        {
            currentHealth = startingHealth;
        }
    }

    void Death()
    {
        // Set the death flag so this function won't be called again.
        isDead = true;

        Manager.Instance.gameOver = true;
        nav.goToScoreScreen(false);
    }

}
