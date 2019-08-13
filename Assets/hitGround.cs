using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class hitGround : MonoBehaviour {
	public GameObject plank;

	public AudioSource fallingWind;
	
	private float where = 0;
	private Falling falling;
	private bool onGround = false;
	private bool waited = false;

	// Use this for initialization
	void Start () {
		falling = plank.GetComponent<Falling>();
	}
	
	// Update is called once per frame
	void Update () {
		if (onGround) {
			fallingWind.Stop();
			StartCoroutine(Wait());	
		}
	}

	void OnTriggerEnter(Collider other)
    {
		if (falling.falling){
			falling.falling = false;
			onGround = true;
		}
    }

	IEnumerator Wait () {
		yield return new WaitForSeconds(6f);
		SceneManager.LoadScene("MainMenu");
	}
}
