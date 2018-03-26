using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AI_Player : MonoBehaviour {
	public List<Move> possiblemoves;
	public static AI_Player instance;
	private int ShuffleCounter;

	void Start(){
		setup();
	}

	public void setup(){
		possiblemoves = new List<Move>();
		instance = this;
		ShuffleCounter = 0;

	}

public void GetPossibleMoves(){
//For Each Piece
	for (int p = 0; p < PieceManager.pieceArray.Length; p++){
//Loop through the Rows & Columns
		for (int i = 0; i < 5; i++){
			for (int j = 0; j < 5; j++){
//Check if the Current position is Empty
				if (BoxSpawner.instance.IsPositionEmpty(i,j) && BoxSpawner.gridArray[i,j].GetComponent<Collider2D>().enabled){
//Add to the List of Possible Moves
					BoxSpawner.gridArray[i,j].GetComponentInChildren<Text>().text = PieceManager.pieceArray[p].GetComponentInChildren<Text>().text;
					possiblemoves.Add(
						new Move {
							row = i,
							column = j,
							pieceValue = int.Parse(BoxSpawner.gridArray[i,j].GetComponentInChildren<Text>().text),
							pieceIndex = p,
							totalScore = returnTotalScore(i,j)
						}
					);
					BoxSpawner.gridArray[i,j].GetComponentInChildren<Text>().text = "";
				}
			}
		}
	}
//Check if the Move Is Valid
	removeInValidPlacements();
//Filter Out the Even Totals
	removeEvenTotals();

//Sort to lowest > highest
	possiblemoves.Sort(delegate(Move a , Move b){
		return a.totalScore.CompareTo(b.totalScore);
	});

	if (possiblemoves.Count != 0){
		placeMove();
	} else if (possiblemoves.Count == 0){
			returnToHumanPlayer();
	}
}


public void returnToHumanPlayer(){
	Debug.Log("WE HAVE FOUND NO MOVES. Switching back to Human Player.");
	ShuffleCounter++;
		if (ShuffleCounter == 2){
			ShuffleCounter = 0;
			PieceManager.instance.SwapPieces(Random.Range(0,PieceManager.pieceArray.Length-1));
		} else {	
			TurnManagement.instance.skipTurn();	
		}
}


void removeInValidPlacements(){
	List<Move> removedInvalids = new List<Move>();
	foreach (Move m in possiblemoves){
		if (ValidationManager.PositioningValidation(m.row,m.column)){
			removedInvalids.Add(m);
		}
	}
	possiblemoves.Clear();
	possiblemoves.AddRange(removedInvalids);
}


public int returnTotalScore(int row,int column){

	int total = ValidationManager.RowTotal(row,column);
	if (total == int.Parse(BoxSpawner.gridArray[row,column].GetComponentInChildren<Text>().text)){
		total = ValidationManager.columnTotal(row,column);
		if (ValidationManager.secondaryRowCheck(row,column)){
			total += ValidationManager.RowTotal(row,column);
		}
	}
	else {
		if (ValidationManager.secondaryColumnCheck(row,column)){
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
