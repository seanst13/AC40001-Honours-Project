using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PieceGeneration : MonoBehaviour {
	public GameObject playingPiece; 
	List<int> numberList;  

	// Use this for initialization
	void Start () {	
		setPieceValue();
	}	
	void setPieceValue(){
	//Retrieves a random value from the number bag and adds it to the list. 
		if(NumberBag.numbers != null){
			int index = Random.Range(0,NumberBag.numbers.Count);
			int value = (int) NumberBag.numbers[index];
			Debug.Log("The value that has been retrieved is: " + value);
			playingPiece.GetComponentInChildren<Text>().text = value.ToString(); 
			//NumberBag.numbers.RemoveAt(index); 
	}
	else {
		Debug.Log("The list is Null. ");
	}
}
	// Update is called once per frame
	void Update () {
		
	}
}
