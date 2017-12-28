using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtons : MonoBehaviour {

public Canvas panelCanvas; 
private bool openpanel = false; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PanelState(){
		if (openpanel == false){
			openpanel = true;
			panelCanvas.enabled = true; 
		}
		else if (openpanel == true){
			openpanel = false;
			panelCanvas.enabled = false; 
		}
	}
}
