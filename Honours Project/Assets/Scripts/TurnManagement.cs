﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnManagement : MonoBehaviour {
	public GameObject EndTurn; 
	public void checkIfValid(){
		int validplays = 0; 
		int row = 0;
		int column = 0;
		int index = 0; 

		foreach(Piece placement in PieceManager.instance.placedPieces){
			row = int.Parse(placement.position.Substring(0,1));
			column = int.Parse(placement.position.Substring(2,1)); 
			
			if (OddCheck(row,column)){
				validplays++;
			}
		}
		if (validplays == PieceManager.instance.placedPieces.Count){
			if (validplays == 1){
				foreach(Piece placement in PieceManager.instance.placedPieces){
					row = int.Parse(placement.position.Substring(0,1));
					column = int.Parse(placement.position.Substring(2,1)); 
					addPiece(row,column, placement.index);
					addScore(row,column); 				
				}
			} else if (validplays > 1){
				bool first = true; 
				int previousrow = 0;
				int previouscol = 0; 
				bool rowadded = false; 
				bool coladded = false;

				foreach (Piece placement in PieceManager.instance.placedPieces){
					if (first){
						previousrow = int.Parse(placement.position.Substring(0,1));
						previouscol = int.Parse(placement.position.Substring(2,1));
						first = true;
						addPiece(previousrow,previouscol, placement.index);
					} else if (previousrow == int.Parse(placement.position.Substring(0,1)) && previouscol != int.Parse(placement.position.Substring(2,1))){
						column = int.Parse(placement.position.Substring(2,1));
						addPiece(previousrow,column, placement.index);
						if (!rowadded){
							addScore(previousrow,column);
							rowadded = true; 
							}
						
						previouscol = column; 
					} else if (previouscol == int.Parse(placement.position.Substring(2,1)) && previousrow != int.Parse(placement.position.Substring(0,1))){
						row = int.Parse(placement.position.Substring(0,1));
						addPiece(row,previouscol, placement.index);
						if (!coladded){
							addScore(row,previouscol);
							coladded = true; 
						}

						
						previousrow = row; 
					} else {
						row = int.Parse(placement.position.Substring(0,1));
						column = int.Parse(placement.position.Substring(2,1)); 
						addPiece(row,column, placement.index);
						addScore(row,column); 	
					}
					addScore(previousrow,previouscol);
				}

			}	
			
			// Need to figure out where I'm placing this and looping. 

			//Ideas - 
			
// 			Do something different if list is size one.
// 			Check if its pieces have already been registered for scoring. 
// 			If it has block it from scoring? 
// 			 
			PieceManager.instance.placedPieces.Clear(); 
		} else {
			foreach(Piece placement in PieceManager.instance.placedPieces){
				row = int.Parse(placement.position.Substring(0,1));
				column = int.Parse(placement.position.Substring(2,1)); 

				BoxSpawner.gridArray[row,column].GetComponentInChildren<Text>().text = ""; 
			}
			PieceManager.instance.placedPieces.Clear(); 
		}
	}

	bool OddCheck(int row, int column){
	
		return ValidationManager.RowValidation(row, column); 
	
	}

	void addPiece(int row, int column, int index){
		//BoxSpawner.gridArray[row,column].GetComponentInChildren<Text>().text = PieceManager.instance.returnPieceValue().ToString();

		PieceManager.instance.pieceClicked(index); 
		BoxSpawner.gridArray[row,column].GetComponent<Collider2D>().enabled = false;
		PieceManager.instance.setPieceValue(index);
	}
	

	void addScore(int row, int column){
		int total = 0;
		int secondtotal = 0; 

		if (ValidationManager.RowTotal(row, column) != int.Parse(BoxSpawner.gridArray[row,column].GetComponentInChildren<Text>().text)){
			total = ValidationManager.RowTotal(row, column);
			Debug.Log("Row Total: " + total);
			// Secondary Column Checks
			if( row-1 >= BoxSpawner.gridArray.GetLowerBound(0) && row+1 <= BoxSpawner.gridArray.GetUpperBound(0)){
				if (BoxSpawner.gridArray[row+1,column].GetComponentInChildren<Text>().text !=""
					|| BoxSpawner.gridArray[row-1,column].GetComponentInChildren<Text>().text !="" ){
						secondtotal = ValidationManager.columnTotal(row,column);
					}
					Debug.Log("Second Column Total[STATMENT 1]: " + secondtotal);
					total = total + secondtotal; 
			} else if(row+1 > BoxSpawner.gridArray.GetUpperBound(0)){
				if (BoxSpawner.gridArray[row-1,column].GetComponentInChildren<Text>().text !="" ){
						secondtotal = ValidationManager.columnTotal(row,column);
					}
					Debug.Log("Second Column Total[STATEMENT 2]: " + secondtotal);
					total = total + secondtotal; 
			} else if(row-1 < BoxSpawner.gridArray.GetLowerBound(0)){
				if (BoxSpawner.gridArray[row+1,column].GetComponentInChildren<Text>().text !=""){
						secondtotal = ValidationManager.columnTotal(row,column);
					}
					Debug.Log("Second Column Total[STATEMENT 3]: " + secondtotal);
					total = total + secondtotal; 
			}
		} else {
				total = ValidationManager.columnTotal(row, column);
				Debug.Log("Column Total: " + total);
		}

		Debug.Log("TOTAL SCORE: " + total);
		ScoreManager.instance.setPlayerScore(total);

	}

	private void Update() {
		if (PieceManager.instance.placedPieces.Count == 0){
			EndTurn.GetComponent<Button>().interactable = false; 
		} else if (PieceManager.instance.placedPieces.Count > 0){
			EndTurn.GetComponent<Button>().interactable = true; 
		}
	}

}
