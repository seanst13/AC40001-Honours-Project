using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class PieceManagerTests {

	GameObject Manager;
	[SetUp]
	public void SetUp(){
		Manager = new GameObject();
		Manager.AddComponent<PieceManager>();
		Manager.AddComponent<NumberBag>();
		Manager.GetComponent<NumberBag>().amountToPool = 5;
		Manager.GetComponent<NumberBag>().GenerateNumbers();

		Manager.GetComponent<PieceManager>().playingPiece = (GameObject) Resources.Load("PlayPiece");
		Manager.GetComponent<PieceManager>().setUp();
	}

	[Test]
	public void CheckPieceArrayExists() {
		Assert.IsNotEmpty(Manager.GetComponent<PieceManager>().returnPieceArray());
	}

	[Test]
	public void CheckPieceArrayHasValue(){
		Manager.GetComponent<PieceManager>().index = 0; 
		Assert.IsNotNull(Manager.GetComponent<PieceManager>().returnPieceValue());
		Manager.GetComponent<PieceManager>().index = 1; 
		Assert.IsNotNull(Manager.GetComponent<PieceManager>().returnPieceValue());
	}

	[Test]
	public void checkIfPieceIsSelected(){
		Manager.GetComponent<PieceManager>().pieceClicked(0);
		Assert.AreEqual(true, Manager.GetComponent<PieceManager>().selected);
		Manager.GetComponent<PieceManager>().pieceClicked(0);
		Assert.AreNotEqual(true, Manager.GetComponent<PieceManager>().selected);
	}

	[TearDown]
	public void TearDown(){
		GameObject.DestroyImmediate(Manager);
	}
}
