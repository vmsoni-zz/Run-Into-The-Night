using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

	public string mainMenuLevel;
	public GameManager theGameManager;
	public GameObject pauseMenu;
	public GameObject adStartMenu;

	public void PauseGame() {
		Time.timeScale = 0f;
		pauseMenu.SetActive (true);
	}

	public void KeepRunning() {
		theGameManager = FindObjectOfType<GameManager> ();
		Time.timeScale = 1f;
		theGameManager.Reset (true);
	}

	public void ResumeGame() {
		Time.timeScale = 1f;
		pauseMenu.SetActive (false);
	}

	public void RestartGame() {
		Time.timeScale = 1f;
		FindObjectOfType<GameManager> ().Reset (false);
		pauseMenu.SetActive (false);
		adStartMenu.SetActive (false);
	}

	public void QuitToMain() {
		Time.timeScale = 1f;
		Application.LoadLevel (mainMenuLevel);
	}

}
