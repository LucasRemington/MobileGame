using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orient : MonoBehaviour {

	public static float gravityMod = 15.0f;

	public AudioSource pickUp;
	//public AudioSource wallHit;

	void Update () {
		Physics.gravity = new Vector3((gravityMod*Input.acceleration.x), 0, (gravityMod*Input.acceleration.y)); 
	}

	void OnTriggerEnter (Collider col) {
		if (col.gameObject.tag == "Collect") {
			Destroy (col.gameObject);
			pickUp.Play ();

		}
	}
}
