using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;

public class MoveUp : MonoBehaviour {
	public GameObject elevator;
	public GameObject city;
	public GameObject head;
	public GameObject door;
	public GameObject doorMaterial;

	public AudioSource doorOpen;
	public AudioSource doorClose;
	public AudioSource elevatorUp;
	public AudioSource wind;
	public AudioSource thud;

	public Material solid;
    public Material window;
	public Material skyBox;
	private float duration = 2.0f;

	Renderer rend;

	private ARSessionOrigin arOrigin;
	private ARCameraBackground arBackground;
	
	public bool pressed = false;
	public bool doorInPlace = false;
	private bool Starter = true;
	private bool waited = false;
	private bool played = false;
	private bool waitedAgain = false;
	
	
	void Start () {
		arOrigin = FindObjectOfType<ARSessionOrigin>();
		rend = doorMaterial.GetComponent<Renderer>();
		arBackground = head.GetComponent<ARCameraBackground>();
		door.SetActive(false);
		arBackground.customMaterial = skyBox;
        // At start, use the first material
        rend.material = solid;
	}
	
	// Update is called once per frame
	void Update () {
		if (pressed && door.transform.localPosition.x < -3.057 && elevator.transform.position.y < 10) {
			door.SetActive(true);
			door.transform.Translate(Vector3.right * 0.004f);
			if(!doorOpen.isPlaying) {
				doorOpen.Play();
			}
		}
		if (door.transform.localPosition.x >= -3.057 && !waited) {
			StartCoroutine(Wait());
		}

		if (pressed && elevator.transform.position.y < 116.7 && doorInPlace) {
			StartCoroutine(Up()); 
			if(!elevatorUp.isPlaying) {
					elevatorUp.Play();
					elevatorUp.loop = true;
			}
		}

		if (elevator.transform.position.y >= 116.7 && door.transform.localPosition.x > -4.5) {
			if (!waitedAgain) {
				if (!thud.isPlaying  && !played) {
					thud.Play();
					played = !played;
				}				
				if(!wind.isPlaying) {
					wind.Play();
					wind.loop = true;
				}
				StartCoroutine(WaitAgain());
				elevatorUp.Stop();
			}
			if (waitedAgain){
				door.transform.Translate(Vector3.left * 0.006f);
				if(!doorClose.isPlaying) {
					doorClose.Play();
				}
			}
		}

		if (door.transform.localPosition.x < -4.5) {

			door.SetActive(false);
		}
	}

	IEnumerator Up () {
		RenderSettings.skybox = skyBox;
		arBackground.useCustomMaterial = true;
		city.SetActive(true);
		rend.material = window;
		Vector3 location = new Vector3(city.transform.position.x, city.transform.position.y - 0.08f, city.transform.position.z);
		Vector3 locationEle = new Vector3(elevator.transform.position.x, elevator.transform.position.y + 0.08f, elevator.transform.position.z);
		arOrigin.MakeContentAppearAt(city.transform, location);
		elevator.transform.position = locationEle;
		yield return new WaitForSeconds(0.001f);	
	}

	IEnumerator Wait () {
		yield return new WaitForSeconds(3f);	
		doorInPlace = true;
		waited = true;
	}

	IEnumerator WaitAgain () {
		yield return new WaitForSeconds(3f);	
		waitedAgain = true;
	}

	public void onButtonPress () {
		pressed = true;
	}
}
