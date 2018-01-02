using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
	public int boardRowCount;
	public int boardColumnCount;
	bool BoxWhite = true; 
	public GameObject whiteSquare;
	public GameObject greySquare;

	void Start(){
		DisplayBoard(); 
	}

	public void returnToMenu(){
		SceneManager.LoadScene("Title Screen");
	}

	public void DisplayBoard(){
		int xpos = -285;
		int ypos = 185; 
		   for (var row = 0; row < this.boardRowCount; row++)
         {
             for (var column = 0; column < this.boardColumnCount; column++)
             {
				 if (BoxWhite == true){
                 	Instantiate(whiteSquare, new Vector3(xpos, ypos, 0), Quaternion.identity).SetActive(true);
					BoxWhite = false;
				 } else if (BoxWhite == false){
                 	Instantiate(greySquare, new Vector3(xpos, ypos, 0), Quaternion.identity).SetActive(true);
					BoxWhite = true;
             	}
				 xpos = xpos -50;
			 }
			 xpos = -285;
			 ypos = ypos-50; 
         }
	}

}
