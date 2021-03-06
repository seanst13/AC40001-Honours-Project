﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberBag : MonoBehaviour {
	public static List<int> numbers = new List<int>();
	private int number = 1;
	private int amountToPool = 5;

	// Use this for initialization
	void Start () {
		GenerateNumbers();
	}

// Declare all the numbers that will be used them in the game for the playing pieces and add them to a list.
	public void GenerateNumbers(){
		for (int i = 0; i < amountToPool; i++) {
			for (int j = 0; j < amountToPool; j++){
				numbers.Add(number);
			}
			number++; 
		}
	}
}