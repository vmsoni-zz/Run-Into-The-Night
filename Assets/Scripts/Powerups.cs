using UnityEngine;
using System.Collections;

public class Powerups : MonoBehaviour {

	public bool doublePoints;
	public bool safeMode;
	public bool coinMagnet;
	private ParticleSystem psystem;
	public float powerupLength;

	private PowerupManager thePowerupManager;
	public Sprite[] powerupSprites;

	// Use this for initialization
	void Start () {
		thePowerupManager = FindObjectOfType<PowerupManager> ();
	}


	void Awake () {
		psystem = this.transform.parent.gameObject.GetComponent<ParticleSystem> ();
		int powerupSelector = Random.Range (0, 3);
		GetComponent<SpriteRenderer>().sprite = powerupSprites[powerupSelector];
		if (powerupSelector == 0) {
			doublePoints = true;
		} else if (powerupSelector == 1) {
			psystem.startColor = new Color(34, 0, 169, 255);
			safeMode = true;
		} else if (powerupSelector == 2) {
			psystem.startColor = new Color(223, 125, 0, 255);
			coinMagnet = true;
		}
	}

	void OnTriggerEnter2D (Collider2D other){
		if (other.name == "Player") {
			psystem.Play ();
			thePowerupManager.ActivatePowerup (doublePoints, safeMode, coinMagnet, powerupLength);
			gameObject.SetActive(false);
		}
	}
}
