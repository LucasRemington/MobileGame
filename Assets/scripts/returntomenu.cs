using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class returntomenu : MonoBehaviour {

	public AudioSource buttonClick;

	public void onClick () {
		SceneManager.LoadScene ("menu");
		//buttonClick.Play ();
	}

	public void restartMaze () {
		SceneManager.LoadScene ("rotatetest");
		//buttonClick.Play ();
	}
}
