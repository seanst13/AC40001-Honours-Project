using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class ValidationTests {
	GameObject Validator; 
	[SetUp]
	public void SetUp(){
		Validator = new GameObject(); 
		Validator.AddComponent<PieceManager>();
		Validator.AddComponent<NumberBag>();
		Validator.AddComponent<BoxSpawner>(); 

		Validator.GetComponent<NumberBag>().amountToPool = 5; 
		Validator.GetComponent<NumberBag>().GenerateNumbers();
		Validator.GetComponent<PieceManager>().playingPiece = (GameObject) Resources.Load("PlayPiece");
		Validator.GetComponent<PieceManager>().setUp();
		Validator.GetComponent<BoxSpawner>().gridSize = 5;
		Validator.GetComponent<BoxSpawner>().whiteSquare = (GameObject) Resources.Load("WhiteBox");
		Validator.GetComponent<BoxSpawner>().greySquare = (GameObject) Resources.Load("GreyBox");
		Validator.GetComponent<BoxSpawner>().SetUp(5);
	}
	[Test]
	public void CheckValidPositioning() {
		Assert.AreEqual(true, ValidationManager.PositioningValidation(2,3));
	}

	[Test]
	public void CheckInvalidPositioning(){
		Assert.AreNotEqual(true, ValidationManager.PositioningValidation(0,0));
	}
}
