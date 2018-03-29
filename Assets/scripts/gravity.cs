using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gravity : MonoBehaviour {

	public int gravityY;
	public int gravityX;
	public int gravityZ;

	void Start () {
		gravityX = 0;
		gravityY = 1;
		gravityZ = 0;
	}

	void Update () {
		Physics.gravity = new Vector3(gravityX, gravityY, gravityZ);

		if ((Input.GetKeyDown("a") && gravityX == -1) || (Input.GetKeyDown("a") && gravityX == 0)) {
			gravityX = 1;
		} else if (Input.GetKeyDown("a") && gravityX == 1) {
			gravityX = -1;
		}
		if ((Input.GetKeyDown("s") && gravityY == -1) || (Input.GetKeyDown("s") && gravityY == 0)) {
			gravityY = 1;
		} else if (Input.GetKeyDown("s") && gravityY == 1) {
			gravityY = -1;
		}
		if ((Input.GetKeyDown("d") && gravityZ == -1) || (Input.GetKeyDown("d") && gravityZ == 0)) {
			gravityZ = 1;
		} else if (Input.GetKeyDown("d") && gravityZ == 1) {
			gravityZ = -1;
		}
		if (Input.GetKeyDown ("space")) {
			gravityX = 0;
			gravityY = 0;
			gravityZ = 0;
		}
	}
}
