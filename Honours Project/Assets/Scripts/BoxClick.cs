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
			// Debug.Log(PieceSpawner.pieceArray[PieceSpawner.instance.returnIndex()].name);
			//Checks if The Piece has been clicked and will place it on the grid if it has. 
			if (PieceSpawner.instance.selected && GetComponentInChildren<Text>().text == ""){
				GetComponent<Image>().color = Color.cyan;
				string objectname = this.name;
				Debug.Log(objectname + " has been clicked.");

				int row = int.Parse(this.name.Substring(0,1)); 
				int column = int.Parse(this.name.Substring(2,1));

				if (ValidationManager.PositioningValidation(row, column))
				{
					// if(PieceSpawner.instance.firstmove){
					// 	PieceSpawner.instance.firstmove = false;
					// 	addPiece();
					// 	addScore(row,column);  
					// }else 
					if (ValidationManager.RowValidation(row, column) ) {
						addPiece(row, column); 
						addScore(row,column);
					} else {
						PieceSpawner.instance.selected = false; 
						ErrorManagement.instance.ShowError("Error: Please ensure that the total value is an odd number");
					}

				} else{
					ErrorManagement.instance.ShowError("Error: Piece must be placed next to an existing piece.");
				}
			} else if (PieceSpawner.instance.selected && GetComponentInChildren<Text>().text != "") {
				PieceSpawner.instance.selected = false; 
				ErrorManagement.instance.ShowError("Error: Piece cannot be placed ontop of an existing piece.");
			} else if (!PieceSpawner.instance.selected) {
				Debug.Log(PieceSpawner.pieceArray[PieceSpawner.instance.returnIndex()].name + "is " + PieceSpawner.instance.selected);
				PieceSpawner.instance.selected = false; 
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


	void addPiece(int row, int column){
		BoxSpawner.gridArray[row,column].GetComponentInChildren<Text>().text = PieceSpawner.instance.returnPieceValue().ToString();

		PieceSpawner.instance.pieceClicked(PieceSpawner.instance.returnIndex()); 
		GetComponent<Collider2D>().enabled = false;
		PieceSpawner.instance.setPieceValue(PieceSpawner.instance.returnIndex());
	}

	void addScore(int row, int column){
		int total = 0;
		int secondtotal = 0; 

		if (ValidationManager.RowTotal(row, column) != PieceSpawner.instance.returnPieceValue()){
			total = ValidationManager.RowTotal(row, column);
			// Secondary Column Checks
			if (BoxSpawner.gridArray[row+1,column].GetComponentInChildren<Text>().text !=""
				|| BoxSpawner.gridArray[row-1,column].GetComponentInChildren<Text>().text !="" ){
					secondtotal = ValidationManager.columnTotal(row,column);
				}
			total = total + secondtotal; 
		} else {
			total = ValidationManager.columnTotal(row, column);
		}

		Debug.Log("TOTAL SCORE: " + total);
		ScoreManager.instance.setPlayerScore(total);

	}


}
