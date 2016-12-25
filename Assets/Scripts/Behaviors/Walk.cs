using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : AbstractBehavior {

	public float speed = 5f;
	public float runMultiplier = 2f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		var right = inputState.GetButtonValue (inputButtons [0]);
		var left = inputState.GetButtonValue (inputButtons [1]);
		var run = inputState.GetButtonValue (inputButtons [2]);

		var velX = speed * (float)inputState.facingDir;

		if (run & runMultiplier > 0) {
			velX = speed * runMultiplier * (float)inputState.facingDir;
		}

		if (!right && !left) {
			velX = 0;
		}

		body2d.velocity = new Vector2 (velX, body2d.velocity.y);
	}
}
