using System.Collections;
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
	public bool firstmove = true; 
	[Space]
	[Header("Multiple Piece Related")]
	public List<Piece> placedPieces; 
	public List<StoredPiece> storedPieces; 
	[Space]
	[Header("Swapping Pieces Related Variables")]
	private bool swapSelected = false; 
	private string swap; 
	  	

#endregion
#region Set Up
	void Start () {
		pieceArray = new GameObject[2]; 
		placedPieces = new List<Piece>();
		storedPieces = new List<StoredPiece>();  
		instance = this; 
		generatePieces();
		for (int i = 0; i < pieceArray.Length; i++){
			setPieceValue(i);
		} 
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
#endregion

	public void swapPreviousPlayersVals(){
		if (storedPieces.Count == 0){
			for(int i = 0; i < pieceArray.Length; i++){
				storedPieces.Add(new StoredPiece{
					pieceValue = pieceArray[i].GetComponentInChildren<Text>().text,
					playerNumber = TurnManagement.playerNumber
				});
				setPieceValue(i); 
			}
		} else if (storedPieces.Count != 0){

			for(int i = 0; i < pieceArray.Length; i++){
				storedPieces.Add(new StoredPiece{
					pieceValue = pieceArray[i].GetComponentInChildren<Text>().text,
					playerNumber = TurnManagement.playerNumber
				});
			}

			if (TurnManagement.playerNumber == 1){
			 string indexes = ""; 
				foreach (StoredPiece piece in storedPieces){
					if (piece.playerNumber == 2){
						indexes += storedPieces.IndexOf(piece).ToString();
					}
				}
				if (indexes ==""){
					for(int i = 0; i < pieceArray.Length; i++){
						setPieceValue(i);
					}
				} else if (indexes != ""){
					foreach (char i in indexes){
						int val = int.Parse(i.ToString());
						Debug.Log("Stored Value at ["+val+"]: " + storedPieces[val].pieceValue);
						Debug.Log("Text Value at: ["+val+"]"+ pieceArray[val].GetComponentInChildren<Text>().text);
						pieceArray[val].GetComponentInChildren<Text>().text = storedPieces[val].pieceValue;
					}
					for(int i = indexes.Length-1; i >= 0; i--){
						storedPieces.RemoveAt(int.Parse(indexes[i].ToString()));
					}
				}
				
			} else if (TurnManagement.playerNumber == 2) {
			 string indexes = ""; 
				foreach (StoredPiece piece in storedPieces){
					if (piece.playerNumber == 1){
						indexes += storedPieces.IndexOf(piece).ToString();
					}
				}
				if (indexes ==""){
					for(int i = 0; i < pieceArray.Length; i++){
						setPieceValue(i);
					}
				} else if (indexes != ""){
					Debug.Log(indexes);
					foreach (char i in indexes){
						int val = int.Parse(i.ToString());
						Debug.Log("Stored Value at ["+val+"]: " + storedPieces[val].pieceValue);
						Debug.Log("Text Value at: ["+val+"]"+ pieceArray[val].GetComponentInChildren<Text>().text);
						pieceArray[val].GetComponentInChildren<Text>().text = storedPieces[val].pieceValue.ToString();	
					}
					for(int i = indexes.Length-1; i >= 0; i--){
						storedPieces.RemoveAt(int.Parse(indexes[i].ToString()));
					}
				}


			}
		}
	}


#region Return Methods
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
#endregion

	public void pieceClicked(int val){
		if(!swapSelected){
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
							swap = swap.Remove(p,1);}
						p++; 
					}
					pieceArray[val].GetComponent<Image>().color = Color.white;
				}
			}else if (swap.Length == pieceArray.Length){
				if (swap.Contains(val.ToString())){
					int p = 0; 
					foreach (char i in swap){
						if (i.ToString() == val.ToString())
							{Debug.Log("match found at index: " + p);
							swap = swap.Remove(p,1);}
						p++; 
					}
					pieceArray[val].GetComponent<Image>().color = Color.white;
				}
			}
		} 
	}
#region Piece Swapping Related
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
}
