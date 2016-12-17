using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResetLevel : MonoBehaviour {

	public Button resetButton;

	void Awake() {
		resetButton = GetComponent<Button> ();
		resetButton.onClick.AddListener (ResetScene);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

	void ResetScene() {
		Scene activeScene = SceneManager.GetActiveScene();
		SceneManager.LoadScene (activeScene.name, LoadSceneMode.Single);
	}
}
