using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Filter: MonoBehaviour{
 public static List<Move> addSecondaryScoring(List<Move> possiblemoves){
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
        return possiblemoves; 
	}

    public static List<Move> removeInValidPlacements(List<Move> possiblemoves){
		List<Move> removedInvalids = new List<Move>();
		foreach (Move m in possiblemoves){
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
        return possiblemoves; 
	}

	public static List<Move> removeCompleteEvenTotals(List<Move> possiblemoves){
		List<Move> removedEven = new List<Move>();
		foreach(Move m in possiblemoves){
			if ((ValidationManager.RowValidation(m.row,m.column,m.pieceValue) && ValidationManager.ColumnValidation(m.row,m.column,m.pieceValue)
			|| (!ValidationManager.RowValidation(m.row,m.column,m.pieceValue) && ValidationManager.ColumnValidation(m.row,m.column,m.pieceValue)
			||(ValidationManager.RowValidation(m.row,m.column,m.pieceValue) && !ValidationManager.ColumnValidation(m.row,m.column,m.pieceValue))))){
				removedEven.Add(m);
			}
		}

		possiblemoves.Clear();
		possiblemoves.AddRange(removedEven);
        return possiblemoves; 
	}

	public static List<Move> filterEvenSecondTotals(List<Move> moves){
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

	public static List<Move> removeEvenTotals(List<Move> possiblemoves){
		List<Move> moves = new List<Move>();
		foreach (Move m in possiblemoves){
			if (ValidationManager.RowValidation(m.row,m.column, m.pieceValue) && ValidationManager.ColumnValidation(m.row, m.column, m.pieceValue)){
				moves.Add(m);
			}
		}
		possiblemoves.Clear();
		possiblemoves.AddRange(moves);
		return possiblemoves; 
	}

}