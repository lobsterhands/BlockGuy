using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSmash : AbstractBehavior {

	public ParticleSystem smashParticle;


	private float pauseTime = 1f;
	private float thrust = 500f;
	private	bool justSmashed;

	// Update is called once per frame
	void Update () {

		var down = inputState.GetButtonValue (inputButtons [0]);
		var jump = inputState.GetButtonValue (inputButtons [1]);
		var jumpHoldTime = inputState.GetButtonHoldTime (inputButtons [1]);


		if (!collisionState.isStanding && !justSmashed) {
			if (down && jump && jumpHoldTime < .01f) {
				justSmashed = true;
				StartCoroutine(DoSmashThing ());
			}
		}

		if (collisionState.isStanding && justSmashed) {
			justSmashed = false;

			Vector3 pos = body2d.transform.position;
			pos.y -= 1f;
			smashParticle.transform.position = new Vector3 (pos.x, pos.y, pos.z);

		}
	}

	IEnumerator DoSmashThing() {
		ToggleSimulate ();
		smashParticle = ParticleSystem.Instantiate (smashParticle);
		smashParticle.transform.position = body2d.transform.position;
		smashParticle.Play (true);
		yield return new WaitForSeconds (pauseTime);
		ToggleSimulate ();
		body2d.AddForce (Vector2.down * thrust);
		smashParticle.transform.position = body2d.transform.position;
		smashParticle.Play ();
	}

	void ToggleSimulate() {
		body2d.simulated = !body2d.simulated;
	}
}