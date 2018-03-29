using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class AITests {
	GameObject Manager; 

	[SetUp]
	public void SetUp(){
		Manager = new GameObject ();
		Manager.AddComponent<AI_Player>();
		Manager.AddComponent<BoxSpawner>();
		Manager.AddComponent<NumberBag>();
		Manager.AddComponent<PieceManager>(); 
		Manager.AddComponent<ValidationManager>();

		
		Manager.GetComponent<NumberBag>().amountToPool = 5;
		Manager.GetComponent<NumberBag>().GenerateNumbers();
		Manager.GetComponent<PieceManager>().setUp(); 
		Manager.GetComponent<BoxSpawner>().SetUp(5);
		Manager.GetComponent<AI_Player>().SetUp();

	}

	[Test]
	public void checkListExists(){
		Assert.IsEmpty(Manager.GetComponent<AI_Player>().returnPossibleMoves());
	}

	[Test]
	public void checkPossibleMovesAreRetrieved(){
		Manager.GetComponent<AI_Player>().GetPossibleMoves();
		Assert.Greater(Manager.GetComponent<AI_Player>().returnPossibleMoves().Count,0);
	}

	[Test]
	public void checkFilteringWorks(){
		Manager.GetComponent<AI_Player>().GetPossibleMoves();
		int beforeFilter = Manager.GetComponent<AI_Player>().returnPossibleMoves().Count;
		Manager.GetComponent<AI_Player>().filterAndSortMoves();
		int afterFilter =  Manager.GetComponent<AI_Player>().returnPossibleMoves().Count;
		Assert.Less(afterFilter,beforeFilter);
	}

	
	[Test]
	public void EditorTest() {
		//Arrange
		var gameObject = new GameObject();

		//Act
		//Try to rename the GameObject
		var newGameObjectName = "My game object";
		gameObject.name = newGameObjectName;

		//Assert
		//The object has a new name
		Assert.AreEqual(newGameObjectName, gameObject.name);
	}

	[TearDown]
	public void TearDown(){
		GameObject.DestroyImmediate(Manager); 
	}
}
