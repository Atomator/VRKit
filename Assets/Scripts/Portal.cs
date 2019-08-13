using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Portal : MonoBehaviour {

	public Material[] materials;

	// Use this for initialization
	void Start () {
		foreach (var mat in materials) {
			mat.SetInt("_StencilTest", (int)CompareFunction.Equal);
		}
	}

	void OnTriggerStay(Collider other){
		if (other.tag != "Player") {
			return;
		}

		// Outside of portal
		if(transform.position.z > other.transform.position.z) {
			foreach (var mat in materials) {
				mat.SetInt("_StencilTest", (int)CompareFunction.Equal);
			}
		}

		// Inside of portal
		else {
			foreach (var mat in materials) {
				mat.SetInt("_StencilTest", (int)CompareFunction.Equal);
			}
		}
	}

	void OnDestroy() {
		foreach (var mat in materials) {
			mat.SetInt("_StencilTest", (int)CompareFunction.NotEqual);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
