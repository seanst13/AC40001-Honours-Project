using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class ScoreManager : MonoBehaviour {
	public static ScoreManager instance;
	public GameObject score1;
	public GameObject score2;
	public int playerOneScore;
	public int playerTwoScore; 

	// Use this for initialization
	void Start () {
		playerOneScore = 0; 
		instance = this;
		setPlayerScore(playerOneScore, 1);
		setPlayerScore(playerOneScore, 2);
	}
	
	public void setPlayerScore(int value, int player){

		if (player == 1){
			playerOneScore += value; 
			score1.GetComponent<Text>().text = playerOneScore.ToString();
		} else if (player == 2){
			playerTwoScore += value; 
			score2.GetComponent<Text>().text = playerTwoScore.ToString();
		}

		
	}



}
