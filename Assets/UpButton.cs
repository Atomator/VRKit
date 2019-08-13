using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpButton : MonoBehaviour {
	public GameObject elevator;

	public AudioSource buttonPress;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown(){
		// this object was clicked - do something
		buttonPress.Play();
		elevator.GetComponent<MoveUp>().onButtonPress();
		Material mymat = this.GetComponent<Renderer>().material;
		mymat.SetColor("_EmissionColor", new Color(0.16f, 0.50f, 1.91f));
  }   
}
