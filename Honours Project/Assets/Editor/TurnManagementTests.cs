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
		Manager.GetComponent<NumberBag>().amountToPool = 5; 
		Manager.GetComponent<NumberBag>().GenerateNumbers();
		Manager.GetComponent<PieceManager>().playingPiece = (GameObject) Resources.Load("PlayPiece");
		Manager.GetComponent<PieceManager>().setUp();
		Manager.GetComponent<BoxSpawner>().gridSize = 5;
		Manager.GetComponent<BoxSpawner>().whiteSquare = (GameObject) Resources.Load("WhiteBox");
		Manager.GetComponent<BoxSpawner>().greySquare = (GameObject) Resources.Load("GreyBox");
		Manager.GetComponent<BoxSpawner>().SetUp(5);

		Manager.GetComponent<TurnManagement>().turnText = GameObject.Find("TurnCounter");	
		Manager.GetComponent<TurnManagement>().timerText= GameObject.Find("Timer");	
		Manager.GetComponent<TurnManagement>().setUp();	
	}
	[Test]
	public void ChceckIfPlayerNumberUpdates() {
		Assert.AreEqual(1, Manager.GetComponent<TurnManagement>().returnPlayerNumber());

	}
}
