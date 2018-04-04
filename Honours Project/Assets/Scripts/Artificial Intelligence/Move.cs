using System.Collections.Generic;
public class Move {
	public int row {get; set;}
	public int column {get; set;}
	public int pieceValue {get; set;}
	public int pieceIndex {get; set;}
	public int totalScore {get; set;} 
	//True means the total belongs to the row total, false means its the column total
	public bool totalIsRow {get; set;} 

	private List<Move> secondaryMoves = new List<Move>(); 

	public List<Move> returnSecondaryMoves(){
		return secondaryMoves;
	}

	public int returnSizeOfSecondaryMoves(){
		return secondaryMoves.Count;
	}

	public void setList(List<Move> moves){
		secondaryMoves = moves;
	}
}
