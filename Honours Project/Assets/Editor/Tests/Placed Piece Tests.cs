﻿using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class PlacedPieceTests {

	GameObject Manager;
	[SetUp]
	public void SetUp(){
		Manager = new GameObject(); 
		Manager.AddComponent<PieceManager>();
		Manager.AddComponent<NumberBag>();
		Manager.AddComponent<PlacedPieceManager>();
		Manager.AddComponent<BoxSpawner>();

		Manager.GetComponent<NumberBag>().amountToPool = 5;
		Manager.GetComponent<NumberBag>().GenerateNumbers();

		Manager.GetComponent<PieceManager>().playingPiece = (GameObject) Resources.Load("PlayPiece");
		Manager.GetComponent<PieceManager>().setUp();

		Manager.GetComponent<PlacedPieceManager>().SetUp();

		Manager.GetComponent<BoxSpawner>().SetUp(5);

	}

	[Test]
	public void checkListExists() {
		Assert.IsNotNull(Manager.GetComponent<PlacedPieceManager>().returnPlacedPieces());
	}

	[Test]
	public void checkPieceIsAdded(){
		Manager.GetComponent<PieceManager>().index = 0;
		Manager.GetComponent<PlacedPieceManager>().addPieceToList(1,1);
		Assert.AreEqual(1, Manager.GetComponent<PlacedPieceManager>().returnPlacedPieces().Count); 
	}

	[Test]
	public void checkListIsCleared(){
		Manager.GetComponent<PieceManager>().index = 0;
		Manager.GetComponent<PlacedPieceManager>().addPieceToList(1,0);
	
		Manager.GetComponent<PieceManager>().index = 1;
		Manager.GetComponent<PlacedPieceManager>().addPieceToList(1,1);

		Manager.GetComponent<PlacedPieceManager>().ClearPlacedPieces();

		Assert.IsEmpty(Manager.GetComponent<PlacedPieceManager>().returnPlacedPieces());
	}

	[Test]
	public void doesPieceArrayReactivate(){
		Manager.GetComponent<PieceManager>().index = 0;
		Manager.GetComponent<PlacedPieceManager>().addPieceToList(1,0);
		Manager.GetComponent<PlacedPieceManager>().reactivatePiece(1,0); 

		Assert.IsTrue(Manager.GetComponent<PieceManager>().IsElementActive(0));
	}

	[TearDown]
	public void TearDown(){
		GameObject.DestroyImmediate(Manager);
	}
}
