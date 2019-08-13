using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Remove : MonoBehaviour {
	public void remove(GameObject back, GameObject going, GameObject controller){
		back.SetActive(false);
		going.SetActive(false);
		controller.SetActive(false);
	}
}
