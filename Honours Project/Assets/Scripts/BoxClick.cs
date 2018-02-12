using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BoxClick : MonoBehaviour {

	bool buttonPressed = false;
	 Color32 defaultColour;
	public void boxEnter(){
		if(!buttonPressed){
			defaultColour = GetComponent<Image>().color;
			GetComponent<Image>().color = Color.cyan;
			string objectname = this.name;
			Debug.Log(objectname + " has been clicked.");

			if (PieceManager.instance.PieceSelected){
				GetComponentInChildren<Text>().text = PieceManager.instance.playingPiece.GetComponentInChildren<Text>().text;
				PieceManager.instance.setPieceValue(); 
				PieceManager.instance.PieceSelected = false; 
			}

		}
		buttonPressed = true; 
	}
	public void boxExit(){
		if(buttonPressed){
			Invoke("returnToDefault", 2); //Wait 2 Seconds before resetting the colour
			buttonPressed = false; 
		}
	
	}

	void returnToDefault(){
		GetComponent<Image>().color = defaultColour; 
	}
	void Update () {
		
	}
}
