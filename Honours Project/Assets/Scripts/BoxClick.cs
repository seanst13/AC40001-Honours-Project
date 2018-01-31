﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BoxClick : MonoBehaviour {

	bool buttonPressed = false;
	bool white = false;

	 Color32 greybox = new Color32(135,135,135,255);

	// Use this for initialization
	public void turnBlue(){
		if(!buttonPressed){
			Debug.Log("Button pressed");
			if(GetComponent<Image>().color == Color.white){
				GetComponent<Image>().color = Color.cyan;
				white = true; 
			}else if(GetComponent<Image>().color == greybox) {
				GetComponent<Image>().color = Color.red;
				white = false; 
			}
		}
			buttonPressed = true; 

	}

	public void turnWhite(){

		if(buttonPressed){
			Invoke("changeColour", 2);
			buttonPressed = false; 
		}
		// GetComponent<Image>.color = Color.green;
	}

	void changeColour(){
		if (white){
			Debug.Log("Button escpaed");
			GetComponent<Image>().color = Color.white;
		} else {
			Debug.Log("Button escpaed");
			GetComponent<Image>().color = greybox;
		}
	}
	void Update () {
		
	}
}