using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserSettings : MonoBehaviour {

public static string playerName;

void setPlayerName(string name){
	playerName = name; 
}
public string returnPlayerName(){
	return playerName; 
	}
}
