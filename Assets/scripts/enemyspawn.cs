using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyspawn : MonoBehaviour {

	public Text timerText;
	public float seconds, minutes;
	public GameObject[] collectibles;
	public GameObject winText;
	private int spawnsKilled;
	private int spawnNumber;
	public bool gameOver;
	public AudioSource youWin;

	void Start () {
		Spawner ();
		spawnsKilled = 0;
		gameOver = false;
		orient.gravityMod = 10.0f;
	}
		
	void Update () {
		if (gameOver == false) {
			minutes = (int)(Time.time / 60f);
			seconds = (int)(Time.time % 60f);
			timerText.text = minutes.ToString ("00") + ":" + seconds.ToString ("00");
		}
		if (collectibles[1] == null && collectibles[2] == null && collectibles[3] == null && collectibles[4] == null && collectibles[5] == null && collectibles[6] == null && collectibles[7] == null && collectibles[8] == null && collectibles[9] == null && collectibles[10] == null && collectibles[12] == null && collectibles[11] == null && collectibles[13] == null && collectibles[14] == null && collectibles[0] == null && gameOver == false){
			gameOver = true;
			Debug.Log("Win");
			orient.gravityMod = 0.0f;
			winText.gameObject.SetActive (true);
			youWin.Play ();
		}
	}

	void Spawner () {
		if (spawnsKilled < 8) {
			spawnNumber = Random.Range (0, 15);
			Debug.Log (spawnNumber);
			if (collectibles [spawnNumber] != null) {
				Destroy (collectibles [spawnNumber]);
				spawnsKilled++;
				Debug.Log ("Nice");
			} else {
				return;
			}
			Spawner ();
		}
	}
}
