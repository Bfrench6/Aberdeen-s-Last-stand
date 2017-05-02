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
            collider.gameObject.AddComponent<PowerUp>();
            PowerUp[] playerPUs = collider.gameObject.GetComponents<PowerUp>();
            
            foreach (PowerUp pu in playerPUs)
            {
                if (pu.GetPUType() == customPowerUp.powerUpNum)
                {
                    pu.resetDuration();
                    newPU = false;
                }
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

            Destroy(transform.parent.gameObject);

		}
	}
}
