using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class ScoreManager : MonoBehaviour {
	public static ScoreManager instance;
	public GameObject Scoreboard;
	public int playerScore; 

	// Use this for initialization
	void Start () {
		playerScore = 0; 
		instance = this;
		setPlayerScore(playerScore);
	}
	
	public void setPlayerScore(int value){
		playerScore = playerScore + value; 
		Scoreboard.GetComponent<Text>().text = playerScore.ToString();
	}



}
