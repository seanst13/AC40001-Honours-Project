using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubMove: Move {

	public List<Move> secondaryMoves = new List <Move>(); 

	public List<Move> returnSecondaryMoves(){
		return secondaryMoves;
	}

}
