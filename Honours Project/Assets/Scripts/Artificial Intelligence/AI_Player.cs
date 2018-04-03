using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AI_Player : MonoBehaviour {
	public List<SubMove> possiblemoves;
	public static AI_Player instance;
	private int ShuffleCounter;

	void Start(){
		SetUp();
	}

	public void SetUp(){
		possiblemoves = new List<SubMove>();
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
				new SubMove {
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

	public bool setTotalIsRow(int row, int column, int piecevalue){
		if ((ValidationManager.RowTotal(row,column) + piecevalue) != piecevalue){
			return true;
		} else {
			return false;
		}

	}

	public void filterAndSortMoves(){
		//Check if the Move Is Valid
		removeInValidPlacements();
		//Filter Out the Even Totals
		removeCompleteEvenTotals();
		getSecondPlacements();
		removeEvenTotals();
		addSecondaryScoring();

		//Sort to lowest > highest
		possiblemoves.Sort(delegate(SubMove a , SubMove b){
			return a.totalScore.CompareTo(b.totalScore);
		});
	}

	public void DisplayPossibleMoves(){
		foreach(SubMove m in possiblemoves){
			Debug.Log("Possible Move: " + m.pieceValue + " at [" + m.row + "," + m.column + "], scoring: " + m.totalScore);
			if (m.secondaryMoves.Count != 0){
			foreach(Move n in m.secondaryMoves){
				Debug.Log("Possible Secondary Move: " + n.pieceValue + " at [" + n.row + "," + n.column + "], scoring: " + n.totalScore);
			}
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
			} else {
				TurnManagement.instance.skipTurn();
			}
	}

public void removeInValidPlacements(){
		List<SubMove> removedInvalids = new List<SubMove>();
		foreach (SubMove m in possiblemoves){
			if (ValidationManager.PositioningValidation(m.row,m.column) && BoxSpawner.instance.IsPositionEmpty(m.row,m.column)){
				if (ValidationManager.RowValidation(m.row,m.column,m.pieceValue) && ValidationManager.ColumnValidation(m.row,m.column, m.pieceValue)){
					if (m.pieceValue == int.Parse(PieceManager.pieceArray[m.pieceIndex].GetComponentInChildren<Text>().text)){
						removedInvalids.Add(m);
					}
				}
			}
		}
		possiblemoves.Clear();
		possiblemoves.AddRange(removedInvalids);
	}

	public List<SubMove> returnPossibleMoves(){
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

	public void removeCompleteEvenTotals(){
		List<SubMove> removedEven = new List<SubMove>();
		foreach(SubMove m in possiblemoves){
			if ((ValidationManager.RowValidation(m.row,m.column,m.pieceValue) && ValidationManager.ColumnValidation(m.row,m.column,m.pieceValue)
			|| (!ValidationManager.RowValidation(m.row,m.column,m.pieceValue) && ValidationManager.ColumnValidation(m.row,m.column,m.pieceValue)
			||(ValidationManager.RowValidation(m.row,m.column,m.pieceValue) && !ValidationManager.ColumnValidation(m.row,m.column,m.pieceValue))))){
				removedEven.Add(m);
			}
		}

		possiblemoves.Clear();
		possiblemoves.AddRange(removedEven);
	}

	public void getSecondPlacements(){
		foreach(SubMove m in possiblemoves){
			for(int i = 0; i < PieceManager.pieceArray.Length; i++){
				if (i != m.pieceIndex){
					filterForSecondaryPositions(i);
				}
			}

		}
	}
#region
	public void obtainSecondPlacements(int pieceIndex, SubMove m){
		int pieceval = int.Parse(PieceManager.pieceArray[pieceIndex].GetComponentInChildren<Text>().text);

		if (pieceval %2 !=0 && (!ValidationManager.RowValidation(m.row,m.column, pieceval) || !ValidationManager.ColumnValidation(m.row,m.column,pieceval))
		|| pieceval %2 ==0 && (ValidationManager.RowValidation(m.row,m.column, pieceval) || ValidationManager.ColumnValidation(m.row,m.column,pieceval)))
		{
			List <Move> moves = checkForValidMoves(m, pieceIndex);
			if (moves != null) {
				moves.Sort(delegate(Move a , Move b){
				return a.totalScore.CompareTo(b.totalScore);});
				possiblemoves[possiblemoves.IndexOf(m)].secondaryMoves.AddRange(moves);
			}
		}

	}

	public List<Move> checkForValidMoves(SubMove m, int pieceindex){
		int pieceval = int.Parse(PieceManager.pieceArray[pieceindex].GetComponentInChildren<Text>().text);
		List <Move> posSecondMoves = new List<Move>();
		//Row Checks
		for (int i = 0; i < 5; i++){
			if (ValidationManager.RowValidation(i, m.column, pieceval) && ValidationManager.ColumnValidation(i,m.column, pieceval)
			&& (BoxSpawner.instance.IsPositionEmpty(i,m.column) && ValidationManager.PositioningValidation(i,m.column))){
				posSecondMoves.Add(new Move{
					row = i, column = m.column, pieceValue = pieceval, pieceIndex = pieceindex, totalScore =  m.pieceValue + ValidationManager.RowTotal(i, m.column) + pieceval,
					 totalIsRow = true
				});
			}
		}

		//Col checks
		for (int i = 0; i < 5; i++){
			if (ValidationManager.RowValidation(m.row, i, pieceval) && ValidationManager.ColumnValidation(m.row,i, pieceval)
			&& (BoxSpawner.instance.IsPositionEmpty(m.row,i) && ValidationManager.PositioningValidation(m.row, i))){
				posSecondMoves.Add(new Move{
					row = m.row, column = i, pieceValue = pieceval, pieceIndex = pieceindex, totalScore = m.pieceValue + ValidationManager.columnTotal(m.row, i) + pieceval,
					totalIsRow = false
				});
			}
		}
		//Check if its null

		if (posSecondMoves.Count == 0){
			return null;
		} else {
			return posSecondMoves;
		}

	}
#endregion
	public void filterForSecondaryPositions(int index){
		foreach (SubMove m in possiblemoves){
			List<Move> moves = new List<Move>(); 
			if (ValidationManager.RowValidation(m.row,m.column,m.pieceValue) && ValidationManager.ColumnValidation(m.row,m.column,m.pieceValue)){
				moves = getAddtionalScore(m, index);
			} else if ((!ValidationManager.RowValidation(m.row,m.column,m.pieceValue) && ValidationManager.ColumnValidation(m.row,m.column,m.pieceValue))
			|| (ValidationManager.RowValidation(m.row,m.column,m.pieceValue) && !ValidationManager.ColumnValidation(m.row,m.column,m.pieceValue))){
				moves = getTotalsCorrect(m, index);
		}
			if (moves !=null){
				moves.Sort(delegate(Move a , Move b){
				return a.totalScore.CompareTo(b.totalScore);});
				m.secondaryMoves.AddRange(moves); 
			}
		}
	}

	public List<Move> getAddtionalScore(SubMove m, int index){
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
			secondMoves = filterEvenSecondTotals(secondMoves); 
			secondMoves = filterSecondSecondaryTotals(secondMoves);
			return secondMoves; 
		}
	}


	public List<Move> getTotalsCorrect(SubMove m, int index){
		int pieceval = int.Parse(PieceManager.pieceArray[index].GetComponentInChildren<Text>().text);
		List<Move> secondMoves = new List<Move>();

		if (!ValidationManager.RowValidation(m.row, m.column, m.pieceValue)){
			for(int i = 0; i<5; i++){
				if (i != m.row){
					if (BoxSpawner.instance.IsPositionEmpty(i,m.column) && ValidationManager.PositioningValidation(i, m.column)){
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
		secondMoves = filterEvenSecondTotals(secondMoves);
		secondMoves = filterSecondSecondaryTotals(secondMoves); 
		return secondMoves;
	}
	}

	public List<Move> filterSecondSecondaryTotals(List<Move> moves){
		foreach (Move m in moves){
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
		return moves; 
	}
	

	public List<Move> filterEvenSecondTotals(List<Move> moves){
		List<Move> filteredmoves = new List<Move>();
		foreach(Move m in moves){
			if (m.totalScore %2 !=0){
				filteredmoves.Add(m);
			}
		}
		moves.Clear();
		moves.AddRange(filteredmoves);
		return moves; 
	}

	public void removeEvenTotals(){
		List<SubMove> moves = new List<SubMove>();
		foreach (SubMove m in possiblemoves){
			if (ValidationManager.RowValidation(m.row,m.column, m.pieceValue) && ValidationManager.ColumnValidation(m.row, m.column, m.pieceValue)){
				moves.Add(m);
			}
		}
		possiblemoves.Clear();
		possiblemoves.AddRange(moves);
	}
	public bool determineIfOdd(int i){
		return int.Parse(PieceManager.pieceArray[i].GetComponentInChildren<Text>().text) %2 !=0;
	}

	void placeMove(){
		int max = possiblemoves.Count-1;

		PieceManager.instance.pieceClicked(possiblemoves[max].pieceIndex);
		BoxClick.tempAddPiece(possiblemoves[max].row,possiblemoves[max].column);
		if (possiblemoves[max].secondaryMoves.Count != 0){
			int secondMax = possiblemoves[max].secondaryMoves.Count-1;
			PieceManager.instance.pieceClicked(possiblemoves[max].secondaryMoves[secondMax].pieceIndex);
			BoxClick.tempAddPiece(possiblemoves[max].secondaryMoves[secondMax].row,possiblemoves[max].secondaryMoves[secondMax].column);
		}
		TurnManagement.instance.checkIfValid();
	}

	public int returnScoreAtPosition(int pos){
		return possiblemoves[pos].totalScore;
	}
}