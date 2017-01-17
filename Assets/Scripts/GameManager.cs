using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public float adShowChance;
	public GameObject firstPlatformReal;
	public Transform platformGenerator;
	public PlayerController thePlayer;
	public PauseMenu thePauseScreen;
	public DeathMenu theDeathScreen;
	public GameObject theGameInstructionsScreen;
	public GameObject theKeepRunningScreen;
	public bool powerupReset;
	public GameObject theBackgroundGenerator;
	public GameObject keepRunningButton;
	public Transform firstPlatformStartPoint;

	public PlatformGenerator thePlatformGenerator;
	private DayNightController theDayNightController;
	private ScoreManager theScoreManager;
	private Parallax theParallaxController;
	private PowerupManager thePowerupController;
	private PlatformDestroyer[] platformList;
	private Vector3 playerStartPoint;
	private Vector3 platformStartPoint;
	private Vector3 backgroundStartPoint;
	private Vector3 firstMovingBackgroundStartPoint;
	private AdManager theAdManager;
	private int deathCount=0;
	private int storedDeathCount = 0;
	private bool tenDeathCount = false;

	// Use this for initialization
	void Start () {
		if (PlayerPrefs.HasKey("deathCount")) {
			storedDeathCount = PlayerPrefs.GetInt ("deathCount");
		}

		platformStartPoint = platformGenerator.position;
		playerStartPoint = thePlayer.transform.position;
		backgroundStartPoint = theBackgroundGenerator.transform.position;

		theDayNightController = FindObjectOfType<DayNightController> ();
		theAdManager = FindObjectOfType<AdManager> ();
		theParallaxController = FindObjectOfType<Parallax> ();
		theScoreManager = FindObjectOfType<ScoreManager> ();
		thePowerupController = FindObjectOfType<PowerupManager> ();
		firstMovingBackgroundStartPoint = theParallaxController.backgrounds[0].transform.localPosition;

		if (!PlayerPrefs.HasKey ("InstructionShown")) {
			theGameInstructionsScreen.SetActive (true);
			PlayerPrefs.SetString ("InstructionShown", "true");
			Time.timeScale = 0f;
		}
	}

	public void RestartGame() {
		//StartCoroutine ("RestartGameCo");
		deathCount++;
		storedDeathCount++;

		PlayerPrefs.SetInt ("coinsCount", theScoreManager.coinsCount);
		PlayerPrefs.SetInt ("deathCount", deathCount);
		PlayerPrefs.SetInt ("powerupsCount", thePowerupController.powerupCount);

		if (storedDeathCount >= 10 && !tenDeathCount) {
			Social.ReportProgress (RunIntoTheNightResources.achievement_10_deaths, 100.0f, (bool success) => {
				tenDeathCount = true;
			});
		}

		thePlayer.gameObject.SetActive (false);
		theScoreManager.scoreIncreasing = false;
		theDeathScreen.gameObject.SetActive (true);
		theDeathScreen.theScore.text = "Score: " +  Mathf.Round(theScoreManager.scoreCount);
		if (deathCount % 2 == 0 || Application.internetReachability == NetworkReachability.NotReachable) {
			keepRunningButton.SetActive (false);
		} else {
			keepRunningButton.SetActive (true);
		}
		if (Random.Range (0, 100) < adShowChance) {		
			theAdManager.ShowAd ();
		}
	}

	public void keepRunningAd() {
			theAdManager.ShowAdRewarded ();
			Time.timeScale = 0f;
			theDeathScreen.gameObject.SetActive (false);
			theKeepRunningScreen.SetActive (true);
	}

	public void keepRunningAdScreen() {
		Time.timeScale = 0f;
		theKeepRunningScreen.SetActive (true);
	}


	public void Reset(bool fromAd) {
		theDeathScreen.gameObject.SetActive (false);
		platformList = FindObjectsOfType<PlatformDestroyer> ();

		for (int i = 0; i < platformList.Length; i++) {
			platformList [i].gameObject.SetActive (false);
		}
		for (int i = 1; i < theParallaxController.backgrounds.Count; i++) {
			theParallaxController.backgrounds [i].SetActive(false);
		}

		theDayNightController.resetBackgroundColors ();
		GameObject firstPlatform = (GameObject) Instantiate (firstPlatformReal);
		firstPlatform.SetActive (true);
		firstPlatform.transform.position = firstPlatformStartPoint.position;
		thePlayer.transform.position = playerStartPoint;
		theBackgroundGenerator.transform.parent = null;
		theBackgroundGenerator.transform.position = backgroundStartPoint;
		theParallaxController.backgrounds [0].transform.localPosition = firstMovingBackgroundStartPoint;
		theParallaxController.previousCamPos = theParallaxController.camStartPoint;
		platformGenerator.position = platformStartPoint;
		thePlayer.gameObject.SetActive (true);
		if (!fromAd) {
			theScoreManager.scoreCount = 0;
		} else {
			theKeepRunningScreen.SetActive (false);
		}
		theScoreManager.scoreIncreasing = true;
		powerupReset = true;

	}
}
