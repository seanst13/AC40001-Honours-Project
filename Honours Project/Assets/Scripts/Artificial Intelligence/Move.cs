using System.Collections.Generic;
public class Move {
	public int row {get; set;}
	public int column {get; set;}
	public int pieceValue {get; set;}
	public int pieceIndex {get; set;}
	public int totalScore {get; set;} 
	//True means the total belongs to the row total, false means its the column total;
	public bool totalIsRow; 

	public List<Move> secondaryMoves = new List<Move>(); 
 
}
