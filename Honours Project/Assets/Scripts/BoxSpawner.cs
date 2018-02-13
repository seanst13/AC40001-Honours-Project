﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BoxSpawner : MonoBehaviour {
	public int gridSize; 
	bool BoxWhite = true; 
	public GameObject whiteSquare;
	public GameObject greySquare;
	private int middleSquare;
	public static GameObject[,] gridArray;

	public static BoxSpawner instance; 

	void Start(){
		instance = this; 
	//Set up Array and Middle Square Values
		gridArray = new GameObject[gridSize,gridSize];
		middleSquare = Mathf.RoundToInt(gridSize/2);
		DisplayBoard(); 
		
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

}