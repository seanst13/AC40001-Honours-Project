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

				if (PositioningValidation())
				{
					if(RowValidation() && ColumnValidation()){
					GetComponentInChildren<Text>().text = PieceManager.instance.playingPiece.GetComponentInChildren<Text>().text;
					PieceManager.instance.setPieceValue(); 
					PieceManager.instance.PieceSelected = false; 
					GetComponent<Collider2D>().enabled = false; 
					} else {
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

	bool PositioningValidation(){
		int row = int.Parse(this.name.Substring(0,1)); 
		int column = int.Parse(this.name.Substring(2,1));
		//If the piece lies in the rows; 
		if ((row > 0 && row < 4)  && (column >=0 && column <=4)){
			if (column+1 >4){
				if(		BoxSpawner.gridArray[row, column-1].GetComponentInChildren<Text>().text != "" ||
						BoxSpawner.gridArray[row+1, column].GetComponentInChildren<Text>().text != "" ||
						BoxSpawner.gridArray[row-1, column].GetComponentInChildren<Text>().text != ""){
						return true; 
					}
				else { return false; }
			} else if (column -1 < 0) {
				if(		BoxSpawner.gridArray[row, column+1].GetComponentInChildren<Text>().text != "" ||
						BoxSpawner.gridArray[row+1, column].GetComponentInChildren<Text>().text != "" ||
						BoxSpawner.gridArray[row-1, column].GetComponentInChildren<Text>().text != ""){
						return true; 
					}
				else { return false; }
			} else {
				if( BoxSpawner.gridArray[row, column+1].GetComponentInChildren<Text>().text != "" ||
					BoxSpawner.gridArray[row, column-1].GetComponentInChildren<Text>().text != "" ||
					BoxSpawner.gridArray[row+1, column].GetComponentInChildren<Text>().text != "" ||
					BoxSpawner.gridArray[row-1, column].GetComponentInChildren<Text>().text != ""){
						return true; 
					}
				else {
					return false; 
				}
			}
		//If the piece lies in the top row
		} else if (row == 0 && (column >= 0 && column <=4)){
			if (column+1 > 4){
				if( BoxSpawner.gridArray[row, column-1].GetComponentInChildren<Text>().text != "" ||
					BoxSpawner.gridArray[row+1, column].GetComponentInChildren<Text>().text != ""){
						return true; 
					}
				else {
					return false; 
				}	
			} else if (column-1 < 0 ){
					if( BoxSpawner.gridArray[row, column+1].GetComponentInChildren<Text>().text != "" ||
						BoxSpawner.gridArray[row+1, column].GetComponentInChildren<Text>().text != ""){
							return true; 
					}
				else {
					return false; 
				}
			} else {
					if( BoxSpawner.gridArray[row, column+1].GetComponentInChildren<Text>().text != "" ||
						BoxSpawner.gridArray[row, column-1].GetComponentInChildren<Text>().text != "" ||
						BoxSpawner.gridArray[row+1, column].GetComponentInChildren<Text>().text != ""){
							return true; 
					}
				else {
					return false; 
				}
			}
		} else if (row == 4 && (column >= 0 && column <= 4))
		{
			if (column+1 > 4){
				if( BoxSpawner.gridArray[row, column-1].GetComponentInChildren<Text>().text != "" ||
					BoxSpawner.gridArray[row-1, column].GetComponentInChildren<Text>().text != ""){
						return true; 
					}
				else {
					return false; 
				}	
			} else if (column-1 < 0 ){
					if( BoxSpawner.gridArray[row, column+1].GetComponentInChildren<Text>().text != "" ||
						BoxSpawner.gridArray[row-1, column].GetComponentInChildren<Text>().text != ""){
							return true; 
					}
				else {
					return false; 
				}
			} else {
					if( BoxSpawner.gridArray[row, column+1].GetComponentInChildren<Text>().text != "" ||
						BoxSpawner.gridArray[row, column-1].GetComponentInChildren<Text>().text != "" ||
						BoxSpawner.gridArray[row-1, column].GetComponentInChildren<Text>().text != ""){
							return true; 
					}
				else {
					return false; 
				}
			}
		} else {
			return false; 
		}
	}
public bool ColumnValidation(){
		int column = int.Parse(this.name.Substring(2,1));
		Debug.Log("Column: " + column);
		int total = 0;
		for(int i=0;i<5;i++){
			string txt = BoxSpawner.gridArray[i,column].GetComponentInChildren<Text>().text;
			int value = 0;
			if (txt != ""){
				Debug.Log("String from Grid:" + txt);
				value = int.Parse(txt);
			} else if (txt == ""){
				value = 0; 
			}
			total = total + value;
		}

		total = total + int.Parse(PieceManager.instance.playingPiece.GetComponentInChildren<Text>().text);
		Debug.Log("Row Total: " + total);

	//Check if the total score for the column is an odd number. 
			return oddTotalValidation(total);

	}
	public bool RowValidation(){
		int row = int.Parse(this.name.Substring(0,1)); 
		Debug.Log("Row: " + row); 
		int total = 0;
		for(int i=0;i<5;i++){
			string txt = BoxSpawner.gridArray[row,i].GetComponentInChildren<Text>().text;

			int value = 0;
			if (txt != ""){
				Debug.Log("String from Grid:" + txt);
				value = int.Parse(txt);
			} else if (txt == ""){
				value = 0; 
			}
			total = total + value;
		}

	total = total + int.Parse(PieceManager.instance.playingPiece.GetComponentInChildren<Text>().text);
	Debug.Log("Row Total: " + total);

			return oddTotalValidation(total);
	}

//Check if the total score for the row is an odd number. 
	bool oddTotalValidation(int total){
		return total %2 !=0; 
	}
}
