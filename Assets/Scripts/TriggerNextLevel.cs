using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerNextLevel : MonoBehaviour {

	LevelLoader levelLoader;

	// Use this for initialization
	void Start () {
		levelLoader = GetComponent<LevelLoader> ();
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			SceneManager.LoadScene (LevelLoader.getLevel(), LoadSceneMode.Single);
		}
	}
}
