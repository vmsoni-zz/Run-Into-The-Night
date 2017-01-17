using UnityEngine;
using System.Collections;

public class DayNightController : MonoBehaviour {

	private Color endColor = new Color (0.5f,0.5f,0.5f);
	private Color mountainEndColor = new Color (0.7f,0.7f,0.7f);
	private Color beginningColor = new Color(1f,1f,1f);
	public Color noDisplayColor = new Color (1f, 1f, 1f, 0f);
	public Color DisplayColor = new Color (1f, 1f, 1f, 1f);
	public float fadeTime;

	public SpriteRenderer stars;
	public SpriteRenderer DayNight;
	public SpriteRenderer backgroundMountains;

	private ScoreManager theScoreManager;
	private float tColor = 0f;
	private float tColorStars = 0f;

	// Use this for initialization
	void Start () {
		stars.color = noDisplayColor;
		theScoreManager = FindObjectOfType<ScoreManager> ();
	}

	public void resetBackgroundColors() {
		tColor = 0f;
		//tColorStars = 0;
		stars.color = noDisplayColor;
		DayNight.color = beginningColor;
		backgroundMountains.color = beginningColor;
	}

	// Update is called once per frame
	void Update () {
		if (tColor <= 1f && theScoreManager.scoreCount >= 20) {
			tColor += Time.deltaTime / fadeTime;
			DayNight.color = Color.Lerp (DayNight.color, endColor, tColor);
			stars.color = Color.Lerp (stars.color, DisplayColor, tColor);
			backgroundMountains.color = Color.Lerp (backgroundMountains.color, mountainEndColor, tColor);
		}
	}
}
