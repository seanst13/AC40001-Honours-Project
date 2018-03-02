using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValidationManager : MonoBehaviour {

public static bool PositioningValidation(int row, int column){
		//If the piece lies in the rows; 
		if ((row > 0 && row < 4)  && (column >=0 && column <=4)){
			if (column+1 >4){
				if(		BoxSpawner.gridArray[row, column-1].GetComponentInChildren<Text>().text != "" ||
						BoxSpawner.gridArray[row+1, column].GetComponentInChildren<Text>().text != "" ||
						BoxSpawner.gridArray[row-1, column].GetComponentInChildren<Text>().text != ""){
						return true; 
					}
				else { return false; }
			} else if (column -1 < 0) {
				if(		BoxSpawner.gridArray[row, column+1].GetComponentInChildren<Text>().text != "" ||
						BoxSpawner.gridArray[row+1, column].GetComponentInChildren<Text>().text != "" ||
						BoxSpawner.gridArray[row-1, column].GetComponentInChildren<Text>().text != ""){
						return true; 
					}
				else { return false; }
			} else {
				if( BoxSpawner.gridArray[row, column+1].GetComponentInChildren<Text>().text != "" ||
					BoxSpawner.gridArray[row, column-1].GetComponentInChildren<Text>().text != "" ||
					BoxSpawner.gridArray[row+1, column].GetComponentInChildren<Text>().text != "" ||
					BoxSpawner.gridArray[row-1, column].GetComponentInChildren<Text>().text != ""){
						return true; 
					}
				else {
					return false; 
				}
			}
		//If the piece lies in the top row
		} else if (row == 0 && (column >= 0 && column <=4)){
			if (column+1 > 4){
				if( BoxSpawner.gridArray[row, column-1].GetComponentInChildren<Text>().text != "" ||
					BoxSpawner.gridArray[row+1, column].GetComponentInChildren<Text>().text != ""){
						return true; 
					}
				else {
					return false; 
				}	
			} else if (column-1 < 0 ){
					if( BoxSpawner.gridArray[row, column+1].GetComponentInChildren<Text>().text != "" ||
						BoxSpawner.gridArray[row+1, column].GetComponentInChildren<Text>().text != ""){
							return true; 
					}
				else {
					return false; 
				}
			} else {
					if( BoxSpawner.gridArray[row, column+1].GetComponentInChildren<Text>().text != "" ||
						BoxSpawner.gridArray[row, column-1].GetComponentInChildren<Text>().text != "" ||
						BoxSpawner.gridArray[row+1, column].GetComponentInChildren<Text>().text != ""){
							return true; 
					}
				else {
					return false; 
				}
			}
		} else if (row == 4 && (column >= 0 && column <= 4))
		{
			if (column+1 > 4){
				if( BoxSpawner.gridArray[row, column-1].GetComponentInChildren<Text>().text != "" ||
					BoxSpawner.gridArray[row-1, column].GetComponentInChildren<Text>().text != ""){
						return true; 
					}
				else {
					return false; 
				}	
			} else if (column-1 < 0 ){
					if( BoxSpawner.gridArray[row, column+1].GetComponentInChildren<Text>().text != "" ||
						BoxSpawner.gridArray[row-1, column].GetComponentInChildren<Text>().text != ""){
							return true; 
					}
				else {
					return false; 
				}
			} else {
					if( BoxSpawner.gridArray[row, column+1].GetComponentInChildren<Text>().text != "" ||
						BoxSpawner.gridArray[row, column-1].GetComponentInChildren<Text>().text != "" ||
						BoxSpawner.gridArray[row-1, column].GetComponentInChildren<Text>().text != ""){
							return true; 
					}
				else {
					return false; 
				}
			}
		} else {
			return false; 
		}
	}
public static bool ColumnValidation(int row, int column){
		Debug.Log("Column: " + column);
		int total = 0;
		int firstpos = FindFirstVerticalPosition(row, column);
		int lastpos = findLastVerticalPosition(row,column);
		if (firstpos < lastpos){
				for(int i=firstpos;i<lastpos;i++){
					string txt = BoxSpawner.gridArray[i,column].GetComponentInChildren<Text>().text;
					int value = 0;
					if (txt != ""){
						Debug.Log("String from Grid:" + txt);
						value = int.Parse(txt);
					} else if (txt == ""){
						value = 0; 
					}
					total = total + value;
				}
				total = total + PieceSpawner.instance.returnPieceValue();
				Debug.Log("Column Total: " + total);

				return oddTotalValidation(total);
			}
			else if (firstpos == lastpos){
				Debug.Log("FIRSTPOS == LASTPOS FOR COLUMNS");
				total = total + PieceSpawner.instance.returnPieceValue();
				return RowValidation(row, column); 
			}
		
		return oddTotalValidation(total);

	}
	public static bool RowValidation(int row, int column){
		Debug.Log("Row: " + row); 
		int total = 0;
		int firstpos = FindFirstHorizontalPosition(row, column);
		int lastpos = findLastHortizontalPosition(row,column);

		if (firstpos < lastpos){
				for(int i=firstpos;i<lastpos;i++){
					string txt = BoxSpawner.gridArray[row,i].GetComponentInChildren<Text>().text;
					int value = 0;
					if (txt != ""){
						Debug.Log("String from Grid:" + txt);
						value = int.Parse(txt);
					} else if (txt == ""){
						value = 0; 
					}
					total = total + value;
				}
				total = total + PieceSpawner.instance.returnPieceValue();
				Debug.Log("Row Total: " + total);

				return oddTotalValidation(total);
			}
			else if (firstpos == lastpos){
				Debug.Log("FIRSTPOS == LASTPOS FOR ROWS");
				total = total + PieceSpawner.instance.returnPieceValue();
				return ColumnValidation(row, column); 
			}
		
		return oddTotalValidation(total);
	}

//Check if the total score for the row is an odd number. 
	static bool oddTotalValidation(int total){
		return total %2 !=0; 
	}

	static int FindFirstHorizontalPosition(int row, int column){
		int first = column;

			for (int i = column; i > -1; i--){
				if (BoxSpawner.gridArray[row,i].GetComponentInChildren<Text>().text != ""){
					if (first > i){
						first = i;
					}
				} else if (BoxSpawner.gridArray[row,i].GetComponentInChildren<Text>().text != "") {
						return first; 
				}
			} 

		return first; 
	}

	static int findLastHortizontalPosition(int row, int column){
			int last = column;
			for (int i = column; i < 5; i++){
				if (BoxSpawner.gridArray[row,i].GetComponentInChildren<Text>().text != ""){
					if (last < i){
						last = i;
					}
				} else if (BoxSpawner.gridArray[row,i].GetComponentInChildren<Text>().text != "") {
						return last; 
				}
			} 

		return last;
	}

	static int FindFirstVerticalPosition(int row, int column){
	int first = row;

			for (int i = row; i > -1; i--){
				if (BoxSpawner.gridArray[i,column].GetComponentInChildren<Text>().text != ""){
					if (first > i){
						first = i;
					}
				}else if (BoxSpawner.gridArray[row,i].GetComponentInChildren<Text>().text != "") {
						return first; }

			} 

		return first; 

	}

	static int findLastVerticalPosition(int row, int column){
			int last = row;
			for (int i = row; i < 5; i++){
				if (BoxSpawner.gridArray[i,column].GetComponentInChildren<Text>().text != ""){
					if (last < i){
						last = i;
					}
				} else if (BoxSpawner.gridArray[i,column].GetComponentInChildren<Text>().text != "") {
						return last; 
				}
			} 

		return last;
	}
}
