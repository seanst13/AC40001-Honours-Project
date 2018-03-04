using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PieceClick : MonoBehaviour {
	public void Clicked(){
		int index = int.Parse(this.name.Substring(2,1));
		PieceManager.instance.pieceClicked(index); 
	}
}
