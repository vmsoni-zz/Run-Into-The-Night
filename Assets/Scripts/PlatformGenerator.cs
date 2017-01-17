using UnityEngine;
using System.Collections;

public class PlatformGenerator : MonoBehaviour {

	public GameObject thePlatform;
	public GameObject generationPoint;
	public float distanceBetween;
	public float distanceBetweenMin;
	public float distanceBetweenMax;
	public ObjectPooler[] theObjectPools;
	public Transform maxHeightPoint;
	public float maxHeightChange;
	public float randomCoinThreshold;
	public float randomSpikeThreshold;
	public ObjectPooler theSpikePool;
	public float powerupHeight;
	public ObjectPooler powerupPool;
	public float powerupThreshold;

	//public GameObject[] thePlatforms;

	private float maxHeight;
	private float minHeight;
	private float heightChange;
	private float[] platformWidths;
	private int platformSelector;
	private float platformWidth;
	private CoinGenerator theCoinGenerator;


	// Use this for initialization
	void Start () {
		platformWidths = new float[theObjectPools.Length];
		theCoinGenerator = FindObjectOfType<CoinGenerator> ();
		for (int i = 0; i < theObjectPools.Length; i++) {
			platformWidths[i] = theObjectPools[i].pooledObject.GetComponent<SpriteRenderer> ().bounds.size.x;
		}

		minHeight = transform.position.y;
		maxHeight = maxHeightPoint.position.y;

	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x < generationPoint.transform.position.x) {
			distanceBetween = Random.Range(distanceBetweenMin, distanceBetweenMax);
			platformSelector = Random.Range (0, theObjectPools.Length);
			heightChange = transform.position.y + Random.Range (maxHeightChange, -maxHeightChange);

			if (heightChange > maxHeight) {
				heightChange = maxHeight;
			} else if (heightChange < minHeight) {
				heightChange = minHeight;
			}

			if (Random.Range(0f, 100f) < powerupThreshold) {
				GameObject newPowerup = powerupPool.GetPooledObject ();
				newPowerup.transform.position = transform.position + new Vector3 (distanceBetween / 2f, Random.Range(powerupHeight/2f,powerupHeight), 0f);
				newPowerup.SetActive (true);
				newPowerup.transform.GetChild (0).gameObject.SetActive (true);
			}

			transform.position = new Vector3 (transform.position.x + (platformWidths[platformSelector]/2f + distanceBetween), heightChange, transform.position.z);
			//Instantiate (/*thePlatform*/thePlatforms[platformSelector], transform.position, transform.rotation);

			GameObject newPlatform = theObjectPools[platformSelector].GetPooledObject();
			newPlatform.transform.position = transform.position;
			newPlatform.transform.rotation = transform.rotation;
			newPlatform.SetActive (true);

			if (Random.Range (0f, 100f) < randomCoinThreshold) {
				theCoinGenerator.SpawnCoins (new Vector3 (transform.position.x, transform.position.y + 1f, transform.position.y));
			}

			if (Random.Range (0f, 100f) < randomSpikeThreshold) {
				GameObject newSpike = theSpikePool.GetPooledObject ();

				float spikeXPosition = Random.Range(-platformWidths[platformSelector] / 2 + 1f, platformWidths[platformSelector] / 2 - 1f);

				Vector3 spikePosition = new Vector3 (spikeXPosition, 0.5f, 0f);

				newSpike.transform.position = transform.position + spikePosition;
				newSpike.transform.rotation = transform.rotation;
				newSpike.SetActive(true);
			}

			transform.position = new Vector3 (transform.position.x + (platformWidths[platformSelector]/2), heightChange, transform.position.z);

		}
	}
}
