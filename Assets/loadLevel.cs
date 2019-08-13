using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadLevel : MonoBehaviour {

	AsyncOperation asyncLoadLevel;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void onButtonPress () {
		// SceneManager.LoadScene("Level0");
		StartCoroutine(LoadLevel());	
	}
  
 IEnumerator LoadLevel (){
 	asyncLoadLevel = SceneManager.LoadSceneAsync("Level0");
 	while (!asyncLoadLevel.isDone){
 		print("Loading the Scene"); 
 		yield return null;
 	}
 }
}
