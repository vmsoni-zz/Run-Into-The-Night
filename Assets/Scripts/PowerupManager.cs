using UnityEngine;
using System.Collections;

public class PowerupManager : MonoBehaviour {

	public bool doublePoints;
	public bool safeMode;
	public bool coinMagnet;
	private bool fiftyPowerups = false;

	private bool powerupActive;
	private float powerupLengthCounter=5;

	private ScoreManager theScoreManager;
	private PlatformGenerator thePlatformGenerator;
	private GameManager theGameManager;
	private PickupPoints theCoins;

	private float normalPointsPerSecond;
	private float spikeRate;
	public int powerupCount;

	private PlatformDestroyer[] spikeList;


	// Use this for initialization
	void Start () {
		theScoreManager = FindObjectOfType<ScoreManager> ();
		thePlatformGenerator = FindObjectOfType<PlatformGenerator> ();
		theGameManager = FindObjectOfType<GameManager>();
		if (PlayerPrefs.HasKey("powerupsCount")) {
			powerupCount = PlayerPrefs.GetInt ("powerupsCount");
		}

		normalPointsPerSecond = theScoreManager.pointsPerSecond;
		spikeRate = thePlatformGenerator.randomSpikeThreshold;
	}
	
	// Update is called once per frame
	void Update () {
		if (powerupCount >= 50 && !fiftyPowerups) {
			Social.ReportProgress (RunIntoTheNightResources.achievement_collect_50_power_ups, 100.0f, (bool success) => {
				Debug.Log (success);
				fiftyPowerups = true;
			});
		}

		if (powerupActive) {
			powerupLengthCounter -= Time.deltaTime;

			if (theGameManager.powerupReset) {
				powerupLengthCounter = 0;
				theGameManager.powerupReset = false;
			}

			if (doublePoints) {
				theScoreManager.pointsPerSecond = normalPointsPerSecond * 2.75f;
				theScoreManager.shouldDouble = true;
			}

			if (safeMode) {
				thePlatformGenerator.randomSpikeThreshold = 0f;
			}

			if (powerupLengthCounter <= 0) {
				doublePoints = false;
				safeMode = false;
				coinMagnet = false;
				powerupLengthCounter = 0;
				theScoreManager.pointsPerSecond = normalPointsPerSecond;
				theScoreManager.shouldDouble = false;
				thePlatformGenerator.randomSpikeThreshold = spikeRate;
				powerupActive = false;
			}
		}
	}

	public void ActivatePowerup(bool points, bool safe, bool magnet, float time) {
		doublePoints = points;
		safeMode = safe;
		coinMagnet = magnet;
		powerupLengthCounter = time;

		powerupActive = true;
		powerupCount++;

		if (safeMode) {
			spikeList = FindObjectsOfType<PlatformDestroyer> ();
			for (int i = 0; i < spikeList.Length; i++) {
				if (spikeList [i].gameObject.name.Contains ("Ice Shard")) {
					spikeList [i].gameObject.SetActive (false);
				}
			}
		} 
	}
}
