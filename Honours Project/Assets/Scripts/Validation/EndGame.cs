using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGame: MonoBehaviour {
	private GameObject victoryScreen; 
	private GameObject WinnerText; 
	public static EndGame instance;

	private GameObject endGameScreen; 

	private void Awake() {
		setInstance();	
	} 

	public void setInstance(){
		instance = this; 
		WinnerText = GameObject.Find("WinnerText"); 
		victoryScreen = GameObject.Find("VictoryPanel");
		endGameScreen = GameObject.Find("EndGamePrompt"); 
		
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

	public void disableVictoryScreen(){
		victoryScreen.SetActive(false);
	}

	public void enableScreen(){
		victoryScreen.SetActive(true);
	}

	public void determineWinner(){
		int p1Score = ScoreManager.instance.returnPlayerScore(1);
		int p2Score = ScoreManager.instance.returnPlayerScore(2);
		enableScreen(); 

		if (p1Score > p2Score){
			DisplayWinner(1);
		} else if (p2Score > p1Score){
			DisplayWinner(2);
		} else if (p1Score == p2Score){
			DisplayWinner(0);
		}
	}

	 void DisplayWinner(int winner){
		if (winner == 1){
			WinnerText.GetComponent<Text>().text = "Congratulations! You the won!";
		} else if (winner == 2){
			WinnerText.GetComponent<Text>().text = "The Computer won! Better luck next time!";
		} else {
			WinnerText.GetComponent<Text>().text = "It is a tie!";
		}
		
	}

	public void RestartGame(){
		SceneManager.LoadScene("GameScreen");
	}

	public void QuitGame(){
		SceneManager.LoadScene("Title Screen");
	}

	public bool checkIfCurrentPlayerIsEmpty(){
		if (NumberBag.numbers.Count == 0){
			int usedpieces = 0; 
			for(int i = 0; i < PieceManager.pieceArray.Length; i++){
				if (!PieceManager.IsElementActive(i)){
					usedpieces++;
				}	
			}

			if (usedpieces == PieceManager.pieceArray.Length){
				return true; 
			} else {
				return false; 
			}
		} else {
			return false; 
		}
	}

	public void EnableEndGame(){
		endGameScreen.SetActive(true);
	}

	public void DisableEndGame(){
		endGameScreen.SetActive(false);
	}

	public void disableAll(){
		DisableEndGame();
		disableVictoryScreen();
	}

	public void enableAll(){
		enableScreen();
		EnableEndGame();
	}
	public void EndGameYes(){
		DisableEndGame();
		determineWinner();
	}

	public void EndGameNo(){
		DisableEndGame();
		TurnManagement.instance.resetSkipCounter(); 
		TurnManagement.instance.incrementTurn();
	}


}
