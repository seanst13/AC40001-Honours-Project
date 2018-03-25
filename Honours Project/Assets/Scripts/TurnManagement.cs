using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnManagement : MonoBehaviour {
	private int turnCounter; 
	public GameObject EndTurn; 
	public GameObject turnText; 
	public GameObject timerText; 
	private int Time; 
	public static int playerNumber; 
	public static TurnManagement instance; 
	


	private void Start() {
		setUp(); 
	}

	public void setUp(){
		instance = this; 
		turnCounter = 0; 
		playerNumber = 0; 
		incrementTurn();
	}

	public void checkIfValid(){
		int validplays = 0; 
		int row = 0;
		int column = 0;
		bool first = true; 
		int previousrow = 0;
		int previouscol = 0; 	

		foreach(Piece placement in PieceManager.instance.placedPieces){
			row = int.Parse(placement.position.Substring(0,1));
			column = int.Parse(placement.position.Substring(2,1)); 
			
			if (OddCheck(row,column)){
				validplays++;
			}
		}
		if (validplays == PieceManager.instance.placedPieces.Count){
			if (validplays == 1){
				foreach(Piece placement in PieceManager.instance.placedPieces){
					row = int.Parse(placement.position.Substring(0,1));
					column = int.Parse(placement.position.Substring(2,1)); 
					addPiece(row,column, placement.index);
					addScore(row,column);
					secondaryTotalCheck(row, column, "row"); 				
				}
			} else if (validplays > 1){
					foreach(Piece placement in PieceManager.instance.placedPieces){
						row = int.Parse(placement.position.Substring(0,1));
						column = int.Parse(placement.position.Substring(2,1)); 
						if (first){
							previousrow = row;
							previouscol = column;
							first = false; 
							addPiece(row,column,placement.index);
						} else {
							//If the pieces are in the same row. 
							if (previousrow == row && previouscol != column){
								addPiece(previousrow,column,placement.index);
								addScore(previousrow,column);
								secondaryTotalCheck(previousrow,previouscol, "row");
								secondaryTotalCheck(row, column, "row");

								//DO SECONDARY FIELD CHECKS HERE

								//If the pieces are in the same column. 
							} else if (previousrow != row && previouscol == column){
								addPiece(row,previouscol,placement.index);
								addScore(row,previouscol);
								secondaryTotalCheck(previousrow,previouscol, "col");
								secondaryTotalCheck(row, column, "col");

								//DO SECONDARY FIELD CHECKS HERE

							} else {
								//need to fix the indexing and turn on placement. 
								addPiece(row,column,placement.index);
								addScore(previousrow,previouscol);
								// secondaryTotalCheck(previousrow,previouscol);
								// secondaryTotalCheck(row, column);
								addScore(row,column);
								

								//DO SECONDARY FIELD CHECKS HERE
							}

						}
					}
			}	 			 
			PieceManager.instance.placedPieces.Clear();
			Debug.Log("INCREMENT TURN - CLEARED LIST"); 
			incrementTurn(); 
		} else {
			foreach(Piece placement in PieceManager.instance.placedPieces){
				row = int.Parse(placement.position.Substring(0,1));
				column = int.Parse(placement.position.Substring(2,1));  

				BoxSpawner.gridArray[row,column].GetComponentInChildren<Text>().text = ""; 
				PieceManager.pieceArray[placement.index].SetActive(true);
			}
			PieceManager.instance.placedPieces.Clear(); 
			if(playerNumber == 2){
				Debug.Log("Your move is still some how completely invalid. Explain plz.");
				AI_Player.instance.returnToHumanPlayer(); 

			}

		}
	}

public bool OddCheck(int row, int column){
	
		return ValidationManager.RowValidation(row, column); 
	
	}

	void addPiece(int row, int column, int index){
		PieceManager.instance.pieceClicked(index); 
		BoxSpawner.gridArray[row,column].GetComponent<Collider2D>().enabled = false;
		PieceManager.pieceArray[index].SetActive(true);
		PieceManager.instance.setPieceValue(index);
	}
	

	void addScore(int row, int column){
		int total = 0;
		int secondtotal = 0; 

		if (ValidationManager.RowTotal(row, column) != int.Parse(BoxSpawner.gridArray[row,column].GetComponentInChildren<Text>().text)){
			total = ValidationManager.RowTotal(row, column);
			Debug.Log("Row Total: " + total);
			
			//TO MOVE INTO ITS OWN SCORE METHOD.

				Debug.Log("Second Column Total: " + secondtotal);
				total = total + secondtotal; 
			
		} else {
				total = ValidationManager.columnTotal(row, column);
				Debug.Log("Column Total: " + total);
		}

		Debug.Log("TOTAL SCORE: " + total);
		ScoreManager.instance.setPlayerScore(total,playerNumber);

	}

	public void secondaryTotalCheck(int row, int column, string type){
		if (type == "row"){
			if(ValidationManager.secondaryColumnCheck(row, column)){
				ScoreManager.instance.setPlayerScore(ValidationManager.columnTotal(row,column),playerNumber);
			}
		} else if (type == "col"){
			if(ValidationManager.secondaryRowCheck(row,column)){
				ScoreManager.instance.setPlayerScore(ValidationManager.RowTotal(row,column),playerNumber);
			}
		}
	}


	private void Update() {
		if (PieceManager.instance.placedPieces.Count == 0){
			EndTurn.GetComponent<Button>().interactable = false; 
		} else if (PieceManager.instance.placedPieces.Count > 0){
			EndTurn.GetComponent<Button>().interactable = true; 
		}
	}

	public void incrementTurn(){
		StopAllCoroutines();
		if (playerNumber != 0){ PieceManager.instance.swapPreviousPlayersVals();} 
		ChangePlayer(); 
		turnCounter++;
		Debug.Log("TURN COUNTER " + turnCounter);
		turnText.GetComponent<Text>().text = "Turn " + turnCounter;
		if (playerNumber ==2){
			AI_Player.instance.GetPossibleMoves(); 
		}
		else {StartCoroutine(countdown(60));}
		
		//countdown(60);
	}

	public IEnumerator countdown(int value){
		Time = value; 
		while (Time > 0){
			timerText.GetComponent<Text>().text = Time.ToString();
			yield return new WaitForSeconds(1.0f);
			Time--; 
		}

		if (Time == 0){
			Debug.Log("INCREMENT TURN - TIME = 0");
			//incrementTurn(); 
			
		}

	}

	void ChangePlayer(){
		if (playerNumber == 0){
			playerNumber = 1;
		} else if (playerNumber == 1){
			playerNumber = 2;
		} else if (playerNumber == 2){
			playerNumber = 1; 
		}
	}

	public void skipTurn(){
		PieceManager.instance.ClearPlacedPieces(); 
		incrementTurn(); 
	}

	public int returnPlayerNumber(){
		return playerNumber; 
	}

	public int returnTurnCounter(){
		return turnCounter; 
	}
}
