using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	public int gridSize; 
	bool BoxWhite = true; 
	public GameObject whiteSquare;
	public GameObject greySquare;
	private int middleSquare; 

	void Start(){
		middleSquare = Mathf.RoundToInt(gridSize/2);
		DisplayBoard(); 
		
	}

	public void returnToMenu(){
		SceneManager.LoadScene("Title Screen");
	}

	public void DisplayBoard(){
		int xpos = -285;
		int ypos = 185; 
		for (var row = 0; row < this.gridSize; row++){
        	for (var column = 0; column < this.gridSize; column++){
				SpawnBox(row,column,xpos,ypos);
				xpos = xpos +60;
            }
			xpos = -285;
			ypos = ypos-60; 
         }
}
public void SpawnBox(int row, int column, int xpos, int ypos){
		if (BoxWhite){
			GameObject box = Instantiate(whiteSquare, new Vector3(xpos, ypos, 0), Quaternion.identity);
			box.transform.SetParent(this.transform,false);
			if (column == middleSquare && row == middleSquare)
				box.GetComponentInChildren<Text>().text = "5";
			BoxWhite = false;
		} else if (!BoxWhite){
			GameObject box = Instantiate(greySquare, new Vector3(xpos, ypos, 0), Quaternion.identity);
			box.transform.SetParent(this.transform,false);
			if (column == middleSquare && row == middleSquare)
				box.GetComponentInChildren<Text>().text = "5";
			BoxWhite = true;
		}

}

}
