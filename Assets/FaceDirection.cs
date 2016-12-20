using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceDirection : AbstractBehavior {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		var right = inputState.GetButtonValue (inputButtons[(int)Buttons.Right]);
		var left = inputState.GetButtonValue (inputButtons[(int)Buttons.Left]);

		if (right) {
			inputState.facingDir = FacingDirections.Right;
		} else if (left) {
			inputState.facingDir = FacingDirections.Left;
		}

		transform.localScale = new Vector3 ((float)inputState.facingDir, 1, 1);
	}
}
