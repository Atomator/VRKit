using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onStart : MonoBehaviour {

	public GameObject Canvas;
	public GameObject Move;
	public GameObject Tap;
	public GameObject Interactions;
	public GameObject City;
	public GameObject Elevator;
	public GameObject Indicator;

	private bool Complete;


	// Use this for initialization
	void Start () {
		City.SetActive(false);
		Elevator.SetActive(false);
		StartCoroutine(Intro());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator Intro () {
		Indicator.SetActive(false);
		Interactions.SetActive(false);
		Tap.SetActive(false);
		Move.SetActive(true);
		yield return new WaitForSeconds(6f);
		Move.SetActive(false);
		Tap.SetActive(true);
		yield return new WaitForSeconds(4f);
		Canvas.SetActive(false);
		Interactions.SetActive(true);
		Indicator.SetActive(true);
	}
}
