using System.Collections.Generic; 
using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class FilterTests {

	GameObject filters;
	[SetUp]
	public void SetUp(){
		filters = new GameObject();
		filters.AddComponent<Filter>();
		filters.AddComponent<BoxSpawner>();
		filters.AddComponent<ValidationManager>();
		filters.AddComponent<NumberBag>();
		filters.AddComponent<PieceManager>();
		filters.AddComponent<AI_Player>();

		filters.GetComponent<NumberBag>().GenerateNumbers(); 
		filters.GetComponent<PieceManager>().setUp();
		filters.GetComponent<BoxSpawner>().SetUp(5);
		filters.GetComponent<AI_Player>().SetUp();
	}

	[Test]
	public void checkInvalidFilter() {
		//Checks the invalid filter removes items from the list
		filters.GetComponent<AI_Player>().GetPossibleMoves();
		List<Move> moves = filters.GetComponent<AI_Player>().returnPossibleMoves();
		int beforeFilter = moves.Count;
		moves = Filter.removeInValidPlacements(moves);
		int afterFilter = moves.Count;
		Assert.LessOrEqual(afterFilter, beforeFilter);
	}

	[Test]
	public void checkCompleteEvenFilterWorks(){
		//Checks the even filter removes even totaled items in both rows & columns from the list
		filters.GetComponent<AI_Player>().GetPossibleMoves();
		List<Move> moves = filters.GetComponent<AI_Player>().returnPossibleMoves();
		int beforeFilter = moves.Count;
		moves = Filter.removeCompleteEvenTotals(moves);
		int afterFilter = moves.Count;
		Assert.LessOrEqual(afterFilter, beforeFilter);
	}

	[Test]
	public void checkEvenFilterWorks(){
		//Checks the even filter removes items from the list
		filters.GetComponent<AI_Player>().GetPossibleMoves();
		List<Move> moves = filters.GetComponent<AI_Player>().returnPossibleMoves();
		int beforeFilter = moves.Count;
		moves = Filter.removeEvenTotals(moves);
		int afterFilter = moves.Count;
		Assert.LessOrEqual(afterFilter, beforeFilter);
	}

	[Test]
	public void checkSecondaryScoringWorks(){
		filters.GetComponent<AI_Player>().GetPossibleMoves();
		List<int> scorebefore = new List<int>();
		List<int> scoreafter = new List<int>();
		List<Move> moves = filters.GetComponent<AI_Player>().returnPossibleMoves();
		foreach(Move m in moves){
			scorebefore.Add(m.totalScore);
		} 
		moves = Filter.addSecondaryScoring(moves);
		foreach(Move m in moves){
			scoreafter.Add(m.totalScore);
		}

		for(int i = 0; i < moves.Count; i ++){
			Assert.GreaterOrEqual(scoreafter[i], scorebefore[i]);
		}
		
	}

	[Test]
	public void checkInvalidandEven(){
		//Check to see if the invalid and complete even filters can be used in conjunction
		filters.GetComponent<AI_Player>().GetPossibleMoves();
		List<Move> moves = filters.GetComponent<AI_Player>().returnPossibleMoves();
		int beforeFilter = moves.Count;
		moves = Filter.removeInValidPlacements(moves);
		moves = Filter.removeCompleteEvenTotals(moves);
		int afterFilter = moves.Count;
		Assert.LessOrEqual(afterFilter, beforeFilter);
	}

	[Test]
	public void checkInvalidandScore(){
		//Check to see if the invalid and secondary scoring filters can be used in conjunction
		filters.GetComponent<AI_Player>().GetPossibleMoves();
		List<Move> moves = filters.GetComponent<AI_Player>().returnPossibleMoves();
		moves = Filter.removeInValidPlacements(moves);
		if (moves.Count > 0){
			List<int> scorebefore = new List<int>();
			List<int> scoreafter = new List<int>();
			foreach(Move m in moves){
				scorebefore.Add(m.totalScore);
			} 
			moves = Filter.addSecondaryScoring(moves);
			foreach(Move m in moves){
				scoreafter.Add(m.totalScore);
			}

			for(int i = 0; i < moves.Count; i ++){
				Assert.GreaterOrEqual(scoreafter[i], scorebefore[i]);
			}
		} else {
			Assert.IsEmpty(moves); 
		}
	}

	[Test]
	public void checkEvenandScore(){
		//Checks the even and secondary scoring methods can be used in conjunction
		filters.GetComponent<AI_Player>().GetPossibleMoves();
		List<Move> moves = filters.GetComponent<AI_Player>().returnPossibleMoves();
		moves = Filter.removeCompleteEvenTotals(moves);
		if (moves.Count > 0){
			List<int> scorebefore = new List<int>();
			List<int> scoreafter = new List<int>();
			foreach(Move m in moves){
				scorebefore.Add(m.totalScore);
			} 
			moves = Filter.addSecondaryScoring(moves);
			foreach(Move m in moves){
				scoreafter.Add(m.totalScore);
			}

			for(int i = 0; i < moves.Count; i ++){
				Assert.GreaterOrEqual(scoreafter[i], scorebefore[i]);
			}
		} else {
			Assert.IsEmpty(moves); 
		}
	}

	[Test]
	public void checkInvalidEvenandScore(){
		//Checks if the invalid, even and secondary scoring methods can be used in conjunction 
		filters.GetComponent<AI_Player>().GetPossibleMoves();
		List<Move> moves = filters.GetComponent<AI_Player>().returnPossibleMoves();
		moves = Filter.removeInValidPlacements(moves);
		moves = Filter.removeCompleteEvenTotals(moves);
		if (moves.Count > 0){
			List<int> scorebefore = new List<int>();
			List<int> scoreafter = new List<int>();
			foreach(Move m in moves){
				scorebefore.Add(m.totalScore);
			} 
			moves = Filter.addSecondaryScoring(moves);
			foreach(Move m in moves){
				scoreafter.Add(m.totalScore);
			}

			for(int i = 0; i < moves.Count; i ++){
				Assert.GreaterOrEqual(scoreafter[i], scorebefore[i]);
			}
		} else {
			Assert.IsEmpty(moves); 
		}
	}
	
	[TearDown]
	public void TearDown(){
		GameObject.DestroyImmediate(filters); 
	}
}
