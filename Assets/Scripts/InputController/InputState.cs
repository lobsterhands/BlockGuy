using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonState {
	public bool isActive;
	public float holdTime = 0f;
}

public enum FacingDirections{
	Right = 1,
	Left = -1
}

public class InputState : MonoBehaviour {

	public FacingDirections facingDir = FacingDirections.Right;
	Dictionary<Buttons, ButtonState> buttonStates= new Dictionary<Buttons, ButtonState>();

	public void SetButtonState(Buttons button, bool isBeingPressed) {
		if (!buttonStates.ContainsKey (button)) {
			buttonStates.Add (button, new ButtonState ());
		}

		var bState = buttonStates [button];

		if (bState.isActive && !isBeingPressed) {
			bState.holdTime = 0f;
		} else if (bState.isActive && isBeingPressed) {
			bState.holdTime += Time.deltaTime;
		}

		bState.isActive = isBeingPressed;
	}

	public bool GetButtonValue(Buttons button) {
		if (buttonStates.ContainsKey (button)) {
			return buttonStates [button].isActive;
		} else {
			return false;
		}
	}

	public float GetButtonHoldTime(Buttons button) {
		if (buttonStates.ContainsKey (button)) {
			return buttonStates [button].holdTime;
		} else {
			return 0;
		}
	}
}
