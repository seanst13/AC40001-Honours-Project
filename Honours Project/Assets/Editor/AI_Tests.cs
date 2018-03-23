using UnityEngine;
using UnityEditor;
using UnityEngine.UI; 
using NUnit.Framework;


public class AI_Tests {

	[Test]
	public void EditorTest() {
		//Arrange
		var AI = new AI_Player();
		var Box = new GameObject();
		Box.AddComponent<BoxSpawner>(); 

		Box.AddComponent<BoxSpawner>().gridSize = 5;
		Box.AddComponent<BoxSpawner>().whiteSquare = (GameObject) Resources.Load("WhiteBox");
		Box.AddComponent<BoxSpawner>().greySquare = (GameObject) Resources.Load("GreyBox");

		Box.GetComponent<BoxSpawner>().Start();
		// Box.GetComponent<BoxSpawner>().
		//Act
		//Try to rename the GameObject
		var newGameObjectName = "0_0";
		

		//Assert
		//The object has a new name
		Assert.IsNotEmpty(Box.GetComponent<BoxSpawner>().returnGridArray());
	}
}
