using UnityEngine;
using System.Collections;

public class ArrowMovement : MonoBehaviour {

	private Rigidbody2D arrowRigidBody;
	public float moveSpeed;

	// Use this for initialization
	void Start () {
		arrowRigidBody = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		arrowRigidBody.velocity = new Vector2 (moveSpeed, arrowRigidBody.velocity.y);
	}

	void OnCollisionEnter2D (Collision2D other){
		if (other.gameObject.tag == "killbox" || other.gameObject.tag == "ground") {
			gameObject.SetActive (false);
		}
	}
}
