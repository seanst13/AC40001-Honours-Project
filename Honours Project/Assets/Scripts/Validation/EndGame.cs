using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGame: MonoBehaviour {
	public GameObject victoryScreen; 
	public Text WinnerText; 
	public static EndGame instance;

	private void Awake() {
		instance = this; 	
	} 

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

	public void disableScreen(){
		victoryScreen.SetActive(false);
	}

	public void determineWinner(){
		int p1Score = ScoreManager.instance.returnPlayerScore(1);
		int p2Score = ScoreManager.instance.returnPlayerScore(2);

		victoryScreen.SetActive(true);

		if (p1Score > p2Score){
			DisplayWinner(1);
		} else if (p2Score > p1Score){
			DisplayWinner(2);
		} else if (p1Score == p2Score){
			DisplayWinner(0);
		}
	}

	 void DisplayWinner(int winner){
		if (winner > 0){
			WinnerText.text = "Player " + winner + " is the winner!";
		} else {
			WinnerText.text = "It is a tie!";
		}
		
	}

	public void RestartGame(){
		SceneManager.LoadScene("GameScreen");
	}

	public void QuitGame(){
		SceneManager.LoadScene("Title Screen");
	}

}
