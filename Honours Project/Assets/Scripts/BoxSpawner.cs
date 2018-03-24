﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BoxSpawner : MonoBehaviour {
	[Header("Grid Attributes")]
	public int gridSize; 
	bool BoxWhite = true; 
	[Space]
	[Header("Game Objects")]
	public GameObject whiteSquare;
	public GameObject greySquare;
	private int middleSquare {get; set;}
	public static GameObject[,] gridArray {get; set;}

	public static BoxSpawner instance; 

	public void Start(){
		SetUp(gridSize);	 		
	}

	public void DisplayBoard(){
		int xpos = -285;
		int ypos = 185; 
		for (var row = 0; row < this.gridSize; row++){
        	for (var column = 0; column < this.gridSize; column++){
				SpawnBox(row,column,xpos,ypos);
				xpos = xpos +60;
            }
		//Reset Row Back to the Start
			xpos = -285;
			ypos = ypos-60; 
         }
}
// Spawns A White or Grey Box and adds them to the array
public void SpawnBox(int row, int column, int xpos, int ypos){
		if (BoxWhite){
			gridArray[row,column] = Instantiate(whiteSquare, new Vector3(xpos, ypos, 0), Quaternion.identity);
			gridArray[row,column].transform.SetParent(this.transform,false);
			// Middle of Grid Check
			if (column == middleSquare && row == middleSquare)
				{
					gridArray[row,column].GetComponentInChildren<Text>().text = "5";
					gridArray[row,column].GetComponent<Collider2D>().enabled = false; 	
				}
			gridArray[row,column].transform.name = (row + "_" + column).ToString(); 
			BoxWhite = false;			
		} else if (!BoxWhite){
			gridArray[row,column] = Instantiate(greySquare, new Vector3(xpos, ypos, 0), Quaternion.identity);
			gridArray[row,column].transform.SetParent(this.transform,false);
			// Middle of Grid Check
			if (column == middleSquare && row == middleSquare)
				{
					gridArray[row,column].GetComponentInChildren<Text>().text = "5";
					gridArray[row,column].GetComponent<Collider2D>().enabled = false; 	
				}
			gridArray[row,column].transform.name = (row + "_"+ column).ToString();	
			BoxWhite = true;
			
		}

}


public string returnNameAtPosition(int row, int col){
	return gridArray[row,col].transform.name; 
}

public GameObject[,] returnGridArray(){
	return gridArray;
}

public void SetUp(int size){
	gridArray = new GameObject[size,size];
	instance = this; 
	setMiddleValue(size);
	DisplayBoard();
}

public string returnValueAtPosition(int row, int col){
	return gridArray[row,col].GetComponentInChildren<Text>().text;
}

public void setvalueAtPosition(int row,int col, int val){
	gridArray[row,col].GetComponentInChildren<Text>().text = val.ToString();
}

public void setMiddleValue(int size){
	middleSquare = Mathf.RoundToInt(size/2);
}

public int getMiddleValue(){
	return middleSquare;
}
}
