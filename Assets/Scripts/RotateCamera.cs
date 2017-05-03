//Ben French, Chuan Yui, Pranav Bhardwaj

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour {

	public GameObject target;
	[SerializeField] private float speed = 2f;
	[SerializeField] private int degree = 10;
	private Vector3 point;

	void Start () {
		point = target.transform.position;
		transform.LookAt(point);
	}

	void Update () {
		//rotating around its Y axis, theta degrees per second times the speed
		transform.RotateAround (point, Vector3.up, degree * Time.deltaTime * speed);
	}
}
