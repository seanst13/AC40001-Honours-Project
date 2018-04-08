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
		Manager.AddComponent<EndGame>();

		Manager.GetComponent<StoredPieceManager>().SetUp();

		Manager.GetComponent<NumberBag>().GenerateNumbers();

		Manager.GetComponent<BoxSpawner>().SetUp(5);

		Manager.GetComponent<PieceManager>().setUp();

		Manager.GetComponent<EndGame>().setInstance(); 

		Manager.GetComponent<TurnManagement>().setUp();
		 
		Manager.GetComponent<AI_Player>().SetUp();		  
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
		Manager.GetComponent<BoxSpawner>().setvalueAtPosition(2,1,2);
		Manager.GetComponent<TurnManagement>().incrementTurn();
		Assert.IsTrue(Manager.GetComponent<StoredPieceManager>().piecesAreSwapped());
	}

	[TearDown]
	public void TearDown(){
		Manager.GetComponent<EndGame>().enableScreen();
		GameObject.DestroyImmediate(Manager);
	}
}
