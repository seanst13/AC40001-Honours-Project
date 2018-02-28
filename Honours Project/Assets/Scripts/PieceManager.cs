using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PieceManager : MonoBehaviour {
	public GameObject playingPiece; 
	List<int> numberList;  
	public bool PieceSelected; 
	public bool firstmove = true; 
	public static PieceManager instance; 

	// Use this for initialization
	void Start () {	
		instance = this; 
		PieceSelected= false; 
		setPieceValue();
	}	

	void spawnPieces(){
		GameObject clone = Instantiate(playingPiece,new Vector3(110,-110,0), Quaternion.identity,instance.transform);
		playingPiece = Instantiate(playingPiece,new Vector3(180,-110,0), Quaternion.identity,instance.transform);
	}

	public void setPieceValue(){
		int value = -1; 
		int index; 
	//Retrieves a random value from the number bag and adds it to the list. 
		if(NumberBag.numbers != null){
			if (!firstmove){
				index = Random.Range(0,NumberBag.numbers.Count);
				value = (int) NumberBag.numbers[index];
				Debug.Log("The value that has been retrieved is: " + value);
				playingPiece.GetComponentInChildren<Text>().text = value.ToString(); 
				NumberBag.numbers.RemoveAt(index); 
			} else if(firstmove) {
				index = 7; 
				while (value %2 != 0){
					index = Random.Range(0,NumberBag.numbers.Count);
					value = (int) NumberBag.numbers[index];
					Debug.Log("The value that has been retrieved during the first move is: " + value);
				}
				playingPiece.GetComponentInChildren<Text>().text = value.ToString(); 
				NumberBag.numbers.RemoveAt(index); 
			}
	}
	else {
		Debug.Log("The list is Null. ");
	}
}
	// Update is called once per frame
	void Update () {
		if(PieceSelected){
			playingPiece.GetComponent<Image>().color = Color.yellow;
		} else {
			playingPiece.GetComponent<Image>().color = Color.white;
		}
	}
	public void pieceClicked(){
		PieceSelected = true; 
	}
	

}
