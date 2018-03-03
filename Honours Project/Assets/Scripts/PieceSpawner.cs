using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PieceSpawner : MonoBehaviour {

	public static GameObject[] pieceArray; 
	public GameObject playingPiece;   
	public bool selected; 

	public static PieceSpawner instance;

	public int index; 
	public bool firstmove = true; 
	public bool swapSelected = false; 
	public string swap; 
	void Start () {
		
		pieceArray = new GameObject[3]; 
		instance = this; 
		generatePieces();
		for (int i = 0; i < pieceArray.Length; i++){
			setPieceValue(i);
		} 
		//firstmove = true; 
		// Instantiate(playingPiece,new Vector3(180,-110,0), Quaternion.identity,instance.transform).SetActive(true);
	}


	public void generatePieces(){
		int xposition = 350;
		for(int i = 0; i < pieceArray.Length; i++){
			pieceArray[i] = Instantiate(playingPiece,new Vector3(xposition,110,0), Quaternion.identity,instance.transform);
			pieceArray[i].transform.name = "P_" + i; 
			pieceArray[i].transform.SetParent(this.transform, true);
			xposition = xposition + 80;
			Debug.Log(pieceArray[i].name + " has been instantiated.");
		}

	}

	public void setPieceValue(int pieceIndex){
		int value = -1; 
		int numindex = 9; 
	//Retrieves a random value from the number bag and adds it to the list. 
		if(NumberBag.numbers != null){
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
		} else {
			Debug.Log("The list is Null. ");
		}
	}

	public int returnIndex(){
		return index; 
	}

	public int returnPieceValue(){
		return(int.Parse(pieceArray[index].GetComponentInChildren<Text>().text));
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
	}
	public void pieceClicked(int val){
		if(!swapSelected){
			if(!selected){
				selected = true; 
				index =  val;
				Debug.Log(pieceArray[index].name + " has been selected.");
			} else if (selected){
				selected = false; 
				Debug.Log(pieceArray[index].name + " has been deselected.");
				// index = -5;
			}
		} else if (swapSelected){
			if (swap.Length != pieceArray.Length){
				if(!swap.Contains(val.ToString())){
					swap = swap + val.ToString();
					pieceArray[val].GetComponent<Image>().color = Color.magenta;
				} else if (swap.Contains(val.ToString())){
					int p = 0; 
					foreach (char i in swap){
						if (i.ToString() == val.ToString())
							{Debug.Log("match found at index: " + p);
							swap.Remove(p,0);}
						p++; 
					}
					pieceArray[val].GetComponent<Image>().color = Color.white;
				}
			}

	//This will just conintinually add things. Will need to check on that.
		} 
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
		} else if (!swapSelected){
			pieceArray[index].GetComponent<Image>().color = Color.white;
			selected = false; 
			// index = pieceArray.Length + 1; 
			swapSelected = true; 
		}
	}


}
