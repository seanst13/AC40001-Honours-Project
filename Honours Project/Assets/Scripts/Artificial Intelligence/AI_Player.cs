using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AI_Player : MonoBehaviour {
	public List<Move> possiblemoves;
	public static AI_Player instance;
	private int ShuffleCounter;

	void Start(){
		SetUp();
	}

	public void SetUp(){
		possiblemoves = new List<Move>();
		instance = this;
		ShuffleCounter = 0;
	}

	public void GetPossibleMoves(){
		for(int i = 0; i < PieceManager.pieceArray.Length; i++){
			Debug.Log("Piece" + PieceManager.pieceArray[i].GetComponentInChildren<Text>().text);
		}
	//For Each Piece
		for (int p = 0; p < PieceManager.pieceArray.Length; p++){
	//Loop through the Rows & Columns
			for (int i = 0; i < 5; i++){
				for (int j = 0; j < 5; j++){
					AddToList(i,j,p);
				}	
			}
		}
	}

	public void makeMove(){
		Debug.Log("Total Possible Moves before filtering:" + possiblemoves.Count);
		filterAndSortMoves();
		Debug.Log("Total Possible Moves after filtering:" + possiblemoves.Count);
		if (possiblemoves.Count != 0){
			DisplayPossibleMoves();
			placeMove();
		} else if (possiblemoves.Count == 0){
				Debug.Log("No Possible Moves. ");
				returnToHumanPlayer();
		}
	}

	public void AddToList(int row, int column, int index){
		//Check if the Current position is Empty
		if (BoxSpawner.instance.IsPositionEmpty(row,column) && BoxSpawner.gridArray[row,column].GetComponent<Collider2D>().enabled){
	//Add to the List of Possible Moves
			possiblemoves.Add(
				new Move {
					row = row, 
					column = column,
					pieceValue = int.Parse(PieceManager.pieceArray[index].GetComponentInChildren<Text>().text),
					pieceIndex = index,
					totalScore = returnTotalScore(row,column,int.Parse(PieceManager.pieceArray[index].GetComponentInChildren<Text>().text))
				}
			);
		}
	}

	public void filterAndSortMoves(){
		//Check if the Move Is Valid
		removeInValidPlacements();
		//Filter Out the Even Totals
		removeEvenTotals();
		addSecondaryScoring();

		//Sort to lowest > highest
		possiblemoves.Sort(delegate(Move a , Move b){
			return a.totalScore.CompareTo(b.totalScore);
		});
	}

	public void DisplayPossibleMoves(){
		foreach(Move m in possiblemoves){
			Debug.Log("Possible Move: " + m.pieceValue + " at [" + m.row + "," + m.column + "], scoring: " + m.totalScore); 
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

public void removeInValidPlacements(){
		List<Move> removedInvalids = new List<Move>();
		foreach (Move m in possiblemoves){
			if (ValidationManager.PositioningValidation(m.row,m.column) && BoxSpawner.instance.IsPositionEmpty(m.row,m.column)){
				if (ValidationManager.newRowValidation(m.row,m.column,m.pieceValue) && ValidationManager.newColValidation(m.row,m.column, m.pieceValue)){
					if (m.pieceValue == int.Parse(PieceManager.pieceArray[m.pieceIndex].GetComponentInChildren<Text>().text)){
						removedInvalids.Add(m);
					}
				}
			}
		}
		possiblemoves.Clear();
		possiblemoves.AddRange(removedInvalids);
	}

	public List<Move> returnPossibleMoves(){
		return possiblemoves; 
	}

	public int returnTotalScore(int row,int column, int valOfPiece){

		int total = ValidationManager.RowTotal(row,column) + valOfPiece;
		if (total == valOfPiece){
			total = ValidationManager.columnTotal(row,column) + valOfPiece;
		}
		return total;
	}


	public void addSecondaryScoring(){
		foreach (Move m in possiblemoves){
			if ((ValidationManager.RowTotal(m.row, m.column) + m.pieceValue ) == m.totalScore){
				if (ValidationManager.columnTotal(m.row,m.column) !=0) {
					m.totalScore += (ValidationManager.columnTotal(m.row,m.column) + m.pieceValue);
				}
			} else if ((ValidationManager.columnTotal(m.row, m.column) + m.pieceValue ) == m.totalScore){
				if (ValidationManager.RowTotal(m.row,m.column) !=0 ){
					m.totalScore += (ValidationManager.RowTotal(m.row,m.column) + m.pieceValue);
				}
			}
		}
	}

	public void removeEvenTotals(){
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
		int max = possiblemoves.Count-1;

		PieceManager.instance.pieceClicked(possiblemoves[max].pieceIndex);
		BoxClick.tempAddPiece(possiblemoves[max].row,possiblemoves[max].column);
		TurnManagement.instance.checkIfValid();
	}

	public int returnScoreAtPosition(int pos){
		return possiblemoves[pos].totalScore;
	}
}