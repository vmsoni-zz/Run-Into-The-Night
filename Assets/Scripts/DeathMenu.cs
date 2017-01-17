using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DeathMenu : MonoBehaviour {

	public string mainMenuLevel;
	public Text theScore;

	public void KeepRunning() {
		FindObjectOfType<GameManager> ().keepRunningAd ();
	}

	public void RestartGame() {
		FindObjectOfType<GameManager> ().Reset (false);
	}

	public void QuitToMain() {
		Application.LoadLevel (mainMenuLevel);
	}
}
