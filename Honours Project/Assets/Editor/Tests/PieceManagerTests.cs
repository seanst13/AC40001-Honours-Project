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

		Manager.GetComponent<NumberBag>().GenerateNumbers();
		
		Manager.GetComponent<PieceManager>().setUp();
	}

	[Test]
	public void CheckPieceArrayExists() {
		Assert.IsNotEmpty(Manager.GetComponent<PieceManager>().returnPieceArray());
	}

	[Test]
	public void CheckPieceArrayHasValue(){
		Manager.GetComponent<PieceManager>().setIndex(0);
		Assert.IsNotNull(Manager.GetComponent<PieceManager>().returnPieceValue());
		Manager.GetComponent<PieceManager>().setIndex(1); 
		Assert.IsNotNull(Manager.GetComponent<PieceManager>().returnPieceValue());
	}

	[Test]
	public void checkIfPieceIsSelected(){
		Manager.GetComponent<PieceManager>().pieceClicked(0);
		Assert.AreEqual(true, PieceManager.returnSelected());
		Manager.GetComponent<PieceManager>().pieceClicked(0);
		Assert.AreNotEqual(true,PieceManager.returnSelected());
	}

	[TearDown]
	public void TearDown(){
		GameObject.DestroyImmediate(Manager);
	}
}