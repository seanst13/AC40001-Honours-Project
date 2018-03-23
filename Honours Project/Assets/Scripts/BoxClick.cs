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
			// Debug.Log(PieceManager.pieceArray[PieceManager.instance.returnIndex()].name);
			//Checks if The Piece has been clicked and will place it on the grid if it has. 
			if (PieceManager.instance.selected && GetComponentInChildren<Text>().text == ""){
				GetComponent<Image>().color = Color.cyan;
				string objectname = this.name;
				Debug.Log(objectname + " has been clicked.");

				int row = int.Parse(objectname.Substring(0,1)); 
				int column = int.Parse(objectname.Substring(2,1));

				if (ValidationManager.PositioningValidation(row, column))
				{
					tempAddPiece(row,column);

				} else{
					ErrorManagement.instance.ShowError("Error: Piece must be placed next to an existing piece.");
				}
			} else if (PieceManager.instance.selected && GetComponentInChildren<Text>().text != "") {

				if (!GetComponent<Collider2D>().enabled){
					PieceManager.instance.selected = false; 
					ErrorManagement.instance.ShowError("Error: Piece cannot be placed ontop of an existing piece.");
				} else if (GetComponent<Collider2D>().enabled){
					PieceManager.instance.selected = false;
					GetComponentInChildren<Text>().text = "";
					foreach(Piece p in PieceManager.instance.placedPieces){
						if (p.position == this.name){
							PieceManager.pieceArray[p.index].SetActive(true);
							PieceManager.instance.placedPieces.RemoveAt(PieceManager.instance.placedPieces.IndexOf(p));
						}
					}
				}

				
			} else if (!PieceManager.instance.selected) {
				Debug.Log(PieceManager.pieceArray[PieceManager.instance.returnIndex()].name + "is " + PieceManager.instance.selected);
				PieceManager.instance.selected = false; 
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

	void tempAddPiece(int row, int column){
		PieceManager.instance.placedPieces.Add(new Piece{
			position = this.name,
			index = PieceManager.instance.returnIndex()
		});
		BoxSpawner.gridArray[row,column].GetComponentInChildren<Text>().text = PieceManager.instance.returnPieceValue().ToString();
		PieceManager.pieceArray[PieceManager.instance.returnIndex()].SetActive(false);
		PieceManager.instance.pieceClicked( PieceManager.instance.returnIndex());
	}

}
