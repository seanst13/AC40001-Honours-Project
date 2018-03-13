﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnManagement : MonoBehaviour {
	private int turnCounter; 
	public GameObject EndTurn; 
	public GameObject turnText; 

	private void Start() {
		turnCounter = 0; 
		incrementTurn(); 
	}

	public void checkIfValid(){
		int validplays = 0; 
		int row = 0;
		int column = 0;
		bool first = true; 
		int previousrow = 0;
		int previouscol = 0; 	

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
					secondaryTotalCheck(row, column, "row"); 				
				}
			} else if (validplays > 1){
					foreach(Piece placement in PieceManager.instance.placedPieces){
						row = int.Parse(placement.position.Substring(0,1));
						column = int.Parse(placement.position.Substring(2,1)); 
						if (first){
							previousrow = row;
							previouscol = column;
							first = false; 
							addPiece(row,column,placement.index);
						} else {
							//If the pieces are in the same row. 
							if (previousrow == row && previouscol != column){
								addPiece(previousrow,column,placement.index);
								addScore(previousrow,column);
								secondaryTotalCheck(previousrow,previouscol, "row");
								secondaryTotalCheck(row, column, "row");

								//DO SECONDARY FIELD CHECKS HERE

								//If the pieces are in the same column. 
							} else if (previousrow != row && previouscol == column){
								addPiece(row,previouscol,placement.index);
								addScore(row,previouscol);
								secondaryTotalCheck(previousrow,previouscol, "col");
								secondaryTotalCheck(row, column, "col");

								//DO SECONDARY FIELD CHECKS HERE

							} else {
								//need to fix the indexing and turn on placement. 
								addPiece(row,column,placement.index);
								addScore(previousrow,previouscol);
								// secondaryTotalCheck(previousrow,previouscol);
								// secondaryTotalCheck(row, column);
								addScore(row,column);
								

								//DO SECONDARY FIELD CHECKS HERE
							}

						}
					}
			}	 			 
			PieceManager.instance.placedPieces.Clear(); 
			incrementTurn(); 
		} else {
			foreach(Piece placement in PieceManager.instance.placedPieces){
				row = int.Parse(placement.position.Substring(0,1));
				column = int.Parse(placement.position.Substring(2,1));  

				BoxSpawner.gridArray[row,column].GetComponentInChildren<Text>().text = ""; 
				PieceManager.pieceArray[placement.index].SetActive(true);
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
		PieceManager.pieceArray[index].SetActive(true);
		PieceManager.instance.setPieceValue(index);
	}
	

	void addScore(int row, int column){
		int total = 0;
		int secondtotal = 0; 

		if (ValidationManager.RowTotal(row, column) != int.Parse(BoxSpawner.gridArray[row,column].GetComponentInChildren<Text>().text)){
			total = ValidationManager.RowTotal(row, column);
			Debug.Log("Row Total: " + total);
			
			//TO MOVE INTO ITS OWN SCORE METHOD.

				Debug.Log("Second Column Total: " + secondtotal);
				total = total + secondtotal; 
			
		} else {
				total = ValidationManager.columnTotal(row, column);
				Debug.Log("Column Total: " + total);
		}

		Debug.Log("TOTAL SCORE: " + total);
		ScoreManager.instance.setPlayerScore(total);

	}

	void secondaryTotalCheck(int row, int column, string type){
		if (type == "row"){
			if(secondaryColumnCheck(row, column)){
				ScoreManager.instance.setPlayerScore(ValidationManager.columnTotal(row,column));
			}
		} else if (type == "col"){
			if(secondaryRowCheck(row,column)){
				ScoreManager.instance.setPlayerScore(ValidationManager.RowTotal(row,column));
			}
		}
	}


	bool secondaryColumnCheck(int row, int column){

	if( row-1 >= BoxSpawner.gridArray.GetLowerBound(0) && row+1 <= BoxSpawner.gridArray.GetUpperBound(0)){
		if (BoxSpawner.gridArray[row+1,column].GetComponentInChildren<Text>().text !=""
			|| BoxSpawner.gridArray[row-1,column].GetComponentInChildren<Text>().text !="" ){
				return true; 
			}
	} else if(row+1 > BoxSpawner.gridArray.GetUpperBound(0)){
		if (BoxSpawner.gridArray[row-1,column].GetComponentInChildren<Text>().text !="" ){
				return true; 
			}
	} else if(row-1 < BoxSpawner.gridArray.GetLowerBound(0)){
		if (BoxSpawner.gridArray[row+1,column].GetComponentInChildren<Text>().text !=""){
				return true; 
			}
	} else { return false; }
	return false; 
	}

	bool secondaryRowCheck(int row, int column){

	if( column-1 >= BoxSpawner.gridArray.GetLowerBound(1) && column+1 <= BoxSpawner.gridArray.GetUpperBound(1)){
		if (BoxSpawner.gridArray[row,column-1].GetComponentInChildren<Text>().text !=""
			|| BoxSpawner.gridArray[row,column+1].GetComponentInChildren<Text>().text !="" ){
				return true; 
			}
	} else if(column+1 > BoxSpawner.gridArray.GetUpperBound(1)){
		if (BoxSpawner.gridArray[row,column-1].GetComponentInChildren<Text>().text !="" ){
				return true; 
			}
	} else if(column-1 < BoxSpawner.gridArray.GetLowerBound(1)){
		if (BoxSpawner.gridArray[row,column+1].GetComponentInChildren<Text>().text !=""){
				return true; 
			}
	} else { return false; }
	return false; 
	}



	private void Update() {
		if (PieceManager.instance.placedPieces.Count == 0){
			EndTurn.GetComponent<Button>().interactable = false; 
		} else if (PieceManager.instance.placedPieces.Count > 0){
			EndTurn.GetComponent<Button>().interactable = true; 
		}
	}

	void incrementTurn(){
		turnCounter++;
		turnText.GetComponent<Text>().text = "Turn " + turnCounter;

	}

}
