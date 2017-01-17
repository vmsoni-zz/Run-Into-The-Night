using UnityEngine;
using System.Collections;

public class BackgroundGenerator : MonoBehaviour {
	public GameObject theCamera;
	public GameObject backgroundGenerationPoint;
	public GameObject backgroundToDisplay;
	public Parallax theParallaxController;
	public bool ahead = false;

	private float backgroundGenerationDifference;


	// Use this for initialization
	void Start () {
		backgroundGenerationDifference = backgroundGenerationPoint.transform.position.x - transform.position.x;
		theParallaxController = FindObjectOfType<Parallax> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x < backgroundGenerationPoint.transform.position.x) {
			ahead = true;
			Vector3 newBackgroundPosition = new Vector3 (backgroundGenerationPoint.transform.position.x + backgroundGenerationDifference/2, backgroundToDisplay.transform.position.y, backgroundToDisplay.transform.position.z);


			GameObject newBackground = Instantiate (backgroundToDisplay, newBackgroundPosition, backgroundToDisplay.transform.rotation) as GameObject;
			newBackground.transform.parent = theCamera.transform;
			theParallaxController.backgrounds.Add(newBackground);
			transform.position = new Vector3(backgroundGenerationPoint.transform.position.x + backgroundGenerationDifference, transform.position.y, transform.position.z);
			transform.parent = newBackground.transform;
		}
	
	}
}

