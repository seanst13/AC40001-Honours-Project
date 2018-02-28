using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class ScoreManager : MonoBehaviour {
	public static ScoreManager instance;
	public GameObject Scoreboard;

	// Use this for initialization
	void Start () {
		instance = this;
		setPlayerScore(0);
	}
	
	void setPlayerScore(int value){
		Scoreboard.GetComponent<Text>().text = value.ToString();
	}

	


	// Update is called once per frame
	void Update () {
		
	}

}
