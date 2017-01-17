using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameInstructions : MonoBehaviour {

	public Image[] gameInstructions;

	public void closeInstructions() {
		gameObject.SetActive (false);
		Time.timeScale = 1f;
	}

	public void openInstructions() {
		gameObject.SetActive (true);
	}

	public void rightImageButton() {
		for(int i=0; i < gameInstructions.Length; i++){
			if(gameInstructions[i].gameObject.activeSelf) {
				if(i == (gameInstructions.Length - 1)) {
					gameInstructions[i].gameObject.SetActive(false);
					gameInstructions[0].gameObject.SetActive(true);
					break;
				}
				else{
					gameInstructions[i].gameObject.SetActive(false);
					gameInstructions[i + 1].gameObject.SetActive(true);
					break;
				}
			}
		}
	}

	public void leftImageButton() {
		for(int i=0; i < gameInstructions.Length; i++){
			if(gameInstructions[i].gameObject.activeSelf) {
				if(i == 0) {
					gameInstructions[0].gameObject.SetActive(false);
					gameInstructions[gameInstructions.Length - 1].gameObject.SetActive(true);
					break;
				}
				else{
					gameInstructions[i].gameObject.SetActive(false);
					gameInstructions[i - 1].gameObject.SetActive(true);
					break;
				}
			}
		}
	}
}
