using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public string playGameLevel;
	public bool audioValue = true;
	public Sprite[] audioSprites;
	public GameObject musicController;
	public Button musicButton;
	public MainMenu theMainMenu;

	void Awake () {
		if (!GameObject.Find ("Background Music").GetComponent<AudioSource> ().mute) {
			musicButton.image.overrideSprite = audioSprites [0];
			audioValue = true;
		} else {
			musicButton.image.overrideSprite = audioSprites [1];
			audioValue = false;
		}
	}

	public void PlayGame(){
		Application.LoadLevel (playGameLevel);
	}

	public void MuteMusic(){
		if (audioValue) {
			musicButton.image.overrideSprite = audioSprites [1];
			GameObject.Find("Background Music").GetComponent<AudioSource>().mute = true;
			audioValue = false;
		} else if (!audioValue) {
			musicButton.image.overrideSprite = audioSprites [0];
			GameObject.Find("Background Music").GetComponent<AudioSource>().mute = false;
			audioValue = true;
		}
	}

	public void QuitGame() {
		Application.Quit ();
	}
}
