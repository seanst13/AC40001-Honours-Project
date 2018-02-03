using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PieceGeneration : MonoBehaviour {
	public GameObject playingPiece; 
	List<int> numberList;  

	// Use this for initialization
	void Start () {	
		numberList = NumberBag.instance.returnList(); 	
		setValue();
	}
	
	void setValue(){
		if(numberList == null){
			int value = (int) numberList[Random.Range(0,NumberBag.numbers.Count)];
			Debug.Log("THIS IS VALUE:" + value);
			playingPiece.GetComponentInChildren<Text>().text = value.ToString(); 
	}
	else {
		Debug.Log("NUMBERS IS NULL. WAT DO");
	}
}
	// Update is called once per frame
	void Update () {
		
	}
}
