using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceSpawner : MonoBehaviour {

	public static GameObject[] pieceArray; 
	public GameObject playingPiece;   
	public bool selected; 

	public static PieceSpawner instance;
	void Start () {
		
		pieceArray = new GameObject[2]; 
		instance = this; 
		generatePieces(); 
		// Instantiate(playingPiece,new Vector3(180,-110,0), Quaternion.identity,instance.transform).SetActive(true);
	}

	public void generatePieces(){
		int xposition = 350;
		for(int i = 0; i < pieceArray.Length; i++){
			pieceArray[i] = Instantiate(playingPiece,new Vector3(xposition,110,0), Quaternion.identity,instance.transform);
			pieceArray[i].transform.name = "P_" + i; 
			pieceArray[i].transform.SetParent(this.transform, true);
			xposition = xposition + 80;
			Debug.Log(pieceArray[i].name + " has been instantiated.");
		}

	}

	
	// Update is called once per frame
	void Update () {
		
	}
}
