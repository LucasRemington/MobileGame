using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gamecontrol : MonoBehaviour {

	public static bool hardMode = false;
	public GameObject playerCamera;
	public GameObject lockedCamera;
	public AudioSource snailsHouse;
	public AudioSource buttonClick;
	public GameObject hardButton;

	void Start () {
		FindCamera ();
		snailsHouse.Play ();
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}

	void OnLevelWasLoaded () {
		FindCamera ();
		Debug.Log ("LoadCamera");
		if (hardMode == true) {
			hardButton.GetComponent<Image>().color = Color.red;
			buttonClick.Play ();
		} 
	}

	void Awake () {
		DontDestroyOnLoad (this.gameObject);
	}

	void Update () {
	}

	public void ActivateHardMode () {
		if (hardMode == false) {
			hardMode = true;
			hardButton.GetComponent<Image>().color = Color.red;
		} else { 
			hardMode = false;
			hardButton.GetComponent<Image>().color = Color.white;
		}
		buttonClick.Play ();

	}

	public void LoadMaze () {
		buttonClick.Play ();
		SceneManager.LoadScene ("rotatetest");
	}

	void FindCamera () {
		playerCamera = GameObject.Find("PlayerCamera");
		lockedCamera = GameObject.Find("LockedCamera");
		if (hardMode == true) {
			playerCamera.gameObject.SetActive(true);
			lockedCamera.gameObject.SetActive(false);
			Debug.Log ("hard");
		} else { 
			playerCamera.gameObject.SetActive(false);
			lockedCamera.gameObject.SetActive(true);
			Debug.Log ("normal");
		}
		Debug.Log ("Camera");
	}

}
