using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour {

	public static bool gridIsComplete(){
		int filledpieces = 0; 

		for(int i = 0; i < 5; i++){
			for (int j = 0; j < 5; j++){
				if(!BoxSpawner.instance.IsPositionEmpty(i,j)){
					filledpieces++;
				}
			}
		}
	
		if (filledpieces == 25){
			return true;
		} else {
			return false; 
		}
	}


	public static void determineWinner(){
		int p1Score = ScoreManager.instance.returnPlayerScore(1);
		int p2Score = ScoreManager.instance.returnPlayerScore(2);

		if (p1Score > p2Score){
			//Player 1 is the Winner
		} else if (p1Score < p2Score){
			//Player 2 is the Winner
		} else if (p1Score == p2Score){
			//Neither Win - it is a draw. 
		}
	}
}
