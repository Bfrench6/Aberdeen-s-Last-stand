//provided by power up particles asset

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PowerUpRotation : MonoBehaviour
{

	#region Settings
	public float rotationSpeed = 99.0f;
	public bool reverse = false;
	#endregion

	void Update ()
	{
		if(reverse)
			//transform.Rotate(Vector3.back * Time.deltaTime * this.rotationSpeed);
			transform.Rotate(new Vector3(0f,0f,1f) * Time.deltaTime * rotationSpeed);
		else
			//transform.Rotate(Vector3.forward * Time.deltaTime * this.rotationSpeed);
			transform.Rotate(new Vector3(0f,0f,1f) * Time.deltaTime * rotationSpeed);
	}

	public void SetRotationSpeed(float speed)
	{
        rotationSpeed = speed;
	}

	public void SetReverse(bool reverse)
	{
		this.reverse = reverse;
	}
}
