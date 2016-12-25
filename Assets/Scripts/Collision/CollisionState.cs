using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionState : MonoBehaviour {

	[Header("Layer to Check Collision")]
	public LayerMask collisionLayer;

	[Header("Coordinate(s) to Check for Collision")]
	public Vector2[] bottomPosition;
	[Header("Boolean(s) to Store Collision Data")]
	public bool[] colliderCondition;
	public bool isStanding;

	[Space(10)]
	public float collisionRadius = 0.5f;
	public Color debugCollisionColor = Color.red;

	void Update () {
		isStanding = IsPlayerStanding ();
	}

	void FixedUpdate() {
		
		for (var i = 0; i < bottomPosition.Length; i++) {
			var pos = bottomPosition[i];
			pos.x += transform.position.x;
			pos.y += transform.position.y;
			colliderCondition[i] = Physics2D.OverlapCircle (pos, collisionRadius, collisionLayer);
		}
	}

	void OnDrawGizmos() {
		Gizmos.color = debugCollisionColor;

		for (var i = 0; i < bottomPosition.Length; i++) {
			var pos = bottomPosition[i];
			pos.x += transform.position.x;
			pos.y += transform.position.y;
			Gizmos.DrawWireSphere (pos, collisionRadius);
		}
	}

	bool IsPlayerStanding() {
		for (var i = 0; i < colliderCondition.Length; i++) {
			if (colliderCondition [i]) {
				return true;
			}
		}
		return false;
	}
}
