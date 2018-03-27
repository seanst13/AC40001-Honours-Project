 using System.Collections;
 using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class TurnManagementTests {
	GameObject Manager; 
	[SetUp]
	public void SetUp(){
		Manager = new GameObject();
		Manager.AddComponent<PieceManager>();
		Manager.AddComponent<NumberBag>();
		Manager.AddComponent<BoxSpawner>(); 
		Manager.AddComponent<TurnManagement>();
		Manager.AddComponent<ValidationManager>(); 
		Manager.AddComponent<AI_Player>();
		Manager.AddComponent<ScoreManager>(); 

		Manager.GetComponent<NumberBag>().amountToPool = 5; 
		Manager.GetComponent<NumberBag>().GenerateNumbers();
		Manager.GetComponent<PieceManager>().playingPiece = (GameObject) Resources.Load("PlayPiece");
		Manager.GetComponent<PieceManager>().setUp();
		Manager.GetComponent<BoxSpawner>().gridSize = 5;
		Manager.GetComponent<BoxSpawner>().whiteSquare = (GameObject) Resources.Load("WhiteBox");
		Manager.GetComponent<BoxSpawner>().greySquare = (GameObject) Resources.Load("GreyBox");
		Manager.GetComponent<ScoreManager>().score1 = GameObject.Find("PlayerScore"); 
		Manager.GetComponent<ScoreManager>().score2 = GameObject.Find("ComputerScore");
		Manager.GetComponent<ScoreManager>().setup();  
		Manager.GetComponent<BoxSpawner>().SetUp(5);
		Manager.GetComponent<AI_Player>().setup();


		Manager.GetComponent<TurnManagement>().turnText = GameObject.Find("TurnCounter");	
		Manager.GetComponent<TurnManagement>().timerText= GameObject.Find("Timer");	
		Manager.GetComponent<TurnManagement>().setUp();	
	}
	[Test]
	public void CheckIfPlayerNumberUpdates() {
		Assert.AreEqual(1, Manager.GetComponent<TurnManagement>().returnPlayerNumber());
	}

	[Test]
	public void CheckIfTurnCounterIncrements(){
		Manager.GetComponent<TurnManagement>().incrementTurn();
		Assert.GreaterOrEqual(Manager.GetComponent<TurnManagement>().returnTurnCounter(), 1);
	}

	[Test]
	public void CheckIfSkipTurnClearsArrayOnSkip(){
		Manager.GetComponent<PieceManager>().index = 0; 
		Manager.GetComponent<PieceManager>().addPieceToList(0,0);
		Manager.GetComponent<PieceManager>().index = 1; 
		Manager.GetComponent<PieceManager>().addPieceToList(0,1);

		Manager.GetComponent<TurnManagement>().skipTurn(); 

		Assert.IsEmpty(Manager.GetComponent<PieceManager>().returnPlacedPieces());
	} 

	[TearDown]
	public void TearDown(){
		GameObject.DestroyImmediate(Manager);
	}

	
}
