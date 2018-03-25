using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValidationManager : MonoBehaviour {

#region Positioning Validation
public static bool PositioningValidation(int row, int column){
			if( !BoxSpawner.instance.IsPositionEmpty(row,column-1) || !BoxSpawner.instance.IsPositionEmpty(row-1,column) ||
				!BoxSpawner.instance.IsPositionEmpty(row,column+1) || !BoxSpawner.instance.IsPositionEmpty(row+1,column)){
					return true; 
				} else {
					return false; 
				}
	}
#endregion
#region Row & Column Validation
public static bool ColumnValidation(int row, int column){
		Debug.Log("Column: " + column);
		int total = 0;

		total = columnTotal(row, column);

		//total = total + PieceManager.instance.returnPieceValue();
		Debug.Log("Column Total:  " + total);

		// if (total != PieceManager.instance.returnPieceValue()){
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
		//total = total + PieceManager.instance.returnPieceValue();
		Debug.Log("Row Total:  " + total);

		if (total != PieceManager.instance.returnPieceValue()){
			return oddTotalValidation(total);

		} else{
			return ColumnValidation(row,column);
		}
	}

//Check if the total score for the row is an odd number. 
	static bool oddTotalValidation(int total){
		return total %2 !=0; 
	}
#endregion
#region Secondary Positioning Checks

	public static bool secondaryColumnCheck(int row, int column){

	if( row-1 >= BoxSpawner.gridArray.GetLowerBound(0) && row+1 <= BoxSpawner.gridArray.GetUpperBound(0)){
		if (BoxSpawner.gridArray[row+1,column].GetComponentInChildren<Text>().text !=""
			|| BoxSpawner.gridArray[row-1,column].GetComponentInChildren<Text>().text !="" ){
				return true; 
			}
	} else if(row+1 > BoxSpawner.gridArray.GetUpperBound(0)){
		if (BoxSpawner.gridArray[row-1,column].GetComponentInChildren<Text>().text !="" ){
				return true; 
			}
	} else if(row-1 < BoxSpawner.gridArray.GetLowerBound(0)){
		if (BoxSpawner.gridArray[row+1,column].GetComponentInChildren<Text>().text !=""){
				return true; 
			}
	} else { return false; }
	return false; 
	}

	public static bool secondaryRowCheck(int row, int column){

	if( column-1 >= BoxSpawner.gridArray.GetLowerBound(1) && column+1 <= BoxSpawner.gridArray.GetUpperBound(1)){
		if (BoxSpawner.gridArray[row,column-1].GetComponentInChildren<Text>().text !=""
			|| BoxSpawner.gridArray[row,column+1].GetComponentInChildren<Text>().text !="" ){
				return true; 
			}
	} else if(column+1 > BoxSpawner.gridArray.GetUpperBound(1)){
		if (BoxSpawner.gridArray[row,column-1].GetComponentInChildren<Text>().text !="" ){
				return true; 
			}
	} else if(column-1 < BoxSpawner.gridArray.GetLowerBound(1)){
		if (BoxSpawner.gridArray[row,column+1].GetComponentInChildren<Text>().text !=""){
				return true; 
			}
	} else { return false; }
	return false; 
	}
#endregion
#region Total Generation
	public static int RowTotal(int row, int column){
		int total = 0; 
		for (int i = column; i >= 0; i--){
			if (BoxSpawner.gridArray[row,i].GetComponentInChildren<Text>().text != ""){
				total = total + int.Parse(BoxSpawner.gridArray[row,i].GetComponentInChildren<Text>().text);
				// Debug.Log("Row Total["+i+","+column+"]:" + total );
			} else if (BoxSpawner.gridArray[row,i].GetComponentInChildren<Text>().text == "" && i != column){
				break; 
			}
		}

		for(int i = column+1; i <= 4; i++){
			if (BoxSpawner.gridArray[row,i].GetComponentInChildren<Text>().text != ""){
				total = total + int.Parse(BoxSpawner.gridArray[row,i].GetComponentInChildren<Text>().text);
				// Debug.Log("Row Total["+i+","+column+"]:" + total );
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
				// Debug.Log("Column Total["+row+","+i+"]:" + total );
			} else if (BoxSpawner.gridArray[i,column].GetComponentInChildren<Text>().text == "" && i != row){
				break; 
			}
		}

		for(int i = row+1; i <=4; i++){
			if (BoxSpawner.gridArray[i,column].GetComponentInChildren<Text>().text != ""){
				total = total + int.Parse(BoxSpawner.gridArray[i,column].GetComponentInChildren<Text>().text);
				// Debug.Log("Column Total["+row+","+i+"]:" + total );
			} else if (BoxSpawner.gridArray[i,column].GetComponentInChildren<Text>().text == "" && i != row){
				break; 
			}
		}
		
		return total; 
	}
#endregion
}
