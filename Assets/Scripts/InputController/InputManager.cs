using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Buttons {
	Right,
	Left,
	Up,
	Down,
	A,
	B
}

public enum Comparison { 
	GreaterThan,
	LessThan
}

[System.Serializable]
public class InputAxisState {
	public string axisName;
	public float offValue;
	public Buttons button;
	public Comparison comparison;

	public bool isBeingPressed {
		get {
			var val = Input.GetAxis (axisName);

			switch (comparison) {
			case Comparison.GreaterThan:
				return val > offValue;
			case Comparison.LessThan:
				return val < offValue;
			default:
				return false;
			}
		}
	}
}

public class InputManager : MonoBehaviour {
	
	public InputAxisState[] inputs;
	public InputState inputState;
		
	void Update () {
		if (inputs != null) {
			foreach (var input in inputs) {
				inputState.SetButtonState (input.button, input.isBeingPressed);
			}
		} else {
			Debug.Log ("No input found.");
		}

	}
}
