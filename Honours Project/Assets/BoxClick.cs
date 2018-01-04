using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BoxClick : MonoBehaviour {

	bool buttonPressed = false;
	// Use this for initialization
	public void turnBlue(){
		if(buttonPressed == false){
		Debug.Log("Button pressed");
		GetComponent<Image>().color = Color.cyan;
		buttonPressed = true; 
	}
		// GetComponent<Image>.color = Color.green;
	}

	public void turnWhite(){

		if(buttonPressed == true){
			Invoke("changeColour", 2);
			buttonPressed = false; 
		}
		// GetComponent<Image>.color = Color.green;
	}

	void changeColour(){
		Debug.Log("Button escpaed");
		GetComponent<Image>().color = Color.white;

	}
	void Update () {
		
	}
}
