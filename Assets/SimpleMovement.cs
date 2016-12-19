using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovement : MonoBehaviour {

	[Header("Movement Variables")]
	public float speed = 5f;
	public Buttons[] input;
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
	private Rigidbody2D body2d;
	private InputState inputState;
	private SpriteRenderer spriteRenderer;

	[HideInInspector] 
	public bool isJumping = false;

	// Use this for initialization
	void Start () {
		body2d = GetComponent<Rigidbody2D> ();
		inputState = GetComponent<InputState> ();
		spriteRenderer = GetComponent<SpriteRenderer> ();

		jumpForce = 400f;
	}
	
	// Update is called once per frame
	void Update () {

		var right = inputState.GetButtonValue (input[0]);
		var left = inputState.GetButtonValue (input[1]);
		var velX = speed;

		if (right || left) {
			velX *= left ? -1 : 1;
		} else {
			velX = 0;
		}
		body2d.velocity = new Vector2 (velX, body2d.velocity.y);
			
		if ((velX > 0 && !facingRight) || (velX < 0 && facingRight)) {
			FlipDirectionX ();
		}
			
		if (Input.GetButtonDown ("Jump") && IsPlayerGrounded()) {
			isJumping = true;
			Debug.Log ("Jumping");
		}
	}

	void FixedUpdate() {
		if (isJumping) {
			body2d.AddForce (new Vector2 (0f, jumpForce));
			isJumping = false;
		}
	}


	void FlipDirectionX() {
		facingRight = !facingRight;
		spriteRenderer.flipX = !spriteRenderer.flipX;
	}

	bool IsPlayerGrounded() {
		groundedLeft = Physics2D.Linecast (transform.position, groundCheckLeft.position, 1 << LayerMask.NameToLayer ("Ground"));
		groundedRight = Physics2D.Linecast(transform.position, groundCheckRight.position, 1 << LayerMask.NameToLayer("Ground"));
		return (groundedLeft || groundedRight);
	}
}
