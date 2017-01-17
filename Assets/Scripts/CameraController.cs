using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	private PlayerController thePlayer;

	private Vector3 lastPlayerPosition;
	private float distanceToMove;

	// Use this for initialization
	void Start () {
		thePlayer = FindObjectOfType<PlayerController> ();
		lastPlayerPosition = thePlayer.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		distanceToMove = thePlayer.transform.position.x - lastPlayerPosition.x;
		lastPlayerPosition = thePlayer.transform.position;
		transform.position = new Vector3 (transform.position.x + distanceToMove, transform.position.y, transform.position.z);	
	}
}
