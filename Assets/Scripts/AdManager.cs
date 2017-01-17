using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour {
	
	public GameManager theGameManager;

	[SerializeField] string gameID= "1209332";

	void Awake() {
		Advertisement.Initialize (gameID, false);
	}

	public void ShowAd(string zone = "video") {
		#if UNITY_EDITOR
			StartCoroutine(WaitForAd ());
		#endif

		if (Advertisement.IsReady (zone)) {
			Advertisement.Show (zone);
		}
	}

	IEnumerator WaitForAd() {
		float currentTimeScale = Time.timeScale;
		Time.timeScale = 0f;
		yield return null;

		while(Advertisement.isShowing){
			yield return null;
			Time.timeScale = currentTimeScale;
		}
	}

	public void ShowAdRewarded(string zone = "rewardedVideo"){ 
		ShowOptions options = new ShowOptions();
		options.resultCallback = AdCallbackhandler;

		#if UNITY_EDITOR
		StartCoroutine(WaitForAd ());
		#endif

		if (Advertisement.IsReady(zone))
		{
			Advertisement.Show (zone,options);
		}
	}

	void AdCallbackhandler(ShowResult result){
		switch (result){
		case ShowResult.Finished:
			theGameManager = FindObjectOfType<GameManager> ();
			theGameManager.keepRunningAdScreen ();
			Debug.Log ("Ad Finished. Rewarding player...");
			break;
		case ShowResult.Skipped:
			Application.LoadLevel ("Main Menu");
			Debug.Log ("Ad Skipped");
			break;
		case ShowResult.Failed:
			Application.LoadLevel ("Main Menu");
			Debug.Log("Ad failed");
			break;
		}
	}

}
