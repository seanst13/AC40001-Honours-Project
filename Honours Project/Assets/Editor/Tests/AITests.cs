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
	public void CheckValidFilterWorks(){
		Manager.GetComponent<AI_Player>().GetPossibleMoves();
		int beforeFilter = Manager.GetComponent<AI_Player>().returnPossibleMoves().Count;
		Manager.GetComponent<AI_Player>().removeInValidPlacements();
		int afterFilter =  Manager.GetComponent<AI_Player>().returnPossibleMoves().Count;
		Assert.Less(afterFilter,beforeFilter);
	}

	[Test]
	public void CheckEvenFilterWorks(){
		Manager.GetComponent<AI_Player>().GetPossibleMoves();
		int beforeFilter = Manager.GetComponent<AI_Player>().returnPossibleMoves().Count;
		Manager.GetComponent<AI_Player>().removeEvenTotals();
		int afterFilter =  Manager.GetComponent<AI_Player>().returnPossibleMoves().Count;
		Assert.Less(afterFilter,beforeFilter);
	}

	[Test]
	public void CheckIfSortingWorks(){
		Manager.GetComponent<AI_Player>().GetPossibleMoves();
		Manager.GetComponent<AI_Player>().filterAndSortMoves();
		
		if (Manager.GetComponent<AI_Player>().returnPossibleMoves().Count >0){
			int min = Manager.GetComponent<AI_Player>().returnScoreAtPosition(0);
			int max = Manager.GetComponent<AI_Player>().returnScoreAtPosition(Manager.GetComponent<AI_Player>().returnPossibleMoves().Count-1);
			Assert.LessOrEqual(min,max);
		} else if (Manager.GetComponent<AI_Player>().returnPossibleMoves().Count == 0){
			Assert.IsEmpty(Manager.GetComponent<AI_Player>().returnPossibleMoves());
		}		
	}

	[TearDown]
	public void TearDown(){
		GameObject.DestroyImmediate(Manager); 
	}
}
