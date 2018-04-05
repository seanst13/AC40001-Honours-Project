using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class PlacedPieceManager : MonoBehaviour {

	public static PlacedPieceManager instance; 
	private List<Piece> placedPieces; 

	// Use this for initialization
	void Start () {
		SetUp();
	}
	
	public void SetUp(){
		instance = this;
		placedPieces = new List<Piece>(); 
	}

	public void addPieceToList(int row, int col){
		placedPieces.Add(new Piece{
			position = row+"_"+col,
			index = PieceManager.instance.returnIndex()}
			);
		BoxSpawner.instance.setvalueAtPosition(row, col, PieceManager.instance.returnPieceValue());
		PieceManager.pieceArray[PieceManager.instance.returnIndex()].SetActive(false);
		PieceManager.instance.pieceClicked( PieceManager.instance.returnIndex());
	}

	public List<Piece> returnPlacedPieces(){
		return placedPieces; 
	}

	public void ClearPlacedPieces(){
		foreach (Piece p in placedPieces){
			int row = int.Parse(p.position.Substring(0,1));
			int column = int.Parse(p.position.Substring(2,1)); 
			BoxSpawner.gridArray[row,column].GetComponentInChildren<Text>().text = "";
			PieceManager.pieceArray[p.index].SetActive(true);
		}
		placedPieces.Clear(); 
	}

	public void reactivatePiece(int row, int column){
		List<Piece> pieces = new List<Piece>();
		pieces.AddRange(placedPieces);
		foreach(Piece p in pieces){
			if (p.position == BoxSpawner.instance.returnNameAtPosition(row,column)){
				PieceManager.pieceArray[p.index].SetActive(true);
			} else {
				placedPieces.Add(p); 
			}
		}

		placedPieces.Clear();
		placedPieces.AddRange(pieces);
	}	
}