using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	[HideInInspector] public bool isJumping = false;

	[Header("Movement Variables")]
	public float moveForce;
	public float maxSpeed;
	public float jumpForce;
	public float airResistance;
	public float gravity;

	[Header("Ground Checkers")]
	public Transform groundCheckLeft;
	public Transform groundCheckRight;

	private bool groundedLeft = false;
	private bool groundedRight = false;
	private bool grounded = false;

	private bool facingRight = true;
	private Rigidbody2D rb2d;
	private SpriteRenderer sprite;

	void Awake () {
		rb2d = GetComponent<Rigidbody2D> ();
		sprite = GetComponent<SpriteRenderer> ();
	}

	// Update is called once per frame
	void Update () {
		grounded = IsPlayerGrounded ();
		if (Input.GetButtonDown ("Jump") && grounded) {
			isJumping = true;
		}
	}

	void FixedUpdate() {

		float moveX = Input.GetAxis ("Horizontal");

		if (grounded) {
			AddHorizontalGroundForce (moveX);
		} else {
			AddHorizontalAirForce (moveX);
		}

		if ((moveX > 0 && !facingRight) || (moveX < 0 && facingRight)) {
			FlipDirectionX ();
		}

		if (Mathf.Abs (rb2d.velocity.x) > maxSpeed) {
			ClampMaxSpeed ();
		}

		if (isJumping) {
			rb2d.AddForce (new Vector2 (0f, jumpForce));
			isJumping = false;
		}
	}

	void AddHorizontalGroundForce(float forceX) {
		if (forceX * rb2d.velocity.x < maxSpeed) {
			rb2d.AddForce (Vector2.right * forceX * moveForce);
		}
	}

	void AddHorizontalAirForce(float forceX) {
		if (forceX * rb2d.velocity.x < maxSpeed) {
			rb2d.AddForce (Vector2.right * forceX * (moveForce / airResistance));
			rb2d.AddForce (Vector2.down * gravity);
		}
	}

	void ClampMaxSpeed() {
		rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);
	}

	void FlipDirectionX() {
		facingRight = !facingRight;
		sprite.flipX = !sprite.flipX;
	}

	bool IsPlayerGrounded() {
		groundedLeft = Physics2D.Linecast (transform.position, groundCheckLeft.position, 1 << LayerMask.NameToLayer ("Ground"));
		groundedRight = Physics2D.Linecast(transform.position, groundCheckRight.position, 1 << LayerMask.NameToLayer("Ground"));
		return (groundedLeft || groundedRight);
	}
}
