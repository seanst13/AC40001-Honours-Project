﻿using System.Collections;
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
		List<SubMove> removedInvalids = new List<SubMove>();
		foreach (SubMove m in possiblemoves){
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
			if ((ValidationManager.newRowValidation(m.row,m.column,m.pieceValue) && ValidationManager.newColValidation(m.row,m.column,m.pieceValue) 
			|| (!ValidationManager.newRowValidation(m.row,m.column,m.pieceValue) && ValidationManager.newColValidation(m.row,m.column,m.pieceValue) 
			||(ValidationManager.newRowValidation(m.row,m.column,m.pieceValue) && !ValidationManager.newColValidation(m.row,m.column,m.pieceValue))))){
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
					obtainSecondPlacements(i, m);
				}
			}
			
		}
	}

	public void obtainSecondPlacements(int pieceIndex, SubMove m){
		int pieceval = int.Parse(PieceManager.pieceArray[pieceIndex].GetComponentInChildren<Text>().text);
		
		if (pieceval %2 !=0 && (!ValidationManager.newRowValidation(m.row,m.column, pieceval) || !ValidationManager.newColValidation(m.row,m.column,pieceval))
		|| pieceval %2 ==0 && (ValidationManager.newRowValidation(m.row,m.column, pieceval) || ValidationManager.newColValidation(m.row,m.column,pieceval))) 
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
			if (ValidationManager.newRowValidation(i, m.column, pieceval) && ValidationManager.newColValidation(i,m.column, pieceval) 
			&& (BoxSpawner.instance.IsPositionEmpty(i,m.column) && ValidationManager.PositioningValidation(i,m.column))){
				posSecondMoves.Add(new Move{
					row = i, column = m.column, pieceValue = pieceval, pieceIndex = pieceindex, totalScore =  m.pieceValue + ValidationManager.RowTotal(i, m.column) + pieceval,
					 totalIsRow = true
				});
			}
		}

		//Col checks
		for (int i = 0; i < 5; i++){
			if (ValidationManager.newRowValidation(m.row, i, pieceval) && ValidationManager.newColValidation(m.row,i, pieceval) 
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

	public void removeEvenTotals(){
		List<SubMove> moves = new List<SubMove>(); 
		foreach (SubMove m in possiblemoves){
			if (ValidationManager.newRowValidation(m.row,m.column, m.pieceValue) && ValidationManager.newColValidation(m.row, m.column, m.pieceValue)){
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