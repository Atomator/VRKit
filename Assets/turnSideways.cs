using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turnSideways : MonoBehaviour {

	public GameObject button;
	public GameObject text;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft || Input.deviceOrientation == DeviceOrientation.LandscapeRight) {
			text.SetActive(false);
			button.SetActive(true);
		}
		else {
			text.SetActive(true);
			button.SetActive(false);
		}
	}
}
