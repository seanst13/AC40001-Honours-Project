using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class ScoreManager : MonoBehaviour {
	public static ScoreManager instance;
	private GameObject score1;
	private GameObject score2;
	private int playerOneScore;
	private int playerTwoScore; 

	// Use this for initialization
	void Start () {
		setup();
	}
	
	public void setup(){
		playerOneScore = 0;
		playerTwoScore = 0;  
		instance = this;

		score1 = GameObject.Find("PlayerScore");
		score2 = GameObject.Find("ComputerScore"); 

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

	public int returnPlayerScore(int player){
		if (player == 1){
			return playerOneScore;
		} else if (player == 2){
			return playerTwoScore; 
		} else {
			return -1; 
		}
	}



}
