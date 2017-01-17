using UnityEngine;
using UnityEngine.Advertisements;
using System.Collections;

public class SimpleAds : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Advertisement.Initialize ("1209332", true);
		StartCoroutine (ShowAdWhenReady());
	}

	IEnumerator ShowAdWhenReady() {
		while (!Advertisement.IsReady ()) {
			Debug.Log ("Waiting...");
			yield return null;
		}
		Debug.Log ("Done loading!...");
		Advertisement.Show ();
	}
}
