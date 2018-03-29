﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PieceManager : MonoBehaviour {
#region Variables
	[Header("Piece Array Related")]			
	public static GameObject[] pieceArray; 
	public GameObject playingPiece;   		
	public int index; 
	public static PieceManager instance;
	[Header("Boolean Values")]
	public bool selected; 
	private bool firstmove = true; 
	[Space]
	[Header("Swapping Pieces Related Variables")]
	private bool swapSelected = false; 
	private string swap; 
#endregion
#region Set Up
	void Start () {
		setUp();
	}

	public void setUp(){
		playingPiece =(GameObject) Resources.Load("PlayPiece");
		pieceArray = new GameObject[2]; 
		instance = this; 
		generatePieces();
		swap = ""; 
	}

	public void generatePieces(){
		int xposition = 450;
		for(int i = 0; i < pieceArray.Length; i++){
			pieceArray[i] = Instantiate(playingPiece,new Vector3(xposition,110,0), Quaternion.identity,instance.transform);
			pieceArray[i].transform.name = "P_" + i; 
			pieceArray[i].transform.SetParent(this.transform, true);
			xposition = xposition + 80;
			Debug.Log(pieceArray[i].name + " has been instantiated.");
			setPieceValue(i);
		}

	}

	public void setPieceValue(int pieceIndex){
		int value = -1; 
		int numindex = 9; 
	//Retrieves a random value from the number bag and adds it to the list. 
		if(NumberBag.numbers.Count !=0){
			if (!firstmove){
				numindex = Random.Range(0,NumberBag.numbers.Count-1);
				value = (int) NumberBag.numbers[numindex];
				Debug.Log("The value that has been retrieved is: " + value);
				Debug.Log("THIS IS THE PIECE INDEX: " + pieceIndex);
				pieceArray[pieceIndex].GetComponentInChildren<Text>().text = value.ToString(); 
				NumberBag.numbers.RemoveAt(numindex); 
			} else if(firstmove) {
				Debug.Log("FIRST MOVE ASSIGNMENT"); 
				while (value %2 != 0){
					numindex = Random.Range(1,NumberBag.numbers.Count-1);
					Debug.Log("NUM INDEX " + numindex);
					Debug.Log("VALUE " + value);
					value = (int) NumberBag.numbers[numindex];
					Debug.Log("VALUE " + value);
					Debug.Log("The value that has been retrieved during the first move is: " + value);
				}
				pieceArray[pieceIndex].GetComponentInChildren<Text>().text = value.ToString(); 
				NumberBag.numbers.RemoveAt(numindex); 
				firstmove = false;
			}
		} else if (NumberBag.numbers.Count == 0){
			pieceArray[pieceIndex].SetActive(false);
			Debug.Log("The list is Null. ");
		}
	}
#endregion
#region Return Methods
	public int returnIndex(){
		return index; 
	}

	public int returnPieceValue(){
		return(int.Parse(pieceArray[index].GetComponentInChildren<Text>().text));
	}

	public bool IsElementActive(int index){
		return pieceArray[index].activeInHierarchy;
	}
	// Update is called once per frame
	void Update () {
		if (!swapSelected){
			if(selected){
				pieceArray[index].GetComponent<Image>().color = Color.yellow;
			} else if (!selected) {
				pieceArray[index].GetComponent<Image>().color = Color.white;
			}
		}

		if (NumberBag.numbers.Count == 0){
			int usedpieces = 0; 
			for(int i = 0; i < pieceArray.Length; i++){
				if (!pieceArray[i].activeInHierarchy)
					usedpieces++;

			}

			if (usedpieces == pieceArray.Length-1){
				ErrorManagement.instance.ShowError("YOU HAVE ENDED THE GAME");
			}
		}
	}

	public GameObject[] returnPieceArray(){
		return pieceArray; 
	}
#endregion

	public void pieceClicked(int val){
		if(!swapSelected){
			checkIfSelected(val);
		} else if (swapSelected){
			if (swap.Length != pieceArray.Length){
				if(!swap.Contains(val.ToString())){
					swap = swap + val.ToString();
					pieceArray[val].GetComponent<Image>().color = Color.magenta;
				} else if (swap.Contains(val.ToString())){
					removeElementsFromSwap(val);
				}
			}else if (swap.Length == pieceArray.Length){
				if (swap.Contains(val.ToString())){
					removeElementsFromSwap(val);
				}
			}
		} 
	}

	void checkIfSelected(int val){
		if(!selected){
			selected = true; 
			index =  val;
			Debug.Log(pieceArray[index].name + " has been selected.");
		} else if (selected){
			selected = false; 
			if (index == val){
				pieceArray[index].GetComponent<Image>().color = Color.white;
				Debug.Log(pieceArray[index].name + " has been deselected.");
			} else if (index != val){
				pieceArray[index].GetComponent<Image>().color = Color.white;
				index = val; 
				Debug.Log(pieceArray[index].name + " has been selected.");
				selected = true; 
				}
		}
	}

#region Piece Swapping Related
	 void removeElementsFromSwap(int val)
	{
		int p = 0; 
		foreach (char i in swap){
			if (i.ToString() == val.ToString())
				{Debug.Log("match found at index: " + p);
				swap = swap.Remove(p,1);}
			p++; 
		}
		pieceArray[val].GetComponent<Image>().color = Color.white;
	}

	public void SwapPieces(int index){
		NumberBag.numbers.Add(int.Parse(pieceArray[index].GetComponentInChildren<Text>().text));
		setPieceValue(index);
	}

	public void PerformSwap(){
		if (swapSelected){
			// index = 0; 
			foreach(char i in swap){
			 SwapPieces(int.Parse(i.ToString()));
			 pieceArray[int.Parse(i.ToString())].GetComponent<Image>().color = Color.white;
			}
			swapSelected = false; 
			swap = ""; 
			TurnManagement.instance.skipTurn(); 
		} else if (!swapSelected){
			pieceArray[index].GetComponent<Image>().color = Color.white;
			selected = false; 
			// index = pieceArray.Length + 1; 
			swapSelected = true; 
		}
	}
#endregion

	public void overRideValue(int pos, int val){
		pieceArray[pos].GetComponentInChildren<Text>().text = val.ToString();
	}
}