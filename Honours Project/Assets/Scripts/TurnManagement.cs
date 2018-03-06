using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnManagement : MonoBehaviour {


	public void checkIfValid(){
		int validplays = 0; 
		int row = 0;
		int column = 0;
		foreach(string position in PieceManager.instance.placedPieces){
			row = int.Parse(position.Substring(0,1));
			column = int.Parse(position.Substring(2,1));  

			if (OddCheck(row,column)){
				validplays++;
			}
		}
		if (validplays == PieceManager.instance.count){
			foreach(string position in PieceManager.instance.placedPieces){
				row = int.Parse(position.Substring(0,1));
				column = int.Parse(position.Substring(2,1)); 

				addPiece(row,column);
				addScore(row,column); //Might need to add this outside the loop. 


			}	
			PieceManager.instance.placedPieces.Clear(); 
		} else {
			foreach (string position in PieceManager.instance.placedPieces){
				row = int.Parse(position.Substring(0,1));
				column = int.Parse(position.Substring(2,1)); 

				BoxSpawner.gridArray[row,column].GetComponentInChildren<Text>().text = ""; 
			}
		}
	}

	bool OddCheck(int row, int column){
	
		return ValidationManager.RowValidation(row, column); 
	
	}

	void addPiece(int row, int column){
		//BoxSpawner.gridArray[row,column].GetComponentInChildren<Text>().text = PieceManager.instance.returnPieceValue().ToString();

		PieceManager.instance.pieceClicked(PieceManager.instance.returnIndex()); 
		BoxSpawner.gridArray[row,column].GetComponent<Collider2D>().enabled = false;
		PieceManager.instance.setPieceValue(PieceManager.instance.returnIndex());
	}
	

	void addScore(int row, int column){
		int total = 0;
		int secondtotal = 0; 

		if (ValidationManager.RowTotal(row, column) != PieceManager.instance.returnPieceValue()){
			total = ValidationManager.RowTotal(row, column);
			// Secondary Column Checks
			if( row-1 >= BoxSpawner.gridArray.GetLowerBound(0) && row+1 <= BoxSpawner.gridArray.GetUpperBound(0)){
				if (BoxSpawner.gridArray[row+1,column].GetComponentInChildren<Text>().text !=""
					|| BoxSpawner.gridArray[row-1,column].GetComponentInChildren<Text>().text !="" ){
						secondtotal = ValidationManager.columnTotal(row,column);
					}
					total = total + secondtotal; 
			} else if(row+1 > BoxSpawner.gridArray.GetUpperBound(0)){
				if (BoxSpawner.gridArray[row-1,column].GetComponentInChildren<Text>().text !="" ){
						secondtotal = ValidationManager.columnTotal(row,column);
					}
					total = total + secondtotal; 
			} else if(row-1 < BoxSpawner.gridArray.GetLowerBound(0)){
				if (BoxSpawner.gridArray[row+1,column].GetComponentInChildren<Text>().text !=""){
						secondtotal = ValidationManager.columnTotal(row,column);
					}
					total = total + secondtotal; 
			}
		} else {
				total = ValidationManager.columnTotal(row, column);
		}

		Debug.Log("TOTAL SCORE: " + total);
		ScoreManager.instance.setPlayerScore(total);

	}

}
