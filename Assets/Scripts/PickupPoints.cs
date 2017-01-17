using UnityEngine;
using System.Collections;

public class PickupPoints : MonoBehaviour {

	public int scoreToGive;
	public GameObject thePlayer;
	private PowerupManager thePowerupManager;
	public float distance;
	private ScoreManager theScoreManager;
	public float coinSpeed;


	// Use this for initialization
	void Start () {
		thePowerupManager = FindObjectOfType<PowerupManager> ();
		theScoreManager = FindObjectOfType<ScoreManager> ();
		thePlayer = FindObjectOfType<PlayerController> ().gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if((Vector3.Distance(transform.position, thePlayer.transform.position) < distance) && thePowerupManager.coinMagnet)
		{
			transform.position = Vector3.MoveTowards(transform.position, thePlayer.transform.position, Time.deltaTime * coinSpeed);
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.name == "Player") {
			theScoreManager.AddScore(scoreToGive);
			gameObject.SetActive (false);
		}
	}

}
