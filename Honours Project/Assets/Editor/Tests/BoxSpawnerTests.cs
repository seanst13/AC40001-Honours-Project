﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI; 
using NUnit.Framework;


public class BoxSpawnerTests {
	GameObject Box;
	[SetUp]
	public void setUp(){
		Box = new GameObject();
		Box.AddComponent<BoxSpawner>(); 
		Box.AddComponent<NumberBag>();

		Box.GetComponent<NumberBag>().GenerateNumbers();
		
		Box.GetComponent<BoxSpawner>().SetUp(5);

	}
	[Test]
	public void CheckGridExists() {
		Assert.IsNotEmpty(Box.GetComponent<BoxSpawner>().returnGridArray());
	}

	[Test]
	public void CheckMiddleValue(){
		int middleval = Box.GetComponent<BoxSpawner>().getMiddleValue();

		Assert.AreEqual(5, int.Parse(Box.GetComponent<BoxSpawner>().returnValueAtPosition(middleval,middleval)));
	}

	[Test]
	public void checkObjectNames(){
		string objectNamme = "0_0";

		Assert.AreEqual(objectNamme, Box.GetComponent<BoxSpawner>().returnNameAtPosition(0,0));	
	}

	[Test]
	public void checkIfPiecesCanBeSet(){
		int value = 4; 

		Box.GetComponent<BoxSpawner>().setvalueAtPosition(2,1,value);
		Assert.AreEqual(value.ToString(),Box.GetComponent<BoxSpawner>().returnValueAtPosition(2,1));
		value--;
		Box.GetComponent<BoxSpawner>().setvalueAtPosition(3,3,value);
		
		Assert.AreEqual(value.ToString(),Box.GetComponent<BoxSpawner>().returnValueAtPosition(3,3));
	}

	[Test]
	public void CheckEmptyPiecesFail(){
		Box.GetComponent<BoxSpawner>().setvalueAtPosition(2,1,4);
		Assert.False(Box.GetComponent<BoxSpawner>().IsPositionEmpty(2,1));
	}

	[Test]
	public void CheckEmptyPiecesPasses(){
		Box.GetComponent<BoxSpawner>().setvalueAtPosition(2,1,4);
		Assert.True(Box.GetComponent<BoxSpawner>().IsPositionEmpty(2,0));
	}

	[Test]
	public void CheckEmptyOutOfBounds(){
		Assert.True(Box.GetComponent<BoxSpawner>().IsPositionEmpty(-1,10));
	}

	[TearDown]
	public void TearDown(){
		GameObject.DestroyImmediate(Box);
	}
}
