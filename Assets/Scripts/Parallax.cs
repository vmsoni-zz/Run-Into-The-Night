using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Parallax : MonoBehaviour {

	public List <GameObject> backgrounds;
	//public List<float> parallaxScales;
	public float smoothing;
	public Vector3 camStartPoint;

	public Transform cam;
	public Vector3 previousCamPos;


	// Use this for initialization
	void Start () {
		cam = Camera.main.transform;
		camStartPoint = cam.position;
		previousCamPos = cam.position;
	
	}
	
	// Update is called once per frame
	void LateUpdate () {

		for (int i = 0; i < backgrounds.Count; i++) {
			float parallax = (previousCamPos.x - cam.position.x);
			float backgroundTargetPosX = backgrounds [i].transform.position.x + parallax;
			Vector3 backgroundTargetPos = new Vector3 (backgroundTargetPosX, backgrounds [i].transform.position.y, backgrounds [i].transform.position.z);
			backgrounds[i].transform.position = Vector3.Lerp(backgrounds[i].transform.position, backgroundTargetPos, smoothing * Time.deltaTime);
			if (backgrounds [i].transform.position.x <= GameObject.Find ("PlatformDestructionPoint").transform.position.x && (i != 0)) {
				backgrounds [i].SetActive (false);
			}
		}
		previousCamPos = cam.position;

	}
}
