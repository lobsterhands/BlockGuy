using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractBehavior : MonoBehaviour {

	public Buttons[] inputButtons;

	protected InputState inputState;
	protected Rigidbody2D body2d;

	protected virtual void Awake() {
		inputState = GetComponent<InputState> ();
		body2d = GetComponent<Rigidbody2D> ();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
