using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PieceClick : MonoBehaviour {
	public GameObject playingPiece; 
	public static bool PieceSelected; 
	public bool firstmove = true; 
	//public static PieceManager instance; 
	public int index; 

	// Use this for initialization
	// void Start () {	
	// 	//instance = this; 
	// 	index = int.Parse(this.name.Substring(2,1));
	// 	Debug.Log("Value:" + index);
	// 	PieceManager.pieceArray[index] = playingPiece; 
	// 	Debug.Log(playingPiece.name);
	// 	PieceSelected = false; 
	// 	// setPieceValue(index);
	// }	

	public void Clicked(){
		int index = int.Parse(this.name.Substring(2,1));
		PieceManager.instance.pieceClicked(index); 
	}

	
	

}
