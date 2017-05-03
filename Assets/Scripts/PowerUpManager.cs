//Ben French, Chuan Yui, Pranav Bhardwaj

using UnityEngine;
using System.Collections.Generic;

public class PowerUpManager : Singleton<PowerUpManager>
{
	private PowerUpManager() {}

	public CustomizablePowerUp curPowerUp;
	public CustomizablePowerUp prevPowerUp;
    private GameObject player;
    private GameObject stone;
	
	void Awake() {
        
        player = GameObject.FindGameObjectWithTag("Player");
        stone = GameObject.FindGameObjectWithTag("Stone");
	}

    public void Add(CustomizablePowerUp powerUp)
    {
        //power up player based on type
        switch (powerUp.powerUpNum)
        {
            case ((int)Manager.powerType.damage):
                Manager.Instance.doubleDamage = true;
                break;
            case ((int)Manager.powerType.points):
                Manager.Instance.doublePoints = true;
                break;
            case ((int)Manager.powerType.health):
                player.GetComponent<PlayerHealth>().GainHealth(Manager.Instance.HealthPowerUp);
                break;
            case ((int)Manager.powerType.moveSpeed):
                Manager.Instance.movePU = true;
                break;
            case ((int)Manager.powerType.stoneHealth):
                stone.GetComponent<StoneHealth>().GainHealth(Manager.Instance.StoneHealthPU);
                break;
        }
        if (curPowerUp)
        {
            prevPowerUp = curPowerUp;
        }
        else
        {
            prevPowerUp = powerUp;
        }
        curPowerUp = powerUp;
    }
}