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

		var right = inputState.GetButtonValue (input[(int)Buttons.Right]);
		var left = inputState.GetButtonValue (input[(int)Buttons.Left]);
		var velX = speed;

		if (right || left) {
			velX *= left ? -1 : 1;
		} else {
			velX = 0;
		}
		body2d.velocity = new Vector2 (velX, body2d.velocity.y);

		if (Input.GetButtonDown ("Jump") && IsPlayerGrounded()) {
			isJumping = true;
		}
	}

	void FixedUpdate() {
		if (isJumping) {
			body2d.AddForce (new Vector2 (0f, jumpForce));
			isJumping = false;
		}
	}

	bool IsPlayerGrounded() {
		groundedLeft = Physics2D.Linecast (transform.position, groundCheckLeft.position, 1 << LayerMask.NameToLayer ("Ground"));
		groundedRight = Physics2D.Linecast(transform.position, groundCheckRight.position, 1 << LayerMask.NameToLayer("Ground"));
		return (groundedLeft || groundedRight);
	}
}
