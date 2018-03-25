﻿using System.Collections;
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
		Debug.Log("Column Total:  " + total);
			return oddTotalValidation(total);

	}
	public static bool RowValidation(int row, int column){
		Debug.Log("Row: " + row);
		int total = 0;
		total = RowTotal(row, column);
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
		if (!BoxSpawner.instance.IsPositionEmpty(row+1,column) || !BoxSpawner.instance.IsPositionEmpty(row,column) ){
				return true;
		} else {
			return false;
		}
	}

	public static bool secondaryRowCheck(int row, int column){
		if (!BoxSpawner.instance.IsPositionEmpty(row,column+1) || !BoxSpawner.instance.IsPositionEmpty(row,column-1) ){
				return true;
		} else {
			return false;
		}
	}
#endregion
#region Total Generation
	public static int RowTotal(int row, int column){
		int total = 0;
		for (int i = column; i >= 0; i--){
			if (!BoxSpawner.instance.IsPositionEmpty(row,i)){
				total = total + int.Parse(BoxSpawner.gridArray[row,i].GetComponentInChildren<Text>().text);
			} else if (BoxSpawner.instance.IsPositionEmpty(row,i) && i != row){
				break;
			}
		}

		for(int i = column+1; i <= 4; i++){
			if (!BoxSpawner.instance.IsPositionEmpty(row,i)){
				total = total + int.Parse(BoxSpawner.gridArray[row,i].GetComponentInChildren<Text>().text);
			} else if (BoxSpawner.instance.IsPositionEmpty(row,i) && i != row){
				break;
			}
		}
		return total;
	}

	public static int columnTotal(int row, int column){
		int total = 0;

		for (int i = row; i >= 0; i--){
			if (!BoxSpawner.instance.IsPositionEmpty(i,column)){
				total = total + int.Parse(BoxSpawner.gridArray[i,column].GetComponentInChildren<Text>().text);
			} else if (BoxSpawner.instance.IsPositionEmpty(i,column) && i != row){
				break;
			}
		}

		for(int i = row+1; i <=4; i++){
			if (!BoxSpawner.instance.IsPositionEmpty(i,column)){
				total = total + int.Parse(BoxSpawner.gridArray[i,column].GetComponentInChildren<Text>().text);
			} else if (BoxSpawner.instance.IsPositionEmpty(i,column) && i != row){
				break;
			}
		}
		return total;
	}
#endregion
}