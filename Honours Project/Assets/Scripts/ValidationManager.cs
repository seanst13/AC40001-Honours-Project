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

		total = columnTotal(row, column);

		total = total + PieceSpawner.instance.returnPieceValue();
		Debug.Log("Column Total:  " + total);

		// if (total != PieceSpawner.instance.returnPieceValue()){
			return oddTotalValidation(total);

		// } else{
		// 	return RowValidation(row,column);
		// }

	}
	public static bool RowValidation(int row, int column){
		Debug.Log("Row: " + row); 
		int total = 0;
		// int firstpos = FindFirstHorizontalPosition(row, column);
		// int lastpos = findLastHortizontalPosition(row,column);

		total = RowTotal(row, column); 
		total = total + PieceSpawner.instance.returnPieceValue();
		Debug.Log("Row Total:  " + total);

		if (total != PieceSpawner.instance.returnPieceValue()){
			return oddTotalValidation(total);

		} else{
			return ColumnValidation(row,column);
		}
	}

//Check if the total score for the row is an odd number. 
	static bool oddTotalValidation(int total){
		return total %2 !=0; 
	}


	public static int RowTotal(int row, int column){
		int total = 0; 
		for (int i = column; i >= 0; i--){
			if (BoxSpawner.gridArray[row,i].GetComponentInChildren<Text>().text != ""){
				total = total + int.Parse(BoxSpawner.gridArray[row,i].GetComponentInChildren<Text>().text);
				Debug.Log("Row Total["+i+"]:" + total );
			} else if (BoxSpawner.gridArray[row,i].GetComponentInChildren<Text>().text == "" && i != column){
				break; 
			}
		}

		for(int i = column+1; i <= 4; i++){
			if (BoxSpawner.gridArray[row,i].GetComponentInChildren<Text>().text != ""){
				total = total + int.Parse(BoxSpawner.gridArray[row,i].GetComponentInChildren<Text>().text);
				Debug.Log("Row Total["+i+"]:" + total );
			} else if (BoxSpawner.gridArray[row,i].GetComponentInChildren<Text>().text == "" && i != column){
				break; 
			}
		}
		return total; 
	}

	public static int columnTotal(int row, int column){
		int total = 0; 

		for (int i = row; i >= 0; i--){
			if (BoxSpawner.gridArray[i,column].GetComponentInChildren<Text>().text != ""){
				total = total + int.Parse(BoxSpawner.gridArray[i,column].GetComponentInChildren<Text>().text);
				Debug.Log("Column Total["+i+"]:" + total );
			} else if (BoxSpawner.gridArray[i,column].GetComponentInChildren<Text>().text == "" && i != row){
				break; 
			}
		}

		for(int i = row+1; i <=4; i++){
			if (BoxSpawner.gridArray[i,column].GetComponentInChildren<Text>().text != ""){
				total = total + int.Parse(BoxSpawner.gridArray[i,column].GetComponentInChildren<Text>().text);
				Debug.Log("Column Total["+i+"]:" + total );
			} else if (BoxSpawner.gridArray[i,column].GetComponentInChildren<Text>().text == "" && i != row){
				break; 
			}
		}
		
		return total; 
	}

}
