using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PieceGeneration : MonoBehaviour {
	public GameObject playingPiece; 
	List<int> numberList;  

	// Use this for initialization
	void Start () {	
		numberList = NumberBag.numbers; 
		setValue();
	}	
	void setValue(){
	//Retrieves a random value from the number bag and adds it to the list. 
		if(numberList != null){
			int index = Random.Range(0,numberList.Count);
			int value = (int) numberList[index];
			Debug.Log("The value that has been retrieved is: " + value);
			playingPiece.GetComponentInChildren<Text>().text = value.ToString(); 
			numberList.RemoveAt(index); 
	}
	else {
		Debug.Log("The list is Null. ");
	}
}
	// Update is called once per frame
	void Update () {
		
	}
}
