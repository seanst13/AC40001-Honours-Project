using System.Collections.Generic; 
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
		Manager.AddComponent<Filter>(); 

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
		int count = Manager.GetComponent<AI_Player>().returnPossibleMoves().Count; 
		Assert.Greater(count,0);
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
		Filter.removeInValidPlacements(Manager.GetComponent<AI_Player>().returnPossibleMoves());
		int afterFilter =  Manager.GetComponent<AI_Player>().returnPossibleMoves().Count;
		Assert.Less(afterFilter,beforeFilter);
	}

	[Test]
	public void CheckEvenFilterWorks(){
		Manager.GetComponent<AI_Player>().GetPossibleMoves();
		int beforeFilter = Manager.GetComponent<AI_Player>().returnPossibleMoves().Count;
		Filter.removeCompleteEvenTotals(Manager.GetComponent<AI_Player>().returnPossibleMoves());
		int afterFilter =  Manager.GetComponent<AI_Player>().returnPossibleMoves().Count;
		Assert.LessOrEqual(afterFilter,beforeFilter);
	}

	[Test]
	public void checkSecondaryScoringWorks(){
		Manager.GetComponent<AI_Player>().GetPossibleMoves();
		Filter.removeInValidPlacements(Manager.GetComponent<AI_Player>().returnPossibleMoves());
		Filter.removeCompleteEvenTotals(Manager.GetComponent<AI_Player>().returnPossibleMoves());

		if (Manager.GetComponent<AI_Player>().returnPossibleMoves().Count >0){
			int valbefore = Manager.GetComponent<AI_Player>().returnScoreAtPosition(0);
			Filter.addSecondaryScoring(Manager.GetComponent<AI_Player>().returnPossibleMoves()); 
			int valafter = Manager.GetComponent<AI_Player>().returnScoreAtPosition(0);
			Assert.GreaterOrEqual(valafter,valbefore);
		} else if (Manager.GetComponent<AI_Player>().returnPossibleMoves().Count == 0){
			Assert.IsEmpty(Manager.GetComponent<AI_Player>().returnPossibleMoves());
		}		
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

	[Test]
	public void CheckSecondaryMovesCanBeRetrieved(){
		Manager.GetComponent<AI_Player>().GetPossibleMoves();
		Manager.GetComponent<AI_Player>().getSecondPlacements();
		foreach(Move m in Manager.GetComponent<AI_Player>().returnPossibleMoves()){
			if (m.returnSizeOfSecondaryMoves() > 0){
				Assert.IsNotEmpty(m.returnSecondaryMoves());
			} else{
				Assert.IsEmpty(m.returnSecondaryMoves());
			}
		}
		
	}

	[TearDown]
	public void TearDown(){
		GameObject.DestroyImmediate(Manager); 
	}
}
