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

		Validator.GetComponent<NumberBag>().GenerateNumbers();

		Validator.GetComponent<PieceManager>().setUp();

		Validator.GetComponent<BoxSpawner>().SetUp(5);
	}
	[Test]
	public static void CheckValidPositioning() {
		Assert.AreEqual(true, ValidationManager.PositioningValidation(2,3));
	}

	[Test]
	public static void CheckInvalidPositioning(){
		Assert.AreNotEqual(true, ValidationManager.PositioningValidation(0,0));
	}

	 [Test]
	public void checkIfRowValidation(){
		Validator.GetComponent<BoxSpawner>().setvalueAtPosition(2,3,4);
		Assert.True(ValidationManager.RowValidation(2,3,4));
	}

	[Test]
	public void CheckIfRowValidationCatchesError(){
		Validator.GetComponent<BoxSpawner>().setvalueAtPosition(2,3,3);
		Assert.False(ValidationManager.RowValidation(2,3,3));
	}

	[Test]
	public void CheckColumnValidation(){
		Validator.GetComponent<BoxSpawner>().setvalueAtPosition(1,2,4);
		Assert.True(ValidationManager.ColumnValidation(1,2,4));
	}

	[Test]
	public void CheckColValidationCatchesError(){
		Validator.GetComponent<BoxSpawner>().setvalueAtPosition(1,2,3);
		Assert.False(ValidationManager.ColumnValidation(1,2,3));
	}

	[Test]
	public void checkRowTotal(){
		Validator.GetComponent<BoxSpawner>().setvalueAtPosition(2,3,4);
		Assert.AreEqual(9, (ValidationManager.RowTotal(2,3) + 4));
	}

	[Test]
	public void CheckColumnTotal(){
		Validator.GetComponent<BoxSpawner>().setvalueAtPosition(1,2,4);
		Assert.AreEqual(9, (ValidationManager.columnTotal(1,2) + 4));
	}

	[Test]
	public void CheckOddValidationPasses(){
		Assert.True(ValidationManager.oddTotalValidation(11));
	}
	[Test]
	public void checkOddValidationFails(){
		Assert.False(ValidationManager.oddTotalValidation(50));
	}

	[Test]
	public void CheckSecondaryColumnsPass(){
		Validator.GetComponent<BoxSpawner>().setvalueAtPosition(1,3,3);
		Validator.GetComponent<BoxSpawner>().setvalueAtPosition(1,4,4);
		Assert.True(ValidationManager.secondaryColumnCheck(2,3));
	}

	[Test]
	public void CheckSecondaryColumnsFails(){
		Validator.GetComponent<BoxSpawner>().setvalueAtPosition(2,3,4);
		Assert.False(ValidationManager.secondaryColumnCheck(2,3));
	}

	[Test]
	public void CheckSecondaryRowsPass(){
		Validator.GetComponent<BoxSpawner>().setvalueAtPosition(1,3,3);
		Validator.GetComponent<BoxSpawner>().setvalueAtPosition(2,3,4);
		Assert.True(ValidationManager.secondaryColumnCheck(2,3));
	}

	[Test]
	public void CheckSecondaryRowsFail(){
		Validator.GetComponent<BoxSpawner>().setvalueAtPosition(1,3,3);
		Assert.True(ValidationManager.secondaryColumnCheck(2,3));
	}

	[Test]
	public void CheckRowAndColumnValidationPass(){
		Validator.GetComponent<BoxSpawner>().setvalueAtPosition(1,3,3);
		Validator.GetComponent<BoxSpawner>().setvalueAtPosition(2,4,4);
		Assert.True(ValidationManager.RowValidation(2,3,4) && ValidationManager.ColumnValidation(2,3,4));
	}

	[Test]
	public void CheckRowInvalidColumnValid(){
		Validator.GetComponent<BoxSpawner>().setvalueAtPosition(1,3,3);
		Validator.GetComponent<BoxSpawner>().setvalueAtPosition(2,4,1);
		Assert.False(ValidationManager.RowValidation(2,3,4) && ValidationManager.ColumnValidation(2,3,4));
	}

	[Test]
	public void CheckRowValidColumnInValid(){
		Validator.GetComponent<BoxSpawner>().setvalueAtPosition(1,3,2);
		Validator.GetComponent<BoxSpawner>().setvalueAtPosition(2,4,2);
		Assert.False(ValidationManager.RowValidation(2,3,4) && ValidationManager.ColumnValidation(2,3,4));		
	}

	[Test]
	public void CheckRowAndColumnValidationFail(){
		Validator.GetComponent<BoxSpawner>().setvalueAtPosition(1,3,2);
		Validator.GetComponent<BoxSpawner>().setvalueAtPosition(2,4,1);
		Assert.False(ValidationManager.RowValidation(2,3,4) && ValidationManager.ColumnValidation(2,3,4));
	}

	[TearDown]
	public void TearDown(){
		GameObject.DestroyImmediate(Validator);
	}
	
}
