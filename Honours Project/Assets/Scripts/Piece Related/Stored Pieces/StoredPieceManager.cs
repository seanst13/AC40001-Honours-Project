using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class StoredPieceManager : MonoBehaviour {

	private List<StoredPiece> stored; 
	public static  StoredPieceManager instance; 


	// Use this for initialization
	void Start () {
		SetUp(); 
	}
	
	public void SetUp(){
		instance = this; 
		stored = new List<StoredPiece>(); 
	}


public void addToStoredPieces(){
		for(int i = 0; i < PieceManager.pieceArray.Length; i++){
			stored.Add(new StoredPiece{
				pieceValue = PieceManager.pieceArray[i].GetComponentInChildren<Text>().text,
				playerNumber = TurnManagement.playerNumber,
				pieceArrayIndex = i
				}
			);
		}
	}

	public void swapPreviousPlayersVals(){
		if (stored.Count == 0){
			for (int i = 0; i < PieceManager.pieceArray.Length; i++)
			{
				addToStoredPieces();
				PieceManager.instance.setPieceValue(i); 
			}
		} else if (stored.Count != 0){
			checkIfStoredPiecesMatch(); 					
		}
	}


	public void checkIfStoredPiecesMatch(){
		string indexes = ""; 
		foreach (StoredPiece piece in stored){
			if (piece.playerNumber == TurnManagement.playerNumber){
				indexes += stored.IndexOf(piece).ToString();
			}
		}
		if (indexes == ""){
			for(int i = 0; i < PieceManager.pieceArray.Length; i++){
				PieceManager.instance.setPieceValue(i);
			}
		} else if (indexes != ""){
			allocateStoredPieces(indexes);	
			}	
		}
	

	public void allocateStoredPieces(string indexes){
		Debug.Log("Indexes: " + indexes); 
		foreach (char i in indexes){
			int val = int.Parse(i.ToString());
			Debug.Log("Stored Value at ["+val+"]: " + stored[val].pieceValue);
			Debug.Log("Text Value at: ["+stored[val].pieceArrayIndex+"]"+ PieceManager.pieceArray[stored[val].pieceArrayIndex].GetComponentInChildren<Text>().text);
			PieceManager.pieceArray[stored[val].pieceArrayIndex].GetComponentInChildren<Text>().text = stored[val].pieceValue;
		}

		if (indexes.Length != PieceManager.pieceArray.Length){
			for (int i = 0; i < PieceManager.pieceArray.Length; i++){
					if (!indexes.Contains(i.ToString())){
						PieceManager.instance.setPieceValue(i);
				}
			}
		}
		
		for(int i = indexes.Length-1; i >= 0; i--){
				stored.RemoveAt(int.Parse(indexes[i].ToString()));
		}
	}
}