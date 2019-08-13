using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;

public class VRMode : MonoBehaviour {
	public GameObject Elevator;
	public GameObject Head;
	float m_FieldOfView;
	
	private GvrViewer VRModeEnabled;
	private ARCameraBackground arBackground;
	private MoveUp pressed;

	// Use this for initialization
	void Start () {
		VRModeEnabled = this.GetComponent<GvrViewer>();	
		arBackground = Head.GetComponent<ARCameraBackground>();	
		pressed = Elevator.GetComponent<MoveUp>();
	}
	
	// Update is called once per frame
	void Update () {
		if (VRModeEnabled.BackButtonPressed) {
			enableVR();
		}
	}

	public void enableVR() {
		if (pressed.doorInPlace) {
			arBackground.enabled = !arBackground.enabled;
			VRModeEnabled.VRModeEnabled = !VRModeEnabled.VRModeEnabled;
		}
	}
}
