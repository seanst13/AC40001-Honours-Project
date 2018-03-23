using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  

public class AI_Player : MonoBehaviour {
	public List<Move> possiblemoves; 
	public static AI_Player instance;
	

	void Start(){
		possiblemoves = new List<Move>(); 
		instance = this; 
	}

public void checkPossibleMoves(){
	if (TurnManagement.playerNumber % 2 == 0 && TurnManagement.playerNumber == 2){	
		for(int p = 0; p < PieceManager.pieceArray.Length; p++){
		PieceManager.instance.pieceClicked(p);	
		for (int i = 0; i < 5; i++){
			for(int j = 0; j < 5; j++){
				// Debug.Log("Piece: " + p + "\nRow: " + i + " Column: " + j);
				if (ValidationManager.PositioningValidation(i,j) && BoxSpawner.gridArray[i,j].GetComponentInChildren<Text>().text == ""){
					if(BoxSpawner.gridArray[i,j].GetComponent<Collider2D>().enabled){
						// Debug.Log("VALID MOVE FOUND"); 
						BoxSpawner.gridArray[i,j].GetComponentInChildren<Text>().text = PieceManager.pieceArray[p].GetComponentInChildren<Text>().text; 
						possiblemoves.Add(
							new Move {
								row = i, 
								column = j, 
								pieceValue = int.Parse(BoxSpawner.gridArray[i,j].GetComponentInChildren<Text>().text), 
								pieceIndex = p,
								totalScore = this.returnTotalScore(i,j)
							}
						);
						BoxSpawner.gridArray[i,j].GetComponentInChildren<Text>().text = ""; 
					}
				}
			}
		}

		possiblemoves.Sort(delegate(Move a , Move b){
			return a.totalScore.CompareTo(b.totalScore);
		});
		removeEvenTotals(); 

	if (possiblemoves.Count !=0){
		// foreach(Move m in possiblemoves){
		// 	Debug.Log(m.totalScore);
		// }
		placeMove(); 
	} else if (possiblemoves.Count == 0) {
		Debug.Log("NO POSSIBLE MOVES FOUND"); 
		TurnManagement.instance.skipTurn(); 
	}
		}
	}
}

public int returnTotalScore(int row,int column){
 
	int total = ValidationManager.RowTotal(row,column);
	if (total == int.Parse(BoxSpawner.gridArray[row,column].GetComponentInChildren<Text>().text)){
		total = ValidationManager.columnTotal(row,column);
		if (TurnManagement.instance.secondaryRowCheck(row,column)){
			total += ValidationManager.RowTotal(row,column);
		}
	} 
	else {
		if (TurnManagement.instance.secondaryColumnCheck(row,column)){
			total += ValidationManager.columnTotal(row,column);
		}
	}

	// Debug.Log("TOTAL SCORE OF THE THING:" + total); 
	return total;
}

void removeEvenTotals(){
	List<Move> removedEven = new List<Move>(); 
	foreach(Move m in possiblemoves){
		if (m.totalScore % 2 != 0){
			removedEven.Add(m);
		}
	}
	
	possiblemoves.Clear();
	possiblemoves.AddRange(removedEven);
}


void placeMove(){
	int max = possiblemoves.Count -1;
	
	PieceManager.instance.pieceClicked(possiblemoves[max].pieceIndex);
	BoxClick.tempAddPiece(possiblemoves[max].row,possiblemoves[max].column);
	TurnManagement.instance.checkIfValid(); 
}


}
