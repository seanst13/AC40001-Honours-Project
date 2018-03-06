using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnManagement : MonoBehaviour {


	void checkIfValid(){
		int validplays = 0; 
		foreach(string position in PieceManager.instance.placedPieces){
			int row = int.Parse(position.Substring(0,1));
			int column = int.Parse(position.Substring(2,1));  

			if (OddCheck(row,column)){
				validplays++;
			}
		}
		if (validplays == PieceManager.instance.placedPieces.Count){
			foreach(string position in PieceManager.instance.placedPieces){
				int row = int.Parse(position.Substring(0,1));
				int column = int.Parse(position.Substring(2,1)); 

				addPiece(row,column);
				addScore(row,column); //Might need to add this outside the loop. 


			}	
		}
	}

	bool OddCheck(int row, int column){
	
		return ValidationManager.RowValidation(row, column); 
	
	}

	void addPiece(int row, int column){
		//BoxSpawner.gridArray[row,column].GetComponentInChildren<Text>().text = PieceManager.instance.returnPieceValue().ToString();

		PieceManager.instance.pieceClicked(PieceManager.instance.returnIndex()); 
		GetComponent<Collider2D>().enabled = false;
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
