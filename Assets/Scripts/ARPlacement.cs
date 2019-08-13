using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;

public class ARPlacement : MonoBehaviour {

	public GameObject objectToPlace;
	public GameObject placementIndicator;
	public GameObject elevator;
	public GameObject city;
	public Transform rotate;
	public GameObject buttonBack;
	public GameObject buttonContinue;

	private ARSessionOrigin arOrigin;
	private Pose placementPose;
	private bool placePoseIsValid = false;
	private bool placed = false;

	Vector3 startPosition;
	Quaternion startRotation;

	// Use this for initialization
	void Start () {
		buttonBack.SetActive(false);
		buttonContinue.SetActive(false);
		startPosition = objectToPlace.transform.position;
		startRotation = objectToPlace.transform.rotation;
		arOrigin = FindObjectOfType<ARSessionOrigin>();
		city.SetActive(false);
		elevator.SetActive(false);
		objectToPlace.SetActive(true);
		arOrigin.MakeContentAppearAt(objectToPlace.transform, startPosition, startRotation);
	}
	
	// Update is called once per frame
	void Update () {
		if (placePoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !placed) {
			// arOrigin.MakeContentAppearAt(objectToPlace.transform, startPosition, startRotation);
			PlaceObject();
		}
		UpdatePlacementPose();
		UpdatePlacementIndicator();
	}

	private void UpdatePlacementPose() {
		var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
		var hits = new List<ARRaycastHit>();
		arOrigin.Raycast(screenCenter, hits, TrackableType.Planes);

		placePoseIsValid = hits.Count > 0;

		if (placePoseIsValid && !placed) {
			placementPose = hits[0].pose;

			var cameraForward = rotate.transform.forward;
			var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z);
			placementPose.rotation = Quaternion.LookRotation(cameraBearing);
		}
	}

	private void UpdatePlacementIndicator() {
		if (placePoseIsValid && !placed) {
			placementIndicator.SetActive(true);
			// arOrigin.MakeContentAppearAt(placementIndicator.transform, placementPose.position, placementPose.rotation);
			placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
		}

		else {
			placementIndicator.SetActive(false);
		}
	}

	private void PlaceObject() {
		placementIndicator.SetActive(false);
		elevator.SetActive(true);
		Vector3 rot = rotate.localEulerAngles;
		rot = new Vector3(0, 180 + rot.y, 0);
		print(Camera.current.transform.localRotation);
		arOrigin.MakeContentAppearAt(objectToPlace.transform, placementPose.position);
		arOrigin.MakeContentAppearAt(objectToPlace.transform, Quaternion.Euler(rot));
		placed = true;
		buttonBack.SetActive(true);
		buttonContinue.SetActive(true);
	}

	public void Back() {
		elevator.SetActive(false);
		placed = false;
		buttonBack.SetActive(false);
		buttonContinue.SetActive(false);
		placementIndicator.SetActive(true);
	}

}
