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
					if(PieceManager.instance.firstmove){
						PieceManager.instance.firstmove = false;
						addPiece();  
					}else if (RowValidation() && ColumnValidation() ) {
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
		int row = int.Parse(this.name.Substring(0,1));
		int column = int.Parse(this.name.Substring(2,1)); 
		Debug.Log("Column: " + column);
		int total = 0;
		int firstpos = FindFirstVerticalPosition(row, column);
		int lastpos = findLastVerticalPosition(row,column);
		if (firstpos < lastpos){
				for(int i=firstpos;i<lastpos;i++){
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
				Debug.Log("Column Total: " + total);

				return oddTotalValidation(total);
			}
			else if (firstpos == lastpos){
				total = total + int.Parse(PieceManager.instance.playingPiece.GetComponentInChildren<Text>().text);
				return RowValidation(); 
			}
		
		return oddTotalValidation(total);

	}
	public bool RowValidation(){
		int row = int.Parse(this.name.Substring(0,1));
		int column = int.Parse(this.name.Substring(2,1)); 
		Debug.Log("Row: " + row); 
		int total = 0;
		int firstpos = FindFirstHorizontalPosition(row, column);
		int lastpos = findLastHortizontalPosition(row,column);

		if (firstpos < lastpos){
				for(int i=firstpos;i<lastpos;i++){
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
			else if (firstpos == lastpos){
				total = total + int.Parse(PieceManager.instance.playingPiece.GetComponentInChildren<Text>().text);
				return ColumnValidation(); 
			}
		
		return oddTotalValidation(total);
	}

//Check if the total score for the row is an odd number. 
	bool oddTotalValidation(int total){
		return total %2 !=0; 
	}

	int FindFirstHorizontalPosition(int row, int column){
		int first = column;

			for (int i = column; i > -1; i--){
				if (BoxSpawner.gridArray[row,i].GetComponentInChildren<Text>().text != ""){
					if (first > i){
						first = i;
					}
				} else if (BoxSpawner.gridArray[row,i].GetComponentInChildren<Text>().text != "") {
						return first; 
				}
			} 

		return first; 
	}

	int findLastHortizontalPosition(int row, int column){
			int last = column;
			for (int i = column; i < 5; i++){
				if (BoxSpawner.gridArray[row,i].GetComponentInChildren<Text>().text != ""){
					if (last < i){
						last = i;
					}
				} else if (BoxSpawner.gridArray[row,i].GetComponentInChildren<Text>().text != "") {
						return last; 
				}
			} 

		return last;
	}

	int FindFirstVerticalPosition(int row, int column){
	int first = row;

			for (int i = row; i > -1; i--){
				if (BoxSpawner.gridArray[i,column].GetComponentInChildren<Text>().text != ""){
					if (first > i){
						first = i;
					}
				}else if (BoxSpawner.gridArray[row,i].GetComponentInChildren<Text>().text != "") {
						return first; }

			} 

		return first; 

	}

	int findLastVerticalPosition(int row, int column){
			int last = row;
			for (int i = row; i < 5; i++){
				if (BoxSpawner.gridArray[i,column].GetComponentInChildren<Text>().text != ""){
					if (last < i){
						last = i;
					}
				} else if (BoxSpawner.gridArray[i,column].GetComponentInChildren<Text>().text != "") {
						return last; 
				}
			} 

		return last;
	}


}
