using UnityEngine;
using System.Collections;

public class TakeablePowerUp : MonoBehaviour {
	CustomizablePowerUp customPowerUp;
    AudioSource clip;

	void Start() {
		customPowerUp = transform.parent.gameObject.GetComponent<CustomizablePowerUp>();
        clip = GetComponent<AudioSource>();
		clip.clip = customPowerUp.pickUpSound;
        clip.playOnAwake = false;
	}

	void OnTriggerEnter (Collider collider) {
		if(collider.tag == "Player") {
			PowerUpManager.Instance.Add(customPowerUp);
            bool newPU = true;
            //add power up to player
            collider.gameObject.AddComponent<PowerUp>();
            PowerUp[] playerPUs = collider.gameObject.GetComponents<PowerUp>();
            //set power up setting based on type
            foreach (PowerUp pu in playerPUs)
            {
                //reset duration if player already has this power up
                if (pu.GetPUType() == customPowerUp.powerUpNum)
                {
                    pu.resetDuration();
                    newPU = false;
                }
                //else set values on new power up
                if (!pu.isActive() && newPU)
                {
                    pu.SetDuration(customPowerUp.duration);
                    pu.SetPUType(customPowerUp.powerUpNum);
                    pu.SetColor(customPowerUp.lightColor);
                    pu.SetName(customPowerUp.powerUpName);
                }
                pu.Activate();
            }
            
			if(customPowerUp.pickUpSound != null){
				AudioSource.PlayClipAtPoint(customPowerUp.pickUpSound, transform.position);
			}
            //destroy visual power up
            Destroy(transform.parent.gameObject);

		}
	}
}
