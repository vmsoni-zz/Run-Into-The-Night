using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float moveSpeed;
	public float jumpForce;
	public float jumpTime;
	public bool grounded;
	public LayerMask whatIsGround;
	public float speedMultiplier;
	public float speedIncreaseMilestone;
	public Transform groundCheck;
	public float groundCheckRadius;
	public GameManager theGameManager;

	private bool canDoubleJump;
	private bool stoppedJumping;
	private float speedIncreaseMilestoneStore;
	private float moveSpeedStore;
	private float speedMilestoneCountStore;
	private float speedMilestoneCount;
	private Rigidbody2D myRigidBody;
	private Animator myAnimator;
	private float jumpTimeCounter;
	// Use this for initialization
	void Start () {
		myRigidBody = GetComponent<Rigidbody2D> ();
		myAnimator = GetComponent<Animator> ();
		jumpTimeCounter = jumpTime;
		speedMilestoneCount = speedIncreaseMilestone;
		moveSpeedStore = moveSpeed;
		speedMilestoneCountStore = speedMilestoneCount;
		speedIncreaseMilestoneStore = speedIncreaseMilestone;
		stoppedJumping = true;
	}
	
	// Update is called once per frame
	void Update () {

		//grounded = Physics2D.IsTouchingLayers (myCollider, whatIsGround);
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

		if (transform.position.x > speedMilestoneCount) {
			speedMilestoneCount += speedIncreaseMilestone;
			speedIncreaseMilestone += speedIncreaseMilestone * speedMultiplier;
			moveSpeed = moveSpeed * speedMultiplier;
		}
			
		myRigidBody.velocity = new Vector2 (moveSpeed, myRigidBody.velocity.y);

		if (Input.GetKeyDown (KeyCode.Space)) {
			if (grounded) {
				myRigidBody.velocity = new Vector2(moveSpeed,jumpForce);
				stoppedJumping = false;
			}
			if (!grounded && canDoubleJump) {
				myRigidBody.velocity = new Vector2(moveSpeed,jumpForce);
				canDoubleJump = false;
				stoppedJumping = false;
				jumpTimeCounter = jumpTime;
			}
		}

		if (Input.GetKeyUp (KeyCode.Space)) {
			jumpTimeCounter = 0;
			stoppedJumping = true;
		}
	
		for (int i = 0; i < Input.touchCount; ++i) {
			if (Input.GetTouch (i).phase == TouchPhase.Began) {
				if (grounded) {
					myRigidBody.velocity = new Vector2(moveSpeed,jumpForce);
					stoppedJumping = false;
				}
				if (!grounded && canDoubleJump) {
					myRigidBody.velocity = new Vector2(moveSpeed,jumpForce);
					canDoubleJump = false;
					stoppedJumping = false;
					jumpTimeCounter = jumpTime;
				}
			}
			if (Input.GetTouch (i).phase == TouchPhase.Stationary && !stoppedJumping) {
				if (jumpTimeCounter > 0) {
					myRigidBody.velocity = new Vector2(moveSpeed,jumpForce);
					jumpTimeCounter -= Time.deltaTime;
				}
			}
			if (Input.GetTouch (i).phase == TouchPhase.Ended) {
				jumpTimeCounter = 0;
				stoppedJumping = true;
			}
		}

		if (Input.GetKey (KeyCode.Space) && !stoppedJumping) {
			if (jumpTimeCounter > 0) {
				myRigidBody.velocity = new Vector2(moveSpeed,jumpForce);
				jumpTimeCounter -= Time.deltaTime;
			}
		}

		if (grounded) {
			jumpTimeCounter = jumpTime;
			canDoubleJump = true; 
		}
			

		myAnimator.SetFloat ("Speed", myRigidBody.velocity.x);
		myAnimator.SetBool ("Grounded", grounded);
	}

	void OnCollisionEnter2D (Collision2D other){
		if (other.gameObject.tag == "killbox") {
			theGameManager.RestartGame();
			moveSpeed = moveSpeedStore;
			speedMilestoneCount = speedMilestoneCountStore;
			speedIncreaseMilestone = speedIncreaseMilestoneStore;
		}
	}
}
