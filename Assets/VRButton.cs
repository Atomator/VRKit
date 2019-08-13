using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRButton : MonoBehaviour {
	public GameObject GvrMain;
	public GameObject Elevator;

	public AudioSource buttonPress;

	private GvrViewer VRModeEnabled;
	private MoveUp pressed;
	private Material mymat;

	// Use this for initialization
	void Start () {
		pressed = Elevator.GetComponent<MoveUp>();
		mymat = this.GetComponent<Renderer>().material;
		VRModeEnabled = GvrMain.GetComponent<GvrViewer>();		
	}
	
	// Update is called once per frame
	void Update () {
		if (!pressed.pressed) {
			mymat.SetColor("_EmissionColor", new Color(0f, 0f, 0f));
		}

		else if (pressed.pressed && !VRModeEnabled.VRModeEnabled && pressed.doorInPlace) {
			mymat.SetColor("_EmissionColor", new Color(1.91f, 1.91f, 1.91f));
		}
		
		else if (pressed.pressed && VRModeEnabled.VRModeEnabled) {
			mymat.SetColor("_EmissionColor", new Color(0.16f, 0.50f, 1.91f));
		}

	}

	void OnMouseDown(){
		// this object was clicked - do something
		if (pressed.pressed) {
			buttonPress.Play();
			GvrMain.GetComponent<VRMode>().enableVR();

		}
  }   
}
