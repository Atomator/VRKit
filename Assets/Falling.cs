using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using UnityEngine.SpatialTracking;

public class Falling : MonoBehaviour {
	public GameObject device;
	public GameObject city;
	public GameObject portalWindow;

	public AudioSource wind;
	public AudioSource fallingWind;

	private bool onBoard = false;
	private bool off = false;
	public bool falling = false;

	private float where = 0;
	private PortalOther inOtherWorld;
	private ARSessionOrigin arOrigin;
	private TrackedPoseDriver tracked;
	private ARSessionOrigin ARSessionOrigin;

	// Use this for initialization
	void Start () {
		arOrigin = FindObjectOfType<ARSessionOrigin>();
		ARSessionOrigin = arOrigin.GetComponent<ARSessionOrigin>();
		inOtherWorld = portalWindow.GetComponent <PortalOther> ();
		tracked = device.GetComponent<TrackedPoseDriver>();
	}
	
	// Update is called once per frame
	void Update () {
		// if (!falling && device.transform.position.y >= 115 && !inOtherWorld.inOtherWorld && (device.transform.localPosition.x <= -0.30 || device.transform.localPosition.x >= 0.30 || device.transform.localPosition.z >= 1.5)) {
		// 	falling = true;
		// 	ARSessionOrigin.enabled = false;
		// 	var pose = device.transform.position;
		// 	tracked.enabled = false;
		// 	device.transform.position = pose;
		// 	off = true;
		// }
		if (falling) {
			device.transform.Translate(Vector3.down * 1);
			if (wind.isPlaying) {
				wind.Stop();
			}
			if (!fallingWind.isPlaying) {
				fallingWind.Play();
			}
		}
		
	}
 
    void OnTriggerEnter(Collider other)
    {
		onBoard = true;
    }

	void OnTriggerExit(Collider other)
    {
		ARSessionOrigin.enabled = false;
		var pose = device.transform.position;
		tracked.enabled = false;
		device.transform.position = pose;
		falling = true;

    }
}