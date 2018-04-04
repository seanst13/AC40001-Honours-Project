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
					totalScore = returnTotalScore(row,column,int.Parse(PieceManager.pieceArray[index].GetComponentInChildren<Text>().text)),
					totalIsRow = setTotalIsRow(row,column,int.Parse(PieceManager.pieceArray[index].GetComponentInChildren<Text>().text))
				}
			);
		}
	}

	public static bool setTotalIsRow(int row, int column, int piecevalue){
		if ((ValidationManager.RowTotal(row,column) + piecevalue) != piecevalue){
			return true;
		} else {
			return false;
		}

	}

	public void filterAndSortMoves(){
		//Check if the Move Is Valid
		possiblemoves = Filter.removeInValidPlacements(possiblemoves);
		//Filter Out the Even Totals
		possiblemoves = Filter.removeCompleteEvenTotals(possiblemoves);
		getSecondPlacements();
		possiblemoves = Filter.removeEvenTotals(possiblemoves);
		possiblemoves = Filter.addSecondaryScoring(possiblemoves);

		//Sort to lowest > highest
		possiblemoves.Sort(delegate(Move a , Move b){
			return a.totalScore.CompareTo(b.totalScore);
		});
	}

	public void DisplayPossibleMoves(){
		foreach(Move m in possiblemoves){
			Debug.Log("Possible Move: " + m.pieceValue + " at [" + m.row + "," + m.column + "], scoring: " + m.totalScore);
			if (m.returnSizeOfSecondaryMoves() != 0){
				Debug.Log("Possible Secondary Moves:" + m.returnSizeOfSecondaryMoves());

			} else {
				Debug.Log("No Secondary moves Possible");
			}
		}
	}

	public void returnToHumanPlayer(){
		Debug.Log("WE HAVE FOUND NO MOVES. Switching back to Human Player.");
		ShuffleCounter++;
			if (ShuffleCounter == 2){
				ShuffleCounter = 0;
				PieceManager.instance.SwapPieces(Random.Range(0,PieceManager.pieceArray.Length-1));
				TurnManagement.instance.skipTurn();

			} else {
				TurnManagement.instance.skipTurn();
			}
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

	public void getSecondPlacements(){
		foreach(Move m in possiblemoves){
			for(int i = 0; i < PieceManager.pieceArray.Length; i++){
				if (i != m.pieceIndex){
					filterForSecondaryPositions(i);
				}
			}

		}
	}

	public void filterForSecondaryPositions(int index){
		foreach (Move m in possiblemoves){
			List<Move> moves = new List<Move>(); 
			if (ValidationManager.RowValidation(m.row,m.column,m.pieceValue) && ValidationManager.ColumnValidation(m.row,m.column,m.pieceValue)){
				moves = getAddtionalScore(m, index);
			} else if ((!ValidationManager.RowValidation(m.row,m.column,m.pieceValue) && ValidationManager.ColumnValidation(m.row,m.column,m.pieceValue))
			|| (ValidationManager.RowValidation(m.row,m.column,m.pieceValue) && !ValidationManager.ColumnValidation(m.row,m.column,m.pieceValue))){
				moves = getTotalsCorrect(m, index);
		}
			if (moves !=null){
				moves.Sort(delegate(Move a , Move b){
					return a.totalScore.CompareTo(b.totalScore);
					}
				);
				m.returnSecondaryMoves().AddRange(moves); 
			}
		}
	}

	public List<Move> getAddtionalScore(Move m, int index){
		int pieceval = int.Parse(PieceManager.pieceArray[index].GetComponentInChildren<Text>().text);
		List<Move> secondMoves = new List<Move>();

		for(int i = 0; i < 5; i++){
			for(int j = 0; j < 5; j++){
				if (BoxSpawner.instance.IsPositionEmpty(i,j) && ValidationManager.PositioningValidation(i,j)){
					if (i != m.row && j != m.column){
						if (ValidationManager.RowValidation(i,j, pieceval) && ValidationManager.ColumnValidation(i,j,pieceval)){
							secondMoves.Add(new Move{
								row = i, column = j,
								pieceIndex = index, pieceValue = pieceval,
								totalScore = returnTotalScore(i,j,pieceval)
							});
						}
					}
				}
			}
		}

		if (secondMoves.Count == 0){
			return null;
		} else{
			secondMoves = Filter.filterEvenSecondTotals(secondMoves); 
			secondMoves = Filter.addSecondaryScoring(secondMoves);
			return secondMoves; 
		}
	}


	public static List<Move> getTotalsCorrect(Move m, int index){
		int pieceval = int.Parse(PieceManager.pieceArray[index].GetComponentInChildren<Text>().text);
		List<Move> secondMoves = new List<Move>();

		if (!ValidationManager.RowValidation(m.row, m.column, m.pieceValue)){
			for(int i = 0; i<5; i++){
				if (i != m.row){
					if (BoxSpawner.instance.IsPositionEmpty(i,m.column) && ValidationManager.PositioningValidation(i, m.column)){
						if (ValidationManager.RowValidation(i,m.column, pieceval+m.pieceValue) && ValidationManager.ColumnValidation(i,m.column, pieceval+m.pieceValue))
						{
							secondMoves.Add(new Move{
								row = i, column = m.column,
								pieceIndex = index, pieceValue = pieceval,
								totalScore = m.pieceValue  + ValidationManager.RowTotal(i,m.column) + pieceval
							});
						}
					}
				}
			}
		} else if (!ValidationManager.ColumnValidation(m.row,m.column, m.pieceValue)){
			for (int i = 0; i < 5; i ++){
				if (i !=m.column){
					if (BoxSpawner.instance.IsPositionEmpty(m.row,i) && ValidationManager.PositioningValidation(m.row, i)){
						if (ValidationManager.RowValidation(m.row,i,pieceval+m.pieceValue) && ValidationManager.ColumnValidation(m.row,i,pieceval+m.pieceValue)){
							secondMoves.Add(new Move{
								row = m.row, column = i,
								pieceIndex = index, pieceValue = pieceval,
								totalScore = m.pieceValue  + ValidationManager.columnTotal(m.row,i) + pieceval
							});
						}
					}
				}
			}
		}
	if (secondMoves.Count ==0)
		{	return null; }
	else {
		secondMoves = Filter.filterEvenSecondTotals(secondMoves); 
		secondMoves = Filter.addSecondaryScoring(secondMoves);
		return secondMoves;
	}
	}

	void placeMove(){
		int max = possiblemoves.Count-1;

		PieceManager.instance.pieceClicked(possiblemoves[max].pieceIndex);
		BoxClick.tempAddPiece(possiblemoves[max].row,possiblemoves[max].column);
		if (possiblemoves[max].returnSizeOfSecondaryMoves() != 0){
			int secondMax = possiblemoves[max].returnSizeOfSecondaryMoves()-1;
			PieceManager.instance.pieceClicked(possiblemoves[max].returnSecondaryMoves()[secondMax].pieceIndex);
			BoxClick.tempAddPiece(possiblemoves[max].returnSecondaryMoves()[secondMax].row,possiblemoves[max].returnSecondaryMoves()[secondMax].column);
		}
		TurnManagement.instance.checkIfValid();
	}

	public int returnScoreAtPosition(int pos){
		return possiblemoves[pos].totalScore;
	}
}