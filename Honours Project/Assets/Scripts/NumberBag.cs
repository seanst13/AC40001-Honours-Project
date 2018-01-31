using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberBag : MonoBehaviour {
	public List<int> numbers;
	private int number = 1;
	public int amountToPool = 5;

	// Use this for initialization
	void Start () {
// Declare all the numbers that will be used them in the game for the playing pieces and add them to a list. 
		numbers = new List<int>();
		for (int i = 0; i < amountToPool; i++) {
			for (int j = 0; j < amountToPool; j++){
				numbers.Add(number);
			}
			number++; 
		}

		foreach (int num in numbers){
			Debug.Log(num);
		}	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
