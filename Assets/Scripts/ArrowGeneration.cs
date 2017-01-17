using UnityEngine;
using System.Collections;

public class ArrowGeneration : MonoBehaviour {

	public ObjectPooler theArrowPool;
	public float randomArrowThreshold;


	private PlayerController thePlayerController;

	// Use this for initialization
	void Start () {
		thePlayerController = FindObjectOfType<PlayerController> ();
	}

	// Update is called once per frame
	void Update () {
		if ((Random.Range (0f, 100f) < randomArrowThreshold)  && (thePlayerController.grounded)) {
			GameObject newArrow = theArrowPool.GetPooledObject ();

			newArrow.transform.position = new Vector3 (transform.position.x, transform.position.y, 0f);
			newArrow.transform.rotation = transform.rotation;
			newArrow.SetActive (true);
		}
	}
}
