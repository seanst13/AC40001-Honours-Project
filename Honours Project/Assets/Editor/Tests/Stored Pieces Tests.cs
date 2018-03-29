using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class StoredPiecesTests {
	GameObject Manager;
	[SetUp]
	public void SetUp(){
		Manager = new GameObject(); 
		Manager.AddComponent<PieceManager>();
		Manager.AddComponent<StoredPieceManager>(); 
		Manager.AddComponent<PlacedPieceManager>(); 
		Manager.AddComponent<NumberBag>();
		Manager.AddComponent<BoxSpawner>(); 
		Manager.AddComponent<TurnManagement>();
		Manager.AddComponent<ValidationManager>(); 
		Manager.AddComponent<AI_Player>();
		Manager.AddComponent<ScoreManager>(); 


		Manager.GetComponent<StoredPieceManager>().SetUp();
		Manager.GetComponent<NumberBag>().amountToPool = 5;
		Manager.GetComponent<NumberBag>().GenerateNumbers();
		Manager.GetComponent<BoxSpawner>().SetUp(5);
		Manager.GetComponent<PieceManager>().playingPiece = (GameObject) Resources.Load("PlayPiece");
		Manager.GetComponent<PieceManager>().setUp(); 
		Manager.GetComponent<TurnManagement>().setUp(); 
		Manager.GetComponent<AI_Player>().setup();  
		  
	}
	[Test]
	public void CheckListExists() {
		Assert.IsEmpty(Manager.GetComponent<StoredPieceManager>().returnStoredPieces());
	}

	[Test]
	public void checkListCanBeAdded(){
		Manager.GetComponent<StoredPieceManager>().addToStoredPieces(); 
		Assert.IsNotEmpty(Manager.GetComponent<StoredPieceManager>().returnStoredPieces());
	}

	[Test]
	public void piecesAreSwapped(){
		Debug.Log("THIS IS WHERE THE UNIT TEST IS FAILING");
		Manager.GetComponent<BoxSpawner>().setvalueAtPosition(2,1,2);
		Manager.GetComponent<TurnManagement>().incrementTurn();
		Assert.IsTrue(Manager.GetComponent<StoredPieceManager>().piecesAreSwapped());
	}

	[TearDown]
	public void TearDown(){
		GameObject.DestroyImmediate(Manager);
	}
}
