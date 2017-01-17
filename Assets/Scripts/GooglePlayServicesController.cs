using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;

public class GooglePlayServicesController : MonoBehaviour {

	public Text loginUserNameText;

	// Use this for initialization

	public void OpenAchievements(){
		Social.ShowAchievementsUI ();
	}

	void Start () {
		GooglePlayGames.PlayGamesPlatform.Activate();


		Social.localUser.Authenticate((bool success) =>
			{
				if (success)
				{
					Debug.Log ("Welcome " + Social.localUser.userName);
					loginUserNameText.text = Social.localUser.userName;
				}
				else {
					Debug.Log("Authentication failed.");
					loginUserNameText.text = "Failed Login";
				}
			});
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
