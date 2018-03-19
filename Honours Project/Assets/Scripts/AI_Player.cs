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
	for(int p = 0; p < PieceManager.pieceArray.Length; p++){	
		for (int i = 0; i < 5; i++){
			for(int j = 0; j < 5; j++){
				Debug.Log("Piece: " + p + "\nRow: " + i + " Column: " + j);
				if (ValidationManager.PositioningValidation(i,j) && BoxSpawner.gridArray[i,j].GetComponentInChildren<Text>().text == ""){
					if (ValidationManager.RowValidation(i,j)){
						Debug.Log("VALID MOVE FOUND"); 
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

		//possiblemoves.Sort();

	foreach(Move m in possiblemoves){
		Debug.Log("Position: " + m.row +"_"+ m.column + " Total Score: " + m.totalScore); 
		Debug.Log("Piece Value: " + m.pieceValue);
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
	} else {
		if (TurnManagement.instance.secondaryColumnCheck(row,column)){
			total += ValidationManager.columnTotal(row,column);
		}
	}

	Debug.Log("TOTAL SCORE OF THE THING:" + total); 
	return total;
}




}
