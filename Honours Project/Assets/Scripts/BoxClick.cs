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
			//Checks if The Piece has been clicked and will place it on the grid if it has. 
			if (PieceManager.instance.PieceSelected && GetComponentInChildren<Text>().text == ""){
				GetComponent<Image>().color = Color.cyan;
				string objectname = this.name;
				Debug.Log(objectname + " has been clicked.");

				int row = int.Parse(this.name.Substring(0,1)); 
				int column = int.Parse(this.name.Substring(2,1));

				if (ValidationManager.PositioningValidation(row, column))
				{
					if(PieceManager.instance.firstmove){
						PieceManager.instance.firstmove = false;
						addPiece();  
					}else if (ValidationManager.RowValidation(row, column) && ValidationManager.ColumnValidation(row,column) ) {
						addPiece(); 
					} else {
						PieceManager.instance.PieceSelected = false; 
						ErrorManagement.instance.ShowError("Error: Please ensure that the total value is an odd number");
					}

				} else{
					ErrorManagement.instance.ShowError("Error: Piece must be placed next to an existing piece.");
				}
			} else if (PieceManager.instance.PieceSelected && GetComponentInChildren<Text>().text != "") {
				PieceManager.instance.PieceSelected = false; 
				ErrorManagement.instance.ShowError("Error: Piece cannot be placed ontop of an existing piece.");
			} else {
				PieceManager.instance.PieceSelected = false; 
				ErrorManagement.instance.ShowError("Error: Please select a piece before placing on the grid.");
			
		}
		buttonPressed = true; 	
		}	
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


	void addPiece(){
		GetComponentInChildren<Text>().text = PieceManager.instance.playingPiece.GetComponentInChildren<Text>().text;
		PieceManager.instance.setPieceValue(); 
		PieceManager.instance.PieceSelected = false; 
		GetComponent<Collider2D>().enabled = false; 
	}


}
