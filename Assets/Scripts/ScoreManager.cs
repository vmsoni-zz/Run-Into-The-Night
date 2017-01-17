using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	public Text scoreText;
	public Text highScoreText;
	public float scoreCount;
	public float highScoreCount;
	public bool shouldDouble;

	private bool fiveHunScore = false;
	private bool oneThouScore = false;
	public float pointsPerSecond;
	public bool scoreIncreasing;
	public int coinsCount;
	private bool oneHunCoins = false;


	// Use this for initialization
	void Start () {
		if (PlayerPrefs.HasKey("HighScore")) {
			highScoreCount = PlayerPrefs.GetFloat ("HighScore");
		}
		if (PlayerPrefs.HasKey("coinsCount")) {
			highScoreCount = PlayerPrefs.GetFloat ("coinsCount");
		}
	}
	
	// Update is called once per frame

	void Update () {
		if (scoreIncreasing) {
			scoreCount += pointsPerSecond * Time.deltaTime;
		}
		if (scoreCount > highScoreCount) {
			highScoreCount = scoreCount;
			PlayerPrefs.SetFloat ("HighScore", highScoreCount);
		}

		if (coinsCount >= 100 && !oneHunCoins) {
			Social.ReportProgress (RunIntoTheNightResources.achievement_collect_100_gold, 100.0f, (bool success) => {
				oneHunCoins = true;
			});
		}

		scoreText.text = "Score: " + Mathf.Round(scoreCount);
		highScoreText.text = "High Score: " + Mathf.Round(highScoreCount);

		if (scoreCount >= 500 && scoreCount <= 1000 && !fiveHunScore) {
			Social.ReportProgress (RunIntoTheNightResources.achievement_run_500_meters, 100.0f, (bool success) => {
				fiveHunScore = true;
			});
		} else if (scoreCount >= 1000 && !oneThouScore) {
			Social.ReportProgress (RunIntoTheNightResources.achievement_run_1000_meters, 100.0f, (bool success) => {
				oneThouScore = true;
			});
		}
	}

	public void AddScore(int pointsToAdd){
		if (shouldDouble) {
			pointsToAdd = pointsToAdd * 2;
		}
		coinsCount++;
		scoreCount += pointsToAdd;

	}

}
